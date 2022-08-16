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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieDBContext _context;

        public MoviesController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            List<Movies> movies = new List<Movies>();

            movies = _context.Movies.Include(d => d.Director).Include(d => d.MovieCategorie).ToList();
            foreach (var movie in movies)
            {
                
                foreach (var c in movie.MovieCategorie)
                {
                    var category = _context.Categories.Where(f => f.Id == c.CategoryId).FirstOrDefault();
                    c.Category = category;

                }

             
            }




            return movies;
          
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovies(int id)
        {
            var movies = await _context.Movies.Include(d => d.Director).Include(d => d.MovieCategorie).
                Where(c => c.Id == id).FirstOrDefaultAsync();

            if (movies == null)
            {
                return NotFound();
            }

            return movies;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovies(int id, Movies movies)
        {
            if (id != movies.Id)
            {
                return BadRequest();
            }

            _context.Entry(movies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movies>> PostMovies(Movies movies)
        {
            _context.Movies.Add(movies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovies", new { id = movies.Id }, movies);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovies(int id)
        {
            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();

            return NoContent();
        }

         [HttpGet]
        public List<Movies> GetMoviesByCategoryId(int categoryId)
        {
            List<Movies> movies = new List<Movies>();
            var movieIdList =  _context.MovieCategorie.Where(c => c.CategoryId == categoryId).ToList();
            foreach (var movie in movieIdList)
            {
                var aMovie = _context.Movies.Where(c => c.Id == movie.MovieId).FirstOrDefault();
                foreach(var c in aMovie.MovieCategorie)
                {
                    var category = _context.Categories.Where(f => f.Id == c.CategoryId).FirstOrDefault();
                    c.Category = category;

                }  
                
                movies.Add(aMovie);
            }  


            

            return movies;
        }
        [HttpGet]
        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
