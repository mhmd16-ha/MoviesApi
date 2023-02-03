using MoviesApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTO
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        [MaxLength(2500)]
        public string? StoreLine { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public IFormFile? Poster { get; set; }
        public Byte GenraId { get; set; }
       
    }
}
