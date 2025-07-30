using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.Models
{
    public class ContestWinner
    {
        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public int Place { get; set; }

        public int WinnerId { get; set; }

        public virtual User Winner { get; set; }
    }
}
