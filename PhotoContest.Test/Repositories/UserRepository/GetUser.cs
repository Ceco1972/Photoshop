using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoContest.DataBase;
using PhotoContest.Models.Models;
using PhotoContest.Repository;
using PhotoContest.Service;
using PhotoContest.Web.Data;

namespace PhotoContest.Test.Repositories.UserRepo
{
    [TestClass]
    public class GetUser : BaseTest
    {
        private DbContextOptions<ApplicationDbContext> options;


        [TestInitialize]
        public void Inititialise()
        {
            this.options = Utils.GetOptions(nameof(TestContext.TestName));
            using (var arrangeContext = new ApplicationDbContext(this.options))
            {
                arrangeContext.Users.Add(Utils.GetUser());
                arrangeContext.SaveChanges();

            }

        }


        [TestMethod]
        public void GetUserById_Should()
        {
            //arrange
            var expectedUser = new User()
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
            //Act and Assert
            using (var assertContext = new ApplicationDbContext(this.options))
            {
                var sut = new UserRepository(assertContext);
                var resultDto = sut.GetUserByID(1);


                Assert.AreEqual(expectedUser.Id, resultDto.Id);
                Assert.AreEqual(expectedUser.Username, resultDto.Username);
                Assert.AreEqual(expectedUser.FirstName, resultDto.FirstName);
                Assert.AreEqual(expectedUser.LastName, resultDto.LastName);
                Assert.AreEqual(expectedUser.Password, resultDto.Password);
                Assert.AreEqual(expectedUser.Email, resultDto.Email);
                Assert.AreEqual(expectedUser.RoleId, resultDto.RoleId);
                Assert.AreEqual(expectedUser.IsOrganizer, resultDto.IsOrganizer);
                Assert.AreEqual(expectedUser.IsJury, resultDto.IsJury);
                Assert.AreEqual(expectedUser.IsPhotoJunkey, resultDto.IsPhotoJunkey);
            }
        }

        [TestMethod]
        public void GetUserByUsername_Should()
        {
            var expectedUser = new User()
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
            //Act and Assert
            using (var assertContext = new ApplicationDbContext(this.options))
            {
                var sut = new UserRepository(assertContext);
                var actualUser = sut.GetUserByUsername("photodictator");

                Assert.AreEqual(expectedUser.Id, actualUser.Id);
                Assert.AreEqual(expectedUser.Username, actualUser.Username);
                Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
                Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
                Assert.AreEqual(expectedUser.Password, actualUser.Password);
                Assert.AreEqual(expectedUser.Email, actualUser.Email);
                Assert.AreEqual(expectedUser.RoleId, actualUser.RoleId);
                Assert.AreEqual(expectedUser.IsOrganizer, actualUser.IsOrganizer);
                Assert.AreEqual(expectedUser.IsJury, actualUser.IsJury);
                Assert.AreEqual(expectedUser.IsPhotoJunkey, actualUser.IsPhotoJunkey);
            }

        }
       
    }
}