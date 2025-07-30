using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoContest.Models.Models;
using PhotoContest.Repository.Contracts;
using PhotoContest.Web.Data;
using System.Threading.Tasks;

namespace PhotoContest.Test.Repositories.PicturesRepo
{
    [TestClass]
    public class PictureInsert
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions(string databaseName)
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
        public async Task InsertPicture_Test()
        {
            Picture picture = new Picture
            {
                UserId = 0,
                ContestId = 0,
                ImageFile = new string('a', 5000),
                Title = "TestTitle",
                Story = "TestStory",
            };

            var databaseOptions = GetOptions("asd");
            using (var actContext = new ApplicationDbContext(databaseOptions))
            {
                actContext.Database.EnsureDeleted();
                actContext.Database.EnsureCreated();
                actContext.Pictures.Add(picture);
                actContext.SaveChanges();
            }
            bool isPictureUploaded = false;
            using (var assertContext = new ApplicationDbContext(databaseOptions))
            {
                isPictureUploaded = await assertContext.Pictures.AnyAsync();
            }
            Assert.IsTrue(isPictureUploaded);


        }
    }
}
