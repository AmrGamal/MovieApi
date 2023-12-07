using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.DAL.Contracts
{
    public interface IMovieStatsRepository
    {
        Task<IEnumerable<Stats>> GetAllMovieStats();
    }
}
