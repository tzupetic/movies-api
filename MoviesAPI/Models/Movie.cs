namespace MoviesAPI.Models
{
    public class Movie
    {
        public string searchType { get; set; }
        public string expression { get; set; }
        public List<MovieResult> results { get; set; }
        public string errorMessage { get; set; }
    }

    public class MovieResult
    {
        public string id { get; set; }
        public string resultType { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
