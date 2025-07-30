using PhotoContest.Models.Models.eNums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.Models
{
    public class ContestQueryParameters
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime FirstPhaseEnd { get; set; }
        public DateTime DueDate { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }

    }

}
