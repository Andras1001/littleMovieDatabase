using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace littleMovieDatabaseApp.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Text { get; set; }
    }
}
