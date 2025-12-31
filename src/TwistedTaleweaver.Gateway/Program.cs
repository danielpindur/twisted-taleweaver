using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Formatting.Compact;
using TwistedTaleweaver.Api.Expeditions.Events;
using TwistedTaleweaver.Api.Messages.Events;
using TwistedTaleweaver.Core.Setup;
using TwistedTaleweaver.Gateway.Auth;
using TwistedTaleweaver.Gateway.Common.Errors;
using TwistedTaleweaver.Setup;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .WriteTo.Console(formatter: new CompactJsonFormatter())
    .CreateLogger();

builder.Host.UseSerilog();

const string corsPolicyName = "GatewayCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

const string graphQlLimiterPolicyName = "graphql";
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy(graphQlLimiterPolicyName, context =>
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                     context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";
        
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: userId,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit =
                    context.User.Identity?.IsAuthenticated == true ? 200 : 50, // Higher limit for authenticated users
                Window = TimeSpan.FromMinutes(1)
            });
    });

    options.OnRejected = async (context, token) =>
    {
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            await context.HttpContext.Response.WriteAsync(
                $"Too many requests. Please try again after {retryAfter.TotalSeconds} seconds.",
                token);
        }
        else
        {
            await context.HttpContext.Response.WriteAsync(
                "Too many requests. Please try again later.",
                token);
        }
    };
});

const string defaultScheme = nameof(defaultScheme);
const string websocketScheme = nameof(websocketScheme);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = defaultScheme;
        options.DefaultAuthenticateScheme = defaultScheme;
    })
    .AddScheme<AuthenticationSchemeOptions, SkipWebSocketAuthenticationHandler>(websocketScheme, _ => { })
    .AddJwtBearer(defaultScheme, options =>
    {
        options.Authority = builder.Configuration["Clerk:Authority"];
        options.Audience = builder.Configuration["Clerk:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            NameClaimType = JwtRegisteredClaimNames.Sub
        };
        options.ForwardDefaultSelector = context =>
        {
            var hasTokenFromWebsocket = context.Items.ContainsKey(AuthenticationSocketSessionInterceptor.WEBSOCKET_TOKEN);

            if (!hasTokenFromWebsocket && context.WebSockets.IsWebSocketRequest)
            {
                return websocketScheme;
            }

            return defaultScheme;
        };
        options.Events.OnMessageReceived = context =>
        {
            if (context.HttpContext.Items.TryGetValue(AuthenticationSocketSessionInterceptor.WEBSOCKET_TOKEN, out var token))
            {
                context.Token = token as string;
            }

            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddWebSockets(_ => { });

builder
    .AddGraphQL()
    .AddSocketSessionInterceptor<AuthenticationSocketSessionInterceptor>()
    .AddGatewayTypes()
    .AddErrorFilter<GatewayErrorFilter>()
    .AddAuthorization()
    .AddInMemorySubscriptions()
    .ModifyRequestOptions(options =>
    {
        options.IncludeExceptionDetails = true; //builder.Environment.IsDevelopment();
        options.ExecutionTimeout = TimeSpan.FromSeconds(30);
    });

builder.Services    
    .AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserProvider, UserIdProvider>();

builder.AddCore()
    .AddKafkaEventConsumers(Assembly.GetExecutingAssembly())
    .SubscribeToEvent<MessageCreatedEvent>()
    .SubscribeToEvent<ExpeditionStateUpdatedEvent>();
builder.AddTwistedTaleweaver();

var app = builder.Build();

app.ConfigureCore();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (ctx, http) =>
    {
        ctx.Set("TraceId", http.TraceIdentifier);
        ctx.Set("UserId", http.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (http.Request.Headers.TryGetValue("X-Request-Id", out var rid))
        {
            ctx.Set("RequestId", rid.ToString());
        }
    };
    options.IncludeQueryInRequestPath = true;
});

app.UseCors(corsPolicyName);
app.UseWebSockets();
app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();

app.MapHealthChecks("/health").DisableRateLimiting();

app.MapGraphQL()
    .RequireCors(corsPolicyName)
    .RequireRateLimiting(graphQlLimiterPolicyName);

app.MapGet("/", () => Results.Redirect("/graphql"));

app.RunWithGraphQLCommands(args);
