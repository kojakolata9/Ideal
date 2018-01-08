using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ideal.Data.Models;
using Ideal.Services.Models;

namespace Ideal.Services
{
    public interface ITeamService : IService
    {
        Task CreateAsync(
            string name,
            int teamsize,
            string ideaownerId);

        Task<Team> ByIdAsync(int id);

        Task<IEnumerable<TeamListingModelService>> ActiveAsyncUserTeams(string id);
    }
}
