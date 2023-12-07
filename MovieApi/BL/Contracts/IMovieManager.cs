using MovieApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.BL.Contracts
{
    public interface IMovieManager
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<IEnumerable<Movie>> GetByMovieIdAndGroupByLanguage(int movieId);
        Task Add(Movie model);
    }
}
