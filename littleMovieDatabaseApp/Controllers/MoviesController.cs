using littleMovieDatabaseApp.Database;
using littleMovieDatabaseApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace littleMovieDatabaseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MyDatabase _dbContext;
        private readonly IHttpClientFactory _httpClientFactory;
        public MoviesController(MyDatabase dbContext,IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        //Storing a new movie in the database if it does not exist.
        public async Task<IActionResult> AddMovie([FromBody] string movieTitle)
        {
            //Checking if the Sqlite database already contains the movie.
            var existingMovie = _dbContext.Movies.FirstOrDefault(m => m.Title == movieTitle);

            if (existingMovie == null)
            {
                //If not, creating the OMDb request.
                var omdbApiKey = "f194e61d";
                var omdbApiURL = $"http://www.omdbapi.com/?t={movieTitle}&apikey={omdbApiKey}";

                using(var httpClient = _httpClientFactory.CreateClient())
                {
                    //Fetching the requested movie into an object.
                    var response = await httpClient.GetStringAsync(omdbApiURL);
                    var fetchedMovie = JsonConvert.DeserializeObject<OMDbResponse>(response);
                    
                    //Checking if it is in the OMDb
                    if (fetchedMovie==null|| fetchedMovie.Response == "False")
                    {
                        return Ok("Movie can't be found in OMDb database.");
                    }

                    //Creating a new movie object from the fetched data.
                    var movieToAdd = new Movie
                    {
                        Title = fetchedMovie.Title,
                        Year = fetchedMovie.Year,
                        Genre = fetchedMovie.Genre,
                        Actors = fetchedMovie.Actors,
                        PosterURL = fetchedMovie.Poster,
                        ImdbRating = fetchedMovie.imdbRating
                    };

                    //Adding it to the Sqlite database and saving the changes.
                    _dbContext.Movies.Add(movieToAdd);
                    await _dbContext.SaveChangesAsync();
                    return Ok("Movie successfully added to database:\n" + movieToAdd.writeOut());
                }          
           
            }
            return Ok("Movie already exist in database:\n"+existingMovie);
        }

        [HttpGet]
        //Returning all movies stored int the database
        public IActionResult GetMovies()
        {
            //Returning a List of Movie objects from the database.
            var movies = _dbContext.Movies.ToList();
            return Ok(movies);
        }

    }
}
