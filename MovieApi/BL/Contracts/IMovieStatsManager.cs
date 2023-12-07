using MovieApi.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.BL.Contracts
{
    public interface IMovieStatsManager
    {
        Task<IEnumerable<MovieStatsDto>> GetAllMoviesWithStats();
    }
}
