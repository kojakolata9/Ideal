using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ideal.Data;
using Ideal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ideal.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IdealDbContext db;


        public UserService(IdealDbContext db)
        {
            this.db = db;


        }
        public async Task CreateAsync(string name, string id)
        {


            db.TeamUsers.Add(new TeamUser() { Participant = db.Users.FirstOrDefault(t => t.Id == id), Team = db.Teams.FirstOrDefault(t => t.Name == name) });


            await this.db.SaveChangesAsync();
        }
    }
}
