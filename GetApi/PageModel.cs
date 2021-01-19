namespace GetApi
{
    // model of a single page
    public class PageModel
    {
        public string page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public AuthorModel[] data { get; set; }
    }
}
