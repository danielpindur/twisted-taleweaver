namespace TwistedTaleweaver.Common.Extensions;

public static class StringExtensions
{
    public static string ApplyFormat(this string input, params object[] args)
    {
        var paramNumber = 0;

        while (paramNumber < args.Length)
        {
            var placeholderString = $"{{{paramNumber}}}";
            input = input.Replace(placeholderString, args[paramNumber].ToString());
            
            paramNumber++;
        }

        return input;
    }
}