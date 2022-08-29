namespace MoviesAPI.Models.ViewModels
{
    public class MovieViewModel
    {
        public string expression { get; set; }
        public List<Movies> movies { get; set; }

        public MovieViewModel() {
            movies = new List<Movies>();
        }
    }

    public class Movies
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<Trailers> trailers { get; set; }

        public Movies()
        {
            trailers = new List<Trailers>();
        }
    }

    public class Trailers
    {
        public string videoId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string publishedAt { get; set; }
    }
}
