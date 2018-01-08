using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ideal.Services.Models;

namespace Ideal.Services
{
    public interface IIdeaService : IService
    {
        Task<IEnumerable<IdeaListingModelService>> ActiveAsyncApproved(int page,int pagesize);
        Task<IEnumerable<IdeaListingModelService>> ActiveAsyncNotApproved(int page, int pagesize);
        Task CreateAsync(
            string name,
            string description,
            string teamname,
            int teamsize,
            string ideaownerId);

        Task<IdeaDetailsModelService> ByIdAsync(int id);

        int TotalApproved();

        int TotalNotApproved();
    }
}
