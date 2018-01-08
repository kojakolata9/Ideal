using Ideal.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideal.Data
{
    public class IdealDbContext : IdentityDbContext<User>
    {


        public IdealDbContext(DbContextOptions<IdealDbContext> options)
            : base(options)
        {
        }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamUser> TeamUsers { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<TeamUser>()
                .HasKey(tu => new { tu.TeamId, tu.ParticipantId });

            builder
                .Entity<TeamUser>()
                .HasOne(tu => tu.Participant)
                .WithMany(p => p.Teams)
                .HasForeignKey(tu => tu.ParticipantId);

            builder
                .Entity<TeamUser>()
                .HasOne(tu => tu.Team)
                .WithMany(t => t.Participants)
                .HasForeignKey(tu => tu.TeamId);

            builder
                .Entity<Idea>()
                .HasOne(i => i.IdeaOwner)
                .WithMany(io => io.Ideas)
                .HasForeignKey(i => i.IdeaOwnerId);
        
            builder
                .Entity<Team>()
                .HasOne(i => i.Idea)
                .WithOne(t => t.Team)
                .HasForeignKey<Idea>(i=>i.TeamId);

            base.OnModelCreating(builder);
        }
    }
}
