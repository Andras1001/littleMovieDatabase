using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace littleMovieDatabaseApp.Models
{
    public class Movie
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; } 
        public string Actors { get; set; }
        public string PosterURL { get; set; }
        public string ImdbRating { get; set; }

        //Used for response in MoviesController
        public string writeOut()
        {
            return $"{Title} - {Year}, ({Genre})";
        }
    }
}
