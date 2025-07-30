using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Repository.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        //IEnumerable<Contest> GetUserContests(User user);
        User GetUserByID(int id);
        public void InsertUser(User user);
        void UpdateUser(User user);
        IEnumerable<Picture> GetUserPictures(User user);
        Task Save();
        bool IsUserValid(int userId);
        bool IsUserAuthor(int userId, int pictureId);
        User GetUserByUsername(string username);


    }
}
