using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Models.ViewModels;
using Newtonsoft.Json;
using System.Net;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IConfiguration configuration;
        private HttpClient client;
        public MoviesController(IConfiguration iConfig)
        {
            configuration = iConfig;
            client = new HttpClient();
        }

        // GET: api/Movies
        [HttpGet("Movies")]        
        public async Task<ActionResult<MovieViewModel>> GetMovies(string title)
        {
            Movie? movie = new Movie();
           
            string imdbApiKey = configuration.GetValue<string>("IMDBSettings:ApiKey");
            string imdbUrl = "https://imdb-api.com/en/API/SearchMovie/" + imdbApiKey + "/ " + title;

            using (var response = await client.GetAsync(imdbUrl))
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new InvalidOperationException("Invalid response from imdb api! Message: " + response.ReasonPhrase);
                }

                string apiResponse = await response.Content.ReadAsStringAsync();

                movie = JsonConvert.DeserializeObject<Movie>(apiResponse);
            }

            MovieViewModel movieVM = new MovieViewModel();            

            if (movie != null)
            {
                movieVM.expression = movie.expression;

                foreach (var mr in movie.results)
                {
                    var mvs = new Movies();
                    mvs.id = mr.id;
                    mvs.title = mr.title;
                    mvs.description = mr.description;

                    var video = await GetVideos(mr.title);

                    if (video != null)
                    {
                        foreach (var trailer in video.items)
                        {
                            mvs.trailers.Add(new Trailers
                            { 
                              videoId = trailer.id.videoId, 
                              title = trailer.snippet.title,
                              description = trailer.snippet.description,
                              publishedAt = trailer.snippet.publishedAt
                            });
                        }
                    }                    

                    movieVM.movies.Add(mvs);
                }
            }


            return movieVM;
        }

        private async Task<Video?> GetVideos(string title)
        {
            Video? video = new Video();
            
            string youTubeApiKey = configuration.GetValue<string>("YouTubeSettings:ApiKey");
            string youTubeUrl = "https://content-youtube.googleapis.com/youtube/v3/search?part=snippet&q=" + title + " trailer" + "&key=" + youTubeApiKey;

            using (var response = await client.GetAsync(youTubeUrl))
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new InvalidOperationException("Invalid response from youtube api! Message: " + response.ReasonPhrase);
                }

                string apiResponse = await response.Content.ReadAsStringAsync();
                video = JsonConvert.DeserializeObject<Video>(apiResponse);
            }            

            return video;
        }     
    }
}
