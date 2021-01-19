using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GetApi
{
    public static class ApiProcessor
    {

        public static async Task<List<AuthorModel>> LoadApi()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var resultList = new List<AuthorModel>();
            var numberOfPages = 0;
            var pageNumber = 1;

            do
            {
                var url = $"https://jsonmock.hackerrank.com/api/article_users/search?page={pageNumber}";

                using HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    PageModel result = JsonSerializer.Deserialize<PageModel>(await response.Content.ReadAsStringAsync());
                    numberOfPages = result.total_pages;
                    resultList.AddRange(result.data);
                    pageNumber++;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            } while (pageNumber <= numberOfPages);

            return resultList;
        }
    }
}
