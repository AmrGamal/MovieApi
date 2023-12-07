using MovieApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.DAL.Contracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<IEnumerable<Movie>> GetByMovieId(int id);
        Task Add(Movie model);

        Task<IEnumerable<Movie>> GetMoviesByMovieIds(IEnumerable<int> movieIds);
    }
}
