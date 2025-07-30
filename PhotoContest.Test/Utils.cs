using Microsoft.EntityFrameworkCore;
using PhotoContest.Web.Data;
using PhotoContest.Models.Models;
using System.Collections.Generic;
using PhotoContest.DataBase;

namespace  PhotoContest.Test 
{
    public class Utils
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(databaseName)
                       .Options;
        }

        

        public static User GetUser()
        {
            return ApplicationDbContext.GetOrganizer();
        }

        public static IEnumerable<User> GetUsers()
        {
            return ModelBuilderExtensions.Users;
        }
        public static IEnumerable<Picture> Pictures()
        {
            return ModelBuilderExtensions.Pictures;
        }
        public static IEnumerable<Contest> Contests()
        {
            return ModelBuilderExtensions.Contests;
        }
    }

}
