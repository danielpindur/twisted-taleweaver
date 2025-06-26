using System.Runtime.Serialization;

namespace TwistedTaleweaver.Core.Unit.Test.Json.Converters.Entities;

public enum TestEnum
{
    [EnumMember(Value = "first")]
    First,
    
    [EnumMember(Value = "second-value")]
    Second,
    
    Third // No EnumMember attribute to test fallback
}