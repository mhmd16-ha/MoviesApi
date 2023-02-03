using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTO;
using MoviesApi.Models;
using MoviesApi.Sevices;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private new List<string> _allowedExtension = new List<string> { ".png", ".jpg" };
        private long _allowedSize = 1048576;
        private readonly IMoviesServices _Movie;
        private readonly IGenrasService _genra;

        public MoviesController(IMoviesServices movie, IGenrasService genra )
        {
            _Movie = movie;
            _genra = genra;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var Movies = await _Movie.GetAll();
                
            return Ok(Movies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var movie = await _Movie.GetById(id);
            if (movie == null)
                return NotFound();
            var dto = new GetMoviesDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genra = movie.Genera.Name,
                Poster = movie.Poster,
                Rate = movie.Rate,
                StoreLine = movie.StoreLine,
                Year = movie.Year,
            };
            return Ok(dto);
        }
        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genraId)
        {

            var Movies = await _Movie.GetAll(genraId);
            return Ok(Movies);
        }
        [HttpPost]
        public async Task<IActionResult> CreatAsync([FromForm]CreateMovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Poster is required!");
            if (_allowedSize<dto.Poster.Length)
            {
                return BadRequest("Only 1 Mega");
            }
                if (!_allowedExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("Only jpg png");
            }
            var isVaildgenra = await _genra.isVaildgenra(dto.GenraId);
            if (!isVaildgenra)
            {
                return BadRequest("Not Vaild Id");
            }


            using var dataStreem = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStreem);

            var movie = new Movie
            {
                Title = dto.Title,
                Poster=dataStreem.ToArray(),
                Rate=dto.Rate,
                StoreLine=dto.StoreLine,
                Year=dto.Year,
                GenraId=dto.GenraId,


            };
            await _Movie.Add(movie);
           
            return Ok(movie);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        { 
            var movie = await _Movie.GetById(id);
            if (movie == null)
                return NotFound($"No movie With is Id {id}");
            _Movie.Delete(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id ,[FromForm]CreateMovieDto dto)
        {
            var result = await _Movie.GetById(id);
            if (dto.Poster != null) { 
            if (_allowedSize < dto.Poster.Length)
            {
                return BadRequest("Only 1 Mega");
            }
            if (!_allowedExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("Only jpg png");
            }
                using var dataStreem = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStreem);
                result.Poster = dataStreem.ToArray();

            }
            var isVaildgenra = await _genra.isVaildgenra(dto.GenraId );
            if (!isVaildgenra)
            {
                return BadRequest("Not Vaild GeneraSId");
            }
            if (result == null)
                return NotFound($"No movie With is Id {id}");
           
            result.StoreLine = dto.StoreLine;
            result.Year = dto.Year;
            result.GenraId = dto.GenraId;
            result.Title = dto.Title;
            _Movie.Update(result);
            return Ok(result);
        }
}}
