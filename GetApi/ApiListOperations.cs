using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetApi
{
    public class ApiListOperations
    {


        public static async Task<List<string>> GetUsernames(int threshold)
        {
            /* 
        This function would retrieve the names of the most active authors (using submission count as the criteria) according to a set threshold. 
            */

            List<AuthorModel> ListOfAuthors = await ApiProcessor.LoadApi();

            var result = ListOfAuthors
                .Where(author => author.submission_count >= threshold)
                .Select(author => author.username)
                .ToList();
            return result;
        }

        public static async Task<string> GetUsernameWithHighestCommentCount()
        {
            // This function would retrieve the name of the author with the highest comment count. 

            List<AuthorModel> ListOfAuthors = await ApiProcessor.LoadApi();

            var usernameWithHighestCommentCount = ListOfAuthors.OrderByDescending(author => author.comment_count)
                .FirstOrDefault().username;

            return usernameWithHighestCommentCount;
        }

        public static async Task<List<string>> GetUsernamesSortedByRecordDate(int threshold)
        {
            /* 
      This function would retrieve the names of authors sorted by when their record was created according to a set threshold. 
          */

            List<AuthorModel> ListOfAuthors = await ApiProcessor.LoadApi();
            List<string> ListOfUsernames = ListOfAuthors.Where(author => author.created_at >= threshold)
                .OrderBy(author => author.created_at)
                .Select(author => author.username)
                .ToList();

            return ListOfUsernames;

        }

        public static async Task<Dictionary<string, List<string>>> GetUsernamesAccordingToRange(Dictionary<int, int> ranges)
        {
            /* 
        This function would retrieve the names of authors according to articles submitted in the range of (0 - 50, 51 - 100, 101 - Above) 
            */

            List<AuthorModel> ListOfAuthors = await ApiProcessor.LoadApi();
            var dictionaryOfUsernames = ListOfAuthors
                .GroupBy(author => ranges
                .FirstOrDefault(i => i.Value >= author.submitted && author.submitted >= i.Key))
                .ToDictionary(dict => dict.Key.ToString(), dict => dict.Select(s => s.username)
                .ToList());

            dictionaryOfUsernames.Remove("[0, 0]");
            if (dictionaryOfUsernames.Count() == 0)
            {
                throw new ArgumentException("No Author with number of articles in the specified range");
            }
            return dictionaryOfUsernames;
        }


    }
}
