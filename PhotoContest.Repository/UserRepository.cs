using Microsoft.EntityFrameworkCore;
using PhotoContest.Common.Exceptions;
using PhotoContest.Models.Models;
using PhotoContest.Repository.Contracts;
using PhotoContest.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public User GetUserByUsername(string username)
        {
            var user = this.
               _context
               .Users
               .FirstOrDefault(u => u.Username == username);
                

            return user;
        }

       
        public User GetUserByID(int id)
        {
            var user = this._context
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefault();
            return user;
        }

        //public IEnumerable<Contest> GetUserContests(User user)
        //{
        //    var dbUser = this._context
        //        .Users.FirstOrDefault(u => u.Id == user.Id);
        //    return dbUser.AllContest;
        //}

        public IEnumerable<Picture> GetUserPictures(User user)
        {
            var pictures = this._context
                .Pictures
                .Where(p => p.Author == user);
            return pictures;
        }

        public IEnumerable<User> GetUsers()
        {
            return this
                ._context
                .Users.Where(u=>u.Role.RoleType==Models.Models.eNums.RoleType.PhotoJunkie);
        }

        public void InsertUser(User user)
        {   
            this._context.Users.Add(user);
            this._context.SaveChanges();
          

            //else
            //{
            //    this._context
            //        .Users
            //        .Add(user);
            //    await this.Save();
            //}
        }
        public bool IsUserAuthor(int userId, int pictureId)
        {
            var pToCheck = this._context
                .Pictures.FirstOrDefault(p => p.Id == pictureId);
            if (pToCheck.UserId == userId)
            {
                return true;
            }
            return false;
        }

        public bool IsUserValid(int userId)
        {
            var user = this.
                _context
                .Users
                .FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new AuthorisationException("This is not existing user!");
            }
            return true;
        }

        public async Task Save()
        {
            await this._context
                .SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            this._context
              .Entry(user)
              .State = EntityState.Modified;
            //await this.Save();
            this._context.SaveChanges();
        } 
    }
}
