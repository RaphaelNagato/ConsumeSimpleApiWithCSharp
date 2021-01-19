using System;
using System.Collections.Generic;
using System.Text;

namespace JsonHackerApi.UI
{
    static class CheckUserInput
    {
        public static int CheckInteger(string input)
        {
            if (int.TryParse(input, out int output))
            {
                return output;
            }
            throw new FormatException("Bad input");
        }
    }
}
