using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Common.Helpers;
using PhotoContest.Models.DTOMapper;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Service.Contracts;
using PhotoContest.Web.Controllers.API;
using PhotoContest.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Test.Controllers.ContestControllers
{
    [TestClass]
    public class GetControllers : BaseTest 
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
        public void GetContests_Should()
        {
           
            var context = new ApplicationDbContext(this.options);
            var expectedContests2 = context.Contests.ToList();
            var contestService = new Mock<IContestService>();
            var authMock = new Mock<AuthorisationHelper>();
            
            contestService
                .Setup(service => service.ReturnAll())
                .Returns(expectedContests2.GetDTO());
                            

            var controller = new ContestController(contestService.Object, authMock.Object);

            var actionResult = controller.Get() as ObjectResult;

            int statusCode = actionResult.StatusCode.Value;
            Assert.AreEqual(200, statusCode);

            var actualContests1 = actionResult.Value as IEnumerable <ContestDTO>;
            var actualContests = actualContests1.ToList();
            Assert.AreEqual(expectedContests2[0].Title, actualContests[0].Title);
            Assert.AreEqual(expectedContests2[1].Title, actualContests[1].Title);
            Assert.AreEqual(expectedContests2[2].Title, actualContests[2].Title);


        }
    }
}
