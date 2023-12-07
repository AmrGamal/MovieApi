using MovieApi.DAL.Contracts;
using MovieApi.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.DAL.Repositories
{
    public class MovieStatsRepository : IMovieStatsRepository
    {
        private readonly string _csvFilePath;

        public MovieStatsRepository(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public async Task<IEnumerable<Stats>> GetAllMovieStats()
        {
            var csvData = await CsvHelper.ReadAllLinesFromCsv(_csvFilePath);
            var movieStats = csvData.Select(row => CsvHelper.MapRowToStats(row));

            return movieStats;
        }

    }
}
