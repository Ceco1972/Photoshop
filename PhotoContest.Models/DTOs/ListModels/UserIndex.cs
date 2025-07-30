using System.Collections.Generic;

namespace PhotoContest.Models.DTOs.ListModels
{
    public class UserIndex 
    {
        public IEnumerable<UserDTO> UserList { get; set; }
    }
}
