using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoContest.DataBase;
using PhotoContest.Models.Models;
using PhotoContest.Repository;
using PhotoContest.Service;
using PhotoContest.Web.Data;
using System.Collections.Generic;
using System.Linq;

namespace PhotoContest.Test.Repositories.ContestRepo
{

    [TestClass]
    public class GetContest : BaseTest
    {
        private DbContextOptions<ApplicationDbContext> options;


        [TestInitialize]
        public void Inititialise()
        {
            this.options = Utils.GetOptions(nameof(TestContext.TestName));
            using (var arrangeContext = new ApplicationDbContext(this.options))
            {
                arrangeContext.Contests.AddRange(Utils.Contests());
                arrangeContext.Users.AddRange(Utils.GetUsers());
                arrangeContext.Pictures.AddRange(Utils.Pictures());
                arrangeContext.SaveChanges();

            }

        }

        [TestMethod]
        public void GetContestByTitle_Should()
        {
            //arrange
            var expectedContest = new Contest()
            {
                Id = 1,
                Title = "First"
            };
            //Act and Assert
            using (var assertContext = new ApplicationDbContext(this.options))
            {
                var sut = new ContestRepository(assertContext);
                var resultDto = sut.GetContestByTitle("First");


                Assert.AreEqual(expectedContest.Title, resultDto.Title);
                Assert.AreEqual(expectedContest.Id, resultDto.Id);
            }
        }

        [TestMethod]
        public void GetContestById_Should()
        {
            var expectedContest = new Contest()
            {
                Id = 2,
                Title = "Initial",
            };
            //Act and Assert
            using (var assertContext = new ApplicationDbContext(this.options))
            {
                var sut = new ContestRepository(assertContext);
                var actualContest = sut.GetById(2);

                Assert.AreEqual(expectedContest.Id, actualContest.Id);
                Assert.AreEqual(expectedContest.Title, actualContest.Title);
            }

        }

        [TestMethod]
        public void GetByPhase_Should()
        {
            var expectedContests = new List<Contest>
            {
                new Contest()
                {
                    Id = 2,
                    Title = "Initial",
                    Phase = Models.Models.eNums.Phase.Open
                },
                new Contest
                {
                     Id = 3,
                     Title = "Second",
                     Phase = Models.Models.eNums.Phase.Open
                }

            };

            //Act and Assert
            using (var assertContext = new ApplicationDbContext(this.options))
            {
                var sut = new ContestRepository(assertContext);
                var actualContests = sut.GetByPhase(Models.Models.eNums.Phase.Open).ToList();

               // Assert.AreEqual(expectedContests, actualContests);
                Assert.AreEqual(expectedContests[0].Id, actualContests[0].Id);
                Assert.AreEqual(expectedContests[0].Phase, actualContests[0].Phase);
                Assert.AreEqual(expectedContests[1].Id, actualContests[1].Id);
                Assert.AreEqual(expectedContests[1].Phase, actualContests[1].Phase);

            }
        }
    }
}