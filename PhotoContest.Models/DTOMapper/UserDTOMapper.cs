using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.DTOMapper
{
    public static class UserDTOMapper
    {
        public static UserDTO GetDTO(this User user)
        {
            if (user == null)
            {
                throw new ArgumentException("Check your credentials");
            }
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Score = user.Score,
                CreatedOn = user.CreatedOn,
                Email = user.Email
            };
        }

        public static User GetUser(this UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new User
            {
                Id = userDTO.Id,
                Username = userDTO.Username,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Score = userDTO.Score,
                CreatedOn = DateTime.Now,
                Email = userDTO.Email,
                Role = userDTO.Role
            };
        }
        public static IEnumerable<UserDTO> GetDTO(this IEnumerable<User> users)
        {
            return users.Select(GetDTO).ToList();
        }

        //public static User ConvertToModel(this UserDTO userDTO)
        //{
        //    User user = new User();
        //    user.FirstName = userDTO.FirstName;
        //    user.LastName = userDTO.LastName;
        //    user.Email = userDTO.Email;
        //    user.Password = userDTO.Password;
        //    return user;

        //}

        //public static User Convert(this RegisterViewModel viewModel)
        //{
        //    return new User()
        //    {
        //        Username = viewModel.Username,
        //        Password = viewModel.Password
        //    };
        //}
    }
}
