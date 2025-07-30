using Microsoft.EntityFrameworkCore;
using PhotoContest.DataBase.Config;
using PhotoContest.Models.Models;
using System.Linq;

namespace PhotoContest.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<UserContest> UserContests { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ContestWinner> ContestWinners { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserContestConfig());
            builder.ApplyConfiguration(new PictureVoteConfig());
            //builder.ApplyConfiguration(new VotePictureConfig());
            builder.ApplyConfiguration(new PictureConfig());
            builder.ApplyConfiguration(new ContestWinnerConfig());
            builder.ApplyConfiguration(new UserRoleConfig());
            builder.ApplyConfiguration(new RoleConfig());

            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleType = Models.Models.eNums.RoleType.PhotoJunkie
                },
                new Role
                {
                    Id = 2,
                    RoleType = Models.Models.eNums.RoleType.Organizer
                });
            var organizer = GetOrganizer();

            builder.Entity<User>().HasData(organizer);
            //organizer.Role = Roles.Where(r => r.Id == 2)
            //    .FirstOrDefault();
            //organizer.RoleId = 2;


            base.OnModelCreating(builder);
        }

        public static User GetOrganizer()
        {
            return new User
            {
                Id = 1,
                Username = "photodictator",
                FirstName = "James",
                LastName = "Pumphrey",
                Password = "parola123!",
                Email = "dictator@photocontest.com",
                RoleId = 2,
                IsOrganizer = true,
                IsJury = true,
                IsPhotoJunkey = false
            };

        }
    }
}