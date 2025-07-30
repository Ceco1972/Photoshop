using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public int ContestId { get; set; }
        public virtual Contest Contest { get; set; }
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
