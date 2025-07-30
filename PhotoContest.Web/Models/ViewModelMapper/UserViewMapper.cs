using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.ViewModelMapper
{
    public static class UserViewMapper
    {
        public static User GetUser(this RegisterViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new User
            {
                Username = viewModel.Username,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                CreatedOn = DateTime.Now,
                RoleId = 1,
                IsJury = false,
                IsOrganizer = false,
                IsPhotoJunkey = true,

            };
        }

        public static IEnumerable<User> GetDTO(this IEnumerable<RegisterViewModel> users)
        {
            return users.Select(GetUser).ToList();
        }
    }
}
