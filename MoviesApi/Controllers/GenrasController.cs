using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.DTO;
using MoviesApi.Models;
using MoviesApi.Sevices;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenrasController : ControllerBase
    {
        private readonly IGenrasService _genra ;
        public GenrasController(IGenrasService genra)
        {
            _genra = genra;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genra = await _genra.GetAllAsync();
            return Ok(genra);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGeneraDTO dto)
        {
            var genra = new Genra { Name = dto.Name };
             await _genra.Add(genra);

            return Ok(genra);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id,[FromBody]CreateGeneraDTO dto)
        {
            var genra = await _genra.GetById(id);
            if (genra == null)
                return NotFound($"No Genera With is Id {id}");
            genra.Name=dto.Name;
            _genra.Update(genra);
            return Ok(genra);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genra = await _genra.GetById(id);
            if (genra == null)
                return NotFound($"No Genera With is Id {id}");
            _genra.Delete(genra);
            return Ok(genra);
        }

    }
}
