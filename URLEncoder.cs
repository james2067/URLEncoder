using System;
using System.Collections.Generic;
using System.Linq;

namespace URLEncoder
{
    class Program
    {
        static Dictionary<string, string> characterEncodings = new Dictionary<string, string>() {
      { " ",  "%20" },
      { "<",  "%3C" },
      { ">",  "%3E" },
      { "#",  "%23" },
      { "%",  "%25" }, // these automatically change non allowed symbols to an allowed version
      { "\"", "%22" },
      { ";",  "%3B" },
      { "/",  "%2F" },
      { "?",  "%3F" },
      { ":",  "%3A" },
      { "@",  "%40" },
      { "&",  "%26" },
      { "=",  "%3D" },
      { "+",  "%2B" },
      { "$",  "%24" },
      { "{",  "%7B" },
      { "}",  "%7D" },
      { "|",  "%7C" },
      { "\\", "%5C" },
      { "^",  "%5E" },
      { "[",  "%5B" },
      { "]",  "%5D" },
      { "`",  "%60" }
    };

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("_____URL GENERATOR_____"); //begins UI
                string projectName = GetResponseFromUser("Project Name");
                string activityName = GetResponseFromUser("Activity Name");
                Console.WriteLine("\nYour Generated URL: https://companyserver.com/content/{0}/files/{1}/{1}Report.pdf\n",
                  EscapeTextForUrl(projectName),
                  EscapeTextForUrl(activityName)
                );
                Console.WriteLine("Would you like to generate another URL? (y/n)");
                running = Console.ReadLine().ToLower().Equals("y");
                Console.WriteLine("");
            }
        }

        static string GetResponseFromUser(string inputIdentifier) 
        {
            string text = string.Format("Enter in a(n) {0}: ", inputIdentifier); //Start of writeline on line 41
            string input = GetInput(text);
            while (string.IsNullOrEmpty(input) && !ContainsControlCharacters(input))
            {
                Console.WriteLine("Input was either empty or contained a control character!"); //error check
                input = GetInput(text);
            }
            return input;
        }

        static string GetInput(string text) //user input
        {
            Console.Write(text);
            return Console.ReadLine();
        }

        static bool ContainsControlCharacters(string input)
        {
            return input.Any(c => char.IsControl(c));
        }

        static string EscapeTextForUrl(string url)
        {
            return string.Join("", url.Select(c => EncodeCharacter(c)));
        }

        static string EncodeCharacter(char c)
        {
            if (characterEncodings.TryGetValue(c.ToString(), out string encodedCharacter))
            {
                return encodedCharacter;
            }
            else
            {
                return c.ToString();
            }
        }
    }
}
