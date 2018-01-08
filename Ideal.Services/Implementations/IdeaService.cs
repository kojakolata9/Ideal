using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ideal.Data;
using Ideal.Data.Models;
using Ideal.Services.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Ideal.Services.Implementations
{
    public class IdeaService : IIdeaService
    {
        private readonly IdealDbContext db;
        private IMapper mapper;


        public IdeaService(IdealDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;

        }

        public async Task<IEnumerable<IdeaListingModelService>> ActiveAsyncApproved(int page=1,int pagesize=8)
        {
            var ideas = await db.Ideas
                .Where(i=>i.IsApproved==true)
                .Skip((page-1)*pagesize)
                .Take(pagesize)
                .OrderByDescending(i=>i.Id)
                .ToListAsync();


            var newideas = Mapper.Map<IEnumerable<IdeaListingModelService>>(ideas);

            return newideas;
        }

        public async Task<IEnumerable<IdeaListingModelService>> ActiveAsyncNotApproved(int page=1, int pagesize=12)
        {
            var ideas = await db.Ideas
                .Where(i => i.IsApproved != true)
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .OrderByDescending(i => i.Id)
                .ToListAsync();

            var newideas = Mapper.Map<IEnumerable<IdeaListingModelService>>(ideas);

            return newideas;
        }

        public async Task<IdeaDetailsModelService> ByIdAsync(int id) 
        {
            var idea = await db.Ideas.FirstOrDefaultAsync(i=>i.Id==id);
            var user = await db.Users.FirstOrDefaultAsync(i => i.Id == idea.IdeaOwnerId);
            var team = await db.Teams.FirstOrDefaultAsync(i => i.Id == idea.TeamId);
            var participants = await db.TeamUsers.Where(tu => tu.TeamId == idea.TeamId).ToListAsync();
            var newidea = Mapper.Map<IdeaDetailsModelService>(idea);
            newidea.IdeaOwner = user.Name;
            newidea.TeamName = team.Name;
            newidea.Teamsize = team.Teamsize;
            foreach (var participant in participants)
            {
                newidea.Participants.Add(db.Users.FirstOrDefault(i => i.Id == participant.ParticipantId).UserName);
            }
            return newidea;
        }

        public int TotalApproved()
            => this.db.Ideas.Where(i=>i.IsApproved==true).ToList().Count;

        public int TotalNotApproved()
            => this.db.Ideas.Where(i => i.IsApproved == false).ToList().Count;


        public async Task CreateAsync(
            string name,
            string description,
            string teamname,
            int teamsize,
            string ideaownerId)
        {
            if (db.Ideas.Any(x => x.Name == name))
            {
                throw new Exception();
            }
            var idea = new Idea
            {
                Name = name,
                Description = description,              
                IdeaOwnerId = ideaownerId,
                Team = this.db.Teams.FirstOrDefault(t=>t.Name==teamname)
            };



            this.db.Add(idea);

            await this.db.SaveChangesAsync();

            
        }
    }
}
