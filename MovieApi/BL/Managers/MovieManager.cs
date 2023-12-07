using MovieApi.BL.Contracts;
using MovieApi.DAL.Contracts;
using MovieApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.BL.Managers
{
    public class MovieManager : IMovieManager
    {
        private readonly IMovieRepository _movieRepository;

        public MovieManager(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            var movies = await _movieRepository.GetAll();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetByMovieIdAndGroupByLanguage(int id)
        {
            // Read all rows from the CSV file
            var movies = await _movieRepository.GetByMovieId(id);

            // Group the Movie objects by Language and select the latest entry for each language
            var latestMetadataPerLanguage = movies
                .GroupBy(movie => movie.Language) // Group by Language
                .Select(g => g.OrderByDescending(m => m.Id).First()) // Select the Movie with the highest Id
                .OrderBy(movie => movie.Language); // Ensure alphabetical order by Language

            return latestMetadataPerLanguage;
        }

        public async Task Add(Movie movie)
        {
            await _movieRepository.Add(movie);
        }
    }



}
