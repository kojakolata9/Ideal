using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ideal.Data;

namespace Ideal.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IdealDbContext db;



        public AdminService(IdealDbContext db)
        {
            this.db = db;


        }
        public void Approve(int ideaId)
        {
            var idea = this.db.Ideas.FirstOrDefault(i => i.Id == ideaId);
            idea.IsApproved = true;
            db.SaveChanges();
        }
    }
}
