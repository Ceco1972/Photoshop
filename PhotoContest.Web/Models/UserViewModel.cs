using PhotoContest.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public DateTime CreatedOn { get; set; }

        public IEnumerable<ContestDTO> OpenContests { get; set; }
        public IEnumerable<ContestDTO> ContestsInVotePhase { get; set; }
        public IEnumerable<ContestDTO> FinishedContests { get; set; }
        public IEnumerable<ContestDTO> WinnedContests { get; set; }

    }
}
