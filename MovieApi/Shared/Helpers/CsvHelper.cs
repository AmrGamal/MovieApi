using MovieApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Shared.Helpers
{
    public static class CsvHelper
    {
        public async static Task<IEnumerable<string>> ReadAllLinesFromCsv(string csvFilePath)
        {
            // Read all lines from the CSV file
            var csvData = await File.ReadAllLinesAsync(csvFilePath);

            // Skip the header (assuming the first line is a header)
            return csvData.Skip(1);
        }

        //public static Movie MapRowToMovie(string row)
        //{
        //    var values = row.Split(',');

        //    if (values.Length > 6)
        //    {
        //        // Concatenate the title parts
        //        string title = string.Join(",", values.Skip(2).Take(values.Length - 5));

        //        return new Movie
        //        {
        //            Id = int.Parse(values[0]),
        //            MovieId = int.Parse(values[1]),
        //            Title = title,
        //            Language = values[values.Length - 3], //3 elements from the end is for language
        //            Duration = values[values.Length - 2], //2 elements from the end is for duration
        //            ReleaseYear = int.Parse(values[values.Length - 1]) //last element is for release year
        //        };
        //    }
        //    else
        //    {
        //        return new Movie
        //        {
        //            Id = int.Parse(values[0]),
        //            MovieId = int.Parse(values[1]),
        //            Title = values[2].Trim('\"'), // Trim quotes from title if present
        //            Language = values[3],
        //            Duration = values[4],
        //            ReleaseYear = int.Parse(values[5])
        //        };
        //    }
        //}

        public static Movie MapRowToMovie(string row)
        {
            var values = row.Split(',');
            
            if (values.Length < 6)
            {
                return null;
            }

            string title;
            if (values.Length > 6)
            {
                // Concatenate the title parts
                title = string.Join(",", values.Skip(2).Take(values.Length - 5));
            }
            else
            {
                // Trim quotes from title if present
                title = values[2].Trim('\"'); 
            }

            var movie = new Movie()
            {
                Id = int.Parse(values[0]),
                MovieId = int.Parse(values[1]),
                Title = title,
                Language = values[values.Length - 3], //3 elements from the end is for language
                Duration = values[values.Length - 2], //2 elements from the end is for duration
                ReleaseYear = int.Parse(values[values.Length - 1]) //last element is for release year
            };

            return validateMovie(movie) ? movie : null;
        }

        private static bool validateMovie(Movie movie)
        {
            // Only metadata with all data fields present should be returned, otherwise it should be considered invalid.
            if (movie == null)
            {
                return false;
            }

            // Check if any of the required fields are null or empty
            if (movie.MovieId <= 0 ||
                string.IsNullOrEmpty(movie.Title) ||
                string.IsNullOrEmpty(movie.Language) ||
                string.IsNullOrEmpty(movie.Duration) ||
                movie.ReleaseYear <= 0)
            {
                return false;
            }

            return true;
        }

        public static Stats MapRowToStats(string row)
        {
            var values = row.Split(',');
            return new Stats
            {
                MovieId = int.Parse(values[0]),
                WatchDurationMs = long.Parse(values[1])
            };
        }

        public static string SerializeToCsvRow(Movie movie)
        {
            return $"{movie.Id},{movie.MovieId},{movie.Title},{movie.Language},{movie.Duration},{movie.ReleaseYear}";
        }
    }
}
