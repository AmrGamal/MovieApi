using MovieApi.BL.Contracts;
using MovieApi.DAL.Contracts;
using MovieApi.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.BL.Managers
{
    public class MovieStatsManager : IMovieStatsManager
    {
        private readonly IMovieStatsRepository _movieStatsRepository;
        private readonly IMovieRepository _movieRepository;

        public MovieStatsManager(IMovieStatsRepository movieStatsRepository, IMovieRepository movieRepository)
        {
            _movieStatsRepository = movieStatsRepository;
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieStatsDto>> GetAllMoviesWithStats()
        {
            //Get Calculated Movie Stats and Calculating average and watch count
            var movieStats = await GetAllMovieStatsAndCalculateAvg();

            //Get distinc MovieIds from the stats
            var movieIdsWithStats = movieStats.Select(ms => ms.MovieId).Distinct();

            // Retrieve all movies with stats
            var relevantMovies = await _movieRepository.GetMoviesByMovieIds(movieIdsWithStats);
            
            // Convert to a dictionary for faster lookups
            var moviesDictionary = relevantMovies.GroupBy(m => m.MovieId).ToDictionary(g => g.Key, g => g.First());

            var result = movieStats
                .Where(ms => moviesDictionary.ContainsKey(ms.MovieId))
                .Select(ms => new MovieStatsDto
                {
                    MovieId = ms.MovieId,
                    Title = moviesDictionary[ms.MovieId].Title,
                    AverageWatchDurationS = Math.Ceiling(ms.AverageWatchDurationS),
                    Watches = ms.Watches,
                    ReleaseYear = moviesDictionary[ms.MovieId].ReleaseYear
                })
                .OrderByDescending(ms => ms.Watches);

            return result;
        }

        public async Task<IEnumerable<MovieStats>> GetAllMovieStatsAndCalculateAvg()
        {
            //Get all movie stats
            var movieStats = await _movieStatsRepository.GetAllMovieStats();

            //Calculate watches and average and convert milliseconds to seconds
            var stats = movieStats.GroupBy(x => x.MovieId)
                         .Select(group => new MovieStats
                         {
                             MovieId = group.Key,
                             AverageWatchDurationS = group.Average(x => x.WatchDurationMs) / 1000,
                             Watches = group.Count()
                         })
                         .OrderByDescending(x => x.Watches);

            return stats;
        }
    }
}
