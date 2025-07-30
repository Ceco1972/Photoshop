using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.Models
{
    public class PictureQueryParameters
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public string Rating { get; set; }
        public string CreatedOn { get; set; }
        public string SortBy { get; set; }
        public string SortOreder { get; set; }
    }
}
