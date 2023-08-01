using System.Text;

namespace Sanitize.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string Sanitize(this string str) 
        {
            if(string.IsNullOrEmpty(str)) return str;

            var newStr = new StringBuilder(str.Length);

            foreach( char c in str ) 
            {
                newStr.Append("*");
            }

            return newStr.ToString()!;
        }
    }
}
