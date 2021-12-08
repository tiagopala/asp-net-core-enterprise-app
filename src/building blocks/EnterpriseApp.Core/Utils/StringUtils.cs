using System.Linq;

namespace EnterpriseApp.Core.Utils
{
    public static class StringUtils
    {
        public static string CheckIfStringIsNumber(this string _, string input)
            => new(input.Where(char.IsDigit).ToArray());
    }
}
