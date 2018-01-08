using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ideal.Data;
using Ideal.Data.Models;
using Ideal.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Ideal.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly IdealDbContext db;

        public TeamService(IdealDbContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(string name, int teamsize, string ideaownerId)
        {
            if (db.Teams.Any(x=>x.Name==name))
            {
                throw new Exception();
            }
            var team = new Team()
            {
                Name = name,
                Teamsize = teamsize,
                
            };
            team.Participants.Add(new TeamUser(){ParticipantId = ideaownerId,TeamId = team.Id});
            this.db.Add(team);

            await this.db.SaveChangesAsync();
        }

        public async Task<Team> ByIdAsync(int id)
        {
            var team = await db.Teams.FirstOrDefaultAsync(i => i.Id == id);



            return team;
        }

        public async Task<IEnumerable<TeamListingModelService>> ActiveAsyncUserTeams(string id)
        {
            
            var teamUsers = await db
                .TeamUsers
                .Where(t => t.ParticipantId == id).Select(tu=>tu.TeamId).ToListAsync();
            var teams = await db
                .Teams
                .Where(t => teamUsers.Any(tu=>tu==t.Id)).ToListAsync();

            var newteams = Mapper.Map<IEnumerable<TeamListingModelService>>(teams);

            return newteams;
        }
    }
}
