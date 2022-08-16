using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCategoriesController : ControllerBase
    {
        private readonly MovieDBContext _context;

        public MovieCategoriesController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: api/MovieCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieCategorie>>> GetMovieCategorie()
        {
            return await _context.MovieCategorie.ToListAsync();
        }

        // GET: api/MovieCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieCategorie>> GetMovieCategorie(int id)
        {
            var movieCategorie = await _context.MovieCategorie.FindAsync(id);

            if (movieCategorie == null)
            {
                return NotFound();
            }

            return movieCategorie;
        }

        // PUT: api/MovieCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieCategorie(int id, MovieCategorie movieCategorie)
        {
            if (id != movieCategorie.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(movieCategorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieCategorieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MovieCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieCategorie>> PostMovieCategorie(MovieCategorie movieCategorie)
        {
            _context.MovieCategorie.Add(movieCategorie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MovieCategorieExists(movieCategorie.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovieCategorie", new { id = movieCategorie.CategoryId }, movieCategorie);
        }

        // DELETE: api/MovieCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieCategorie(int id)
        {
            var movieCategorie = await _context.MovieCategorie.FindAsync(id);
            if (movieCategorie == null)
            {
                return NotFound();
            }

            _context.MovieCategorie.Remove(movieCategorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        private bool MovieCategorieExists(int id)
        {
            return _context.MovieCategorie.Any(e => e.CategoryId == id);
        }
    }
}
