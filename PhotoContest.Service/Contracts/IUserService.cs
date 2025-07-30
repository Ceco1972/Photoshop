using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Service.Contracts
{
    public interface IUserService
    {
        User CheckUsername(string username);
        User InsertUser(User user);
        User GetUser(int userId);
        void UpdateUser(User user);
        IEnumerable<UserDTO> GetAllUsers();
        bool Exist(string username);
    }
}
