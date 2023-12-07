using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.BL.Contracts;
using MovieApi.DAL.Contracts;
using MovieApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieManager _movieManager;
        private readonly IMovieStatsManager _movieStatsManager;

        public MoviesController(IMovieManager movieManager, IMovieStatsManager movieStatsManager)
        {
            _movieManager = movieManager;
            _movieStatsManager = movieStatsManager;
        }

        [HttpGet("metadata/{movieId}")]
        public async Task<IActionResult> GetMovie(int movieId)
        {
            var movies = await _movieManager.GetByMovieIdAndGroupByLanguage(movieId);

            if (movies == null || !movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpPost("metadata")]
        public async Task<IActionResult> AddMovie([FromBody] Movie movieMetadata)
        {
            await _movieManager.Add(movieMetadata);

            return CreatedAtAction(nameof(GetMovie), new { movieId = movieMetadata.MovieId }, movieMetadata);
        }

        [HttpGet("movies/stats")]
        public async Task<IActionResult> GetMoviesStats()
        {
            var stats = await _movieStatsManager.GetAllMoviesWithStats();
            return Ok(stats);
        }

    }
}
