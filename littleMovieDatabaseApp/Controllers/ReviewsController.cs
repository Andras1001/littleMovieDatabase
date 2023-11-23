using littleMovieDatabaseApp.Database;
using littleMovieDatabaseApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace littleMovieDatabaseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MyDatabase _dbContext;

        public ReviewsController(MyDatabase dbContext)
        {
            _dbContext = dbContext;
        }
        public class ReviewInputModel
        {
            public int MovieId { get; set; }
            public string ReviewText { get; set; }
        }
        [HttpPost]
        public IActionResult AddReview([FromBody] ReviewInputModel reviewInput)
        {           
            // Check if the movie exists in database
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == reviewInput.MovieId);
            if (movie == null)
            {
                return BadRequest("Movie not found.");
            }

            // Save the review to the database
            var review = new Review
            {
                MovieId = reviewInput.MovieId,
                Text = reviewInput.ReviewText
            };

            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();

            return Ok("Review added successfully.");
        }

        [HttpGet]        
        //Returning all reviews stored int the database
        public IActionResult GetMovies()
        {
            var reviews = _dbContext.Reviews.ToList();
            return Ok(reviews);
        }
    }
}
