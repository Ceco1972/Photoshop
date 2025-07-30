using PhotoContest.Models.Models;
using PhotoContest.Models.Models.eNums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Models.DTOs
{
    public class ContestDTO
    {
      public int Id { get; set; }
       [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        public ContestType ContestType { get; set; }
        public DateTime FirstPhaseEnd { get; set; }
        public DateTime DueDate { get; set; }
        public Phase Phase { get; set; }
        public bool IsOpen { get; set; }
        public int OrganizerId { get; set; }
        public bool IsUserContest { get; set; }
        public IEnumerable<PictureDTO> Pictures { get; set; }
        public ICollection<UserContest> Participants { get; set; }

    }
}
