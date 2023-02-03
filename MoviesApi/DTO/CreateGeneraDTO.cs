using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTO
{
    public class CreateGeneraDTO
    {
        [MaxLength(length: 100)]
        public string? Name { get; set; }
    }
}
