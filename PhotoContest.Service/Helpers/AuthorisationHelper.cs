using PhotoContest.Common.Exceptions;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Service.Contracts;
using System;
using System.Threading.Tasks;

namespace PhotoContest.Common.Helpers
{
    public class AuthorisationHelper
    {
        private const string AuthorisationMessage = "Only organisers are authorised!";
        private const string AuthenticationMessage = "Authentication failed - check credentials!";

        private readonly IUserService userService;
        private readonly IContestService contestService;
        public AuthorisationHelper(IUserService userService, IContestService contestService)
        {
            this.userService = userService;
            this.contestService = contestService;
        }
        public AuthorisationHelper()
        { }

        public User TryGetUser(string username, string password)
        {
            try
            {
                var user = this.userService.CheckUsername(username);
                this.CheckPassword(user, password);
                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new AuthenticationException();
            }
            catch (AuthenticationException)
            {
                throw new AuthenticationException("Invalid credentials!");
            }
        }
        public User TryGetUser(string username)
        {
            try
            {
                return this.userService.CheckUsername(username);
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorisedOperationsException(AuthenticationMessage);
            }


        }

        public ContestDTO TryGetContest(string contestTitle)
        {
            try
            {
                return this.contestService.GetContestByTitle(contestTitle);
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorisedOperationsException(AuthenticationMessage);
            }
        }
        private void CheckPassword(User user, string password)
        {
            
                if (user == null)
                {
                    throw new AuthenticationException();
                }
                if (user.Password != password)
                {
                    throw new AuthenticationException();
                }
              
            
                        
        }
    }
}