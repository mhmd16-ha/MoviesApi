using MoviesApi.Models;

namespace MoviesApi.Sevices
{
    public interface IGenrasService
    {
        Task<IEnumerable<Genra>> GetAllAsync();
        Task<Genra> GetById(byte id);
        Task<Genra> Add(Genra genra);
        Genra Update(Genra genra);
        Genra Delete(Genra genra);
        Task<bool> isVaildgenra (byte id);



    }
}
