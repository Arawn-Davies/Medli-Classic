using System;
using System.Collections.Generic;
using System.Text;

namespace Medli.System
{
    public static class StringExtensions
    {
        public static String[] SplitAtFirstSpace(this String source)
        {
            if (String.IsNullOrEmpty(source)) return new String[] { };

            var index = source.IndexOf(' ');

            if (index == -1) return new String[] { source };

            return new String[] { source.Substring(0, index), source.Substring(index + 1) };
        }
    }
}
