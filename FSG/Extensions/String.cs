using System;
namespace FSG.Extensions
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string str)
        {
            return string.Concat(str.Remove(1).ToUpper(), str.AsSpan(1));
        }
    }
}

