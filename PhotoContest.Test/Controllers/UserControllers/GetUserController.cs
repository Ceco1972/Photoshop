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

namespace PhotoContest.Test.Controllers.UserControllers
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
        public void GetUserById_Should()
        {
            var context = new ApplicationDbContext(this.options);
            User expectedUser = context.Users.Where(u => u.Username == "photodictator").FirstOrDefault();

            var userService = new Mock<IUserService>();
            var authHelper = new Mock<AuthorisationHelper>();
            userService
               .Setup(service => service.GetUser(1))
               .Returns(expectedUser);
            var controller = new UserController(userService.Object, authHelper.Object);
            var actionResult = controller.GetUserById(1) as ObjectResult;

            int statusCode = actionResult.StatusCode.Value;
            Assert.AreEqual(200, statusCode);

            var acturalUser = actionResult.Value as User;

            Assert.AreEqual(expectedUser.Id, acturalUser.Id);
            Assert.AreEqual(expectedUser.Username, acturalUser.Username);
        }

        [TestMethod]
        public void GetUserByUsername_Should()
        {
            var context = new ApplicationDbContext(this.options);
            var expectedUser = context.Users.Where(u => u.Username == "photodictator").FirstOrDefault();
            var userService = new Mock<IUserService>();
            var authHelper = new Mock<AuthorisationHelper>();
            userService
               .Setup(service => service.CheckUsername("photodictator"))
               .Returns(expectedUser);
            var controller = new UserController(userService.Object, authHelper.Object);
            var actionResult = controller.GetUserByUsername("photodictator") as ObjectResult;
            int statusCode = actionResult.StatusCode.Value;
            Assert.AreEqual(200, statusCode);

            var acturalUser = actionResult.Value as User;

            Assert.AreEqual(expectedUser.Id, acturalUser.Id);
            Assert.AreEqual(expectedUser.Username, acturalUser.Username);
        }
        [TestMethod]
        public void RegisterUser_Should()
        {
            var expectedUserDto = new UserDTO();
            expectedUserDto.FirstName = "Tsvetan";
            expectedUserDto.LastName = "Ivanov";
            expectedUserDto.Username = "Tsvetancho";
            User expectedUser = expectedUserDto.GetUser();

            var userService = new Mock<IUserService>();
            var authHelper = new Mock<AuthorisationHelper>();
            userService
               .Setup(service => service.InsertUser(expectedUser))
               .Returns(expectedUser);

            var controller = new UserController(userService.Object, authHelper.Object);
            var actionResult = controller.RegisterUser(expectedUserDto) as ObjectResult;

            int statusCode = actionResult.StatusCode.Value;
            Assert.AreEqual(201, statusCode);

            var actualUser = actionResult.Value as UserDTO;

            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);

        }
        /*
                [TestMethod]
                public void Login_Should()
                {
                    string username = "Papi";
                    string password = "123";
                    User expectedUser = new User();
                    expectedUser.Username = username;
                    expectedUser.Password = password;
                    expectedUser.FirstName = "Papi";
                    expectedUser.LastName = "Papi";

                    var userService = new Mock<IUserService>();
                    userService
                        .Setup(service => service.CheckUsername(username))
                        .Returns(expectedUser);

                    var authHelper = new Mock<AuthorisationHelper>();
                    //authHelper
                    //   .Setup(service => service.TryGetUser(username, password))
                    //   .Returns(expectedUser);

                    var userDTO = expectedUser.GetDTO();

                    var controller = new UserController(userService.Object, authHelper.Object);
                    var actionResult = controller.Login(username, password) as ObjectResult;

                    int statusCode = actionResult.StatusCode.Value;
                    Assert.AreEqual(201, statusCode);

                    var actualUser = actionResult.Value as UserDTO;

                    Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
                    Assert.AreEqual(expectedUser.LastName, actualUser.LastName);

                }*/



    }
}
