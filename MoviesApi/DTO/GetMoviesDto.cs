namespace MoviesApi.DTO
{
    public class GetMoviesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? StoreLine { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public Byte[] Poster { get; set; }
        public string Genra { get; set; }
    }
}
