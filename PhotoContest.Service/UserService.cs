using PhotoContest.Models.Models;
using PhotoContest.Repository.Contracts;
using PhotoContest.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoContest.Models.DTOMapper;
using PhotoContest.Models.DTOs;



namespace PhotoContest.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User CheckUsername(string username)
        {
            var user = this._userRepository.GetUserByUsername(username);
            return user;
        }
        public bool Exist(string username)
        {
            var user = this.CheckUsername(username);

            if (user != null)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = this._userRepository.GetUsers();
            return users.GetDTO();

        }
        public User GetUser(int userId)
        {
            return this._userRepository.GetUserByID(userId);
        }

        public User InsertUser(User user)
        {
            this._userRepository.InsertUser(user);
            return user;
        }

        public /*async*/ void UpdateUser(User user)
        {
           /* await */this._userRepository.UpdateUser(user);
        }
    }
}
