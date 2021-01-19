using System;

namespace JsonHackerApi.UI
{
    // checks user input
    static class CheckUserInput
    {
        // check if value entered is an integer
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
