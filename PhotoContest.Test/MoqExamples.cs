using Moq;
using System;
using PhotoContest.Service.Contracts;
using PhotoContest.Models.Models;

namespace PhotoContest.Test 
{
    class MoqExamples
    {
        //A list of some of the features Moq provides.
        //Check the quickstart guide for more: https://github.com/Moq/moq4/wiki/Quickstart
        public void Examples()
        {
            //Create a mock object
            var mockService = new Mock<IUserService>();


            //Setup Behavior
            mockService.Setup(x => x.CheckUsername("TestUser"))
                .Returns(new User { Id = 1, Username = "TestUser" });


            //Matching Arguments
            mockService.Setup(x => x.CheckUsername(It.IsAny<string>()))
                .Returns(new User { Id = 1, Username = "TestUser" });


            //Throw when invoking with specific arguments
            mockService.Setup(x => x.CheckUsername("Peter"))
               .Throws<InvalidOperationException>();


            //Setup sequential calls
            mockService.SetupSequence(x => x.CheckUsername(It.IsAny<string>()))
               .Returns(new User { Id = 1, Username = "TestUser1" })
               .Returns(new User { Id = 2, Username = "TestUser2" })
               .Returns(new User { Id = 3, Username = "TestUser3" })
               .Returns(new User { Id = 4, Username = "TestUser4" });


            //Verify a method was never called
            mockService.Verify(x => x.CheckUsername(It.IsAny<string>()), Times.Never());


            //Verify a method was called once
            mockService.Verify(x => x.CheckUsername(It.IsAny<string>()), Times.Once());
        }
    }
}
