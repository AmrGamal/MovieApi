using MovieApi.DAL.Contracts;
using MovieApi.Shared.Helpers;
using MovieApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.DAL.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string _csvFilePath;

        public MovieRepository(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            // Read all rows from the CSV file
            var csvData = await CsvHelper.ReadAllLinesFromCsv(_csvFilePath);

            // Parse each row and map it to Movie
            var models = csvData.Select(row => CsvHelper.MapRowToMovie(row));

            return models;
        }

        public async Task<IEnumerable<Movie>> GetByMovieId(int id)
        {
            // Read all rows from the CSV file
            var csvData = await CsvHelper.ReadAllLinesFromCsv(_csvFilePath);

            // Find the relevant rows with the specified movie id
            var relevantRows = csvData.Where(row =>
            {
                var values = row.Split(',');
                var movieId = int.Parse(values[1]);

                // Return true if the movieId matches the requested id
                return movieId == id;
            });

            // Now that we have the relevant rows, map them to Movie objects
            var relevantMovies = relevantRows.Select(row => CsvHelper.MapRowToMovie(row)).ToList();

            return relevantMovies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByMovieIds(IEnumerable<int> movieIds)
        {
            // Read all rows from the CSV file
            var csvData = await CsvHelper.ReadAllLinesFromCsv(_csvFilePath);

            // Find the relevant rows with the specified movie id
            var relevantRows = csvData.Where(row =>
            {
                var values = row.Split(',');
                var movieId = int.Parse(values[1]);

                // Return true if the movieId matches the requested id
                return movieIds.Contains(movieId);
            });

            // Now that we have the relevant rows, map them to Movie objects
            var relevantMovies = relevantRows.Select(row => CsvHelper.MapRowToMovie(row)).ToList();

            return relevantMovies;
        }

        public async Task Add(Movie movie)
        {
            //Find lastId and increment it by 1
            var maxId = await GetMaxId();
            movie.Id = maxId + 1;

            // Ensure you handle exceptions and file locking appropriately.
            var newRow = CsvHelper.SerializeToCsvRow(movie);

            using (var streamWriter = File.AppendText(_csvFilePath))
            {
                await streamWriter.WriteLineAsync(newRow);
            }
        }

        public async Task<int> GetMaxId()
        {
            var rows = await CsvHelper.ReadAllLinesFromCsv(_csvFilePath);
            var ids = rows.Select(line => int.Parse(line.Split(',')[0])).ToList();
            int maxId = ids.Any() ? ids.Max() : 0;

            return maxId;
        }

    }
}
