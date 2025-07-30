using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.Models
{
    public class Reward
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public int Place { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int ContestId { get; set; }
        public virtual Contest Contest { get; set; }
    }
}
