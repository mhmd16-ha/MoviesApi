using MoviesApi.DTO;
using MoviesApi.Models;

namespace MoviesApi.Sevices
{

    public class MovieSevices : IMoviesServices
    {
        private readonly ApplicationDbContext _context;

        public MovieSevices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genraId = 0)
        {
         var result=  await _context.Movies
                 .Where(m=>m.GenraId==genraId||genraId==0)
                 .Include(m => m.Genera)
                 .OrderByDescending(m => m.Rate).ToListAsync();
            return result;
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _context.Movies.Include(m => m.Genera).SingleOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
