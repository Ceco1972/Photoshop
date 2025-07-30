using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.DTOs
{
    public class VoteDTO
    {
        public int ContestId { get; set; }
        public virtual ContestDTO Contest { get; set; }
        public int UserId { get; set; }
        public virtual UserDTO User { get; set; }
        public int PictureId { get; set; }
        public virtual PictureDTO Picture { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
