namespace MoviesAPI.Models
{
    public class Video
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public List<VideoList> items { get; set; }

    }

    public class VideoList
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public Id id { get; set; }
        public Snippet snippet { get; set; }
    }

    public class Id
    {
        public string kind { get; set; }
        public string videoId { get; set; }
    }

    public class Snippet
    {
        public string publishedAt { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
