using GetApi;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JsonHackerApi.UI
{
    class MainForm
    {
        
        private readonly ILogger<MainForm> _logger;

        // logger dependency injected
        public MainForm(ILogger<MainForm> logger)
        {
            _logger = logger;
        }

        public async Task Run()
        {
            var done = false;
            while (!done)
            {
                Console.WriteLine("Welcome to The JSON Happy Interface");
                Console.WriteLine(@"Enter:
    '1' to Get the names of the most active authors with submission count as threshold
    '2' to Get the author with most comment count
    '3' to Get author names with date created as threshold
    '4' to Get the authors names that have written a number of articles in a specified range
    'q' to Quit");

                string value = Console.ReadLine();
                switch (value)
                {
                    case "1":

                        try
                        {
                            Console.WriteLine("Enter submission count");
                            var input1 = Console.ReadLine();
                            int input1Int = CheckUserInput.CheckInteger(input1);
                            // get the names of authors with submission threshold and above
                            var names = await ApiListOperations.GetUsernames(input1Int);

                            if (names.Count != 0)
                            {
                                Console.WriteLine($"Author names with submission count of {input1Int} and above");

                                foreach (var name in names)
                                {
                                    Console.WriteLine(name);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Author Found");

                            }
                        }
                        catch (FormatException ex)
                        {
                            _logger.LogWarning(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                        catch (HttpRequestException ex)
                        {
                            _logger.LogError(ex.StackTrace);
                            Console.WriteLine("Error retrieving information");
                        }
                        Console.WriteLine();
                        Console.WriteLine("press 'Enter' key to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        try
                        {
                            var authorName = await ApiListOperations.GetUsernameWithHighestCommentCount();
                            Console.WriteLine($"The author with the highest comment count is {authorName}");
                        }
                        catch (HttpRequestException ex)
                        {

                            _logger.LogError(ex.StackTrace);
                            Console.WriteLine("Error retrieving information");

                        }
                        Console.WriteLine();
                        Console.WriteLine("press 'Enter' key to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("Enter Date created as threshold");
                            var input2 = Console.ReadLine();
                            int input2Int = CheckUserInput.CheckInteger(input2);
                            var names2 = await ApiListOperations.GetUsernamesSortedByRecordDate(input2Int);
                            if (names2.Count != 0)
                            {
                                Console.WriteLine($"Author names created by {input2Int} and  above");
                                foreach (var name2 in names2)
                                {
                                    Console.WriteLine(name2);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Author found");
                            }

                        }
                        catch (FormatException ex)
                        {
                            _logger.LogWarning(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                        catch (HttpRequestException ex)
                        {

                            _logger.LogError(ex.StackTrace);
                            Console.WriteLine("Error retrieving information");

                        }
                        Console.WriteLine();
                        Console.WriteLine("press 'Enter' key to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "4":
                        try
                        {
                            Console.WriteLine("Enter minimum value of range");
                            var input3 = Console.ReadLine();
                            int input3Int = CheckUserInput.CheckInteger(input3);
                            Console.WriteLine("Enter maximum value of range");
                            var input4 = Console.ReadLine();
                            int input4Int = CheckUserInput.CheckInteger(input4);
                            var inputDict = new Dictionary<int, int>()
                            {
                                    {input3Int, input4Int }
                            };
                            // pass in the input dictionary
                            var returnDict = await ApiListOperations.GetUsernamesAccordingToRange(inputDict);
                            foreach (var item in returnDict)
                            {
                                Console.WriteLine($"Names of authors with number of articles in the range of " +
                                    $"{item.Key}");

                                foreach (var name in item.Value)
                                {
                                    Console.WriteLine(name);
                                }
                            }
                        }
                        catch (Exception ex) when (ex is FormatException || ex is ArgumentException)
                        {
                            _logger.LogWarning(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                        catch (HttpRequestException ex)
                        {

                            _logger.LogError(ex.StackTrace);
                            Console.WriteLine("Error retrieving information");

                        }
                        Console.WriteLine();
                        Console.WriteLine("press 'Enter' key to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "q":
                        Console.WriteLine("Thank you for your time, program ended");
                        Console.WriteLine("press 'Enter' key to continue");
                        Console.ReadLine();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        Console.WriteLine("press 'Enter' key to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }

    }
}
