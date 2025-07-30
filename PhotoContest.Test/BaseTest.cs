using PhotoContest.Web.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoContest.Test 
{
    public abstract class BaseTest
    {
        [TestCleanup()]
        public void Cleanup()
        {
            var options = Utils.GetOptions(nameof(TestContext.TestName));
            var context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();
        }
    }
}
