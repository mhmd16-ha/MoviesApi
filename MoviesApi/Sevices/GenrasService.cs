using MoviesApi.Models;

namespace MoviesApi.Sevices
{
    public class GenrasService : IGenrasService
    {
        private readonly ApplicationDbContext _context;
        public GenrasService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Genra> Add(Genra genra)
        {           
            await _context.Genras.AddAsync(genra);
            _context.SaveChanges();
            return genra;
        }

        public Genra Delete(Genra genra)
        {                    
            _context.Genras.Remove(genra);
            _context.SaveChanges();
            return genra;
        }

        public async Task<IEnumerable<Genra>> GetAllAsync()
        {
          return await _context.Genras.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genra> GetById(byte id)
        {
            var genra = await _context.Genras.SingleOrDefaultAsync(g => g.Id == id);
            return genra;
        }

        public Task<bool> isVaildgenra(byte id)
        {
           return _context.Genras.AnyAsync(g => g.Id ==id );
          
        }

        public Genra Update(Genra genra)
        {
          
            _context.Update(genra);
            _context.SaveChanges();
            return genra;
        }
    }
}
