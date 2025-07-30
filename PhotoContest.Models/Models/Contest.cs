using PhotoContest.Models.Models.eNums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.Models
{
    public class Contest
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public ContestType ContestType { get; set; }
        public DateTime FirstPhaseEnd { get; set; }
        public DateTime DueDate { get; set; }
        public IEnumerable<Picture> Pictures { get; set; } = new List<Picture>();
        public ICollection<UserContest> Participants { get; set; }
        public ICollection<ContestWinner> Winners { get; set; }
        public int OrganizerId { get; set; } /*= User.Id(When user.isOrganizer = true);*/
        public bool IsOpen { get; set; }
        public Phase Phase { get; set; }

    }
}
