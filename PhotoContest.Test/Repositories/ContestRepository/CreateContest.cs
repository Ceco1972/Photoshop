using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoContest.Models.Models;
using PhotoContest.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Test.Repositories.ContestRepo 
{
    [TestClass]
    public class CreateContest
    {
        static DbContextOptions<ApplicationDbContext> GetOptions(string databaseName)
        {
            var provider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(provider)
                .Options;

        }

        [TestMethod]
        public async Task Create_Should()
        {
            Contest contest = new Contest
            {
                Title = "TestTitle",
                Category = "TestCategory",
                ContestType = Models.Models.eNums.ContestType.Open

            };

            var databaseOptions = GetOptions("asd");

            using (var assertContext = new ApplicationDbContext(databaseOptions))
            {

                assertContext.Database.EnsureDeleted();
                assertContext.Database.EnsureCreated();
                assertContext.Contests.Add(contest);
                assertContext.SaveChanges();
            }

            bool isContestCreated = false;

            using (var assertContext = new ApplicationDbContext(databaseOptions))
            {
                isContestCreated = await assertContext.Contests.AnyAsync();
            }

            Assert.IsTrue(isContestCreated);

        }
    }
}
