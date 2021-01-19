using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GetApi
{
    public static class ApiProcessor
    {
        /// <summary>
        ///  Consumes the API and returns a List of the Author Model
        /// </summary>
        /// <returns></returns>
        public static async Task<List<AuthorModel>> LoadApi()
        {
            // initialize http client
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var resultList = new List<AuthorModel>();
            var numberOfPages = 0;
            var pageNumber = 1;

            // A loop to retrieve data from the two pages of the API
            do
            {
                var url = $"https://jsonmock.hackerrank.com/api/article_users/search?page={pageNumber}";

                using HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // deserialize into c# object
                    PageModel result = JsonSerializer.Deserialize<PageModel>(await response.Content.ReadAsStringAsync());
                    numberOfPages = result.total_pages;
                    resultList.AddRange(result.data);
                    pageNumber++;

                }
                else
                {
                    throw new FormatException(response.ReasonPhrase);
                }

            } while (pageNumber <= numberOfPages);

            return resultList;
        }
    }
}
