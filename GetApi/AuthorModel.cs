using System;

namespace GetApi
{
    // model of the response.data
    public class AuthorModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string about { get; set; }
        public int submitted { get; set; }
        public DateTime updated_at { get; set; }
        public int submission_count { get; set; }
        public int comment_count { get; set; }
        public int created_at { get; set; }
    }
}
