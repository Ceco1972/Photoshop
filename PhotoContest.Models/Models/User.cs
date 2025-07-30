using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Models.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }

        public DateTime CreatedOn { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<UserContest> UserContest { get; set; }
        public ICollection<ContestWinner> WinnedContests { get; set; }
        
        public bool IsJury { get; set; }
        public bool IsLogged { get; set; }
        public bool IsPhotoJunkey { get; set; }
        public bool IsOrganizer { get; set; }
        
    }
}
