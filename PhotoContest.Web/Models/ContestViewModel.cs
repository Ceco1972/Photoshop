using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models
{
    public class ContestViewModel
    {
        //public ContestViewModel(Contest contest)
        //{
        //    this.Title = contest.Title;
        //    this.Category = contest.Category;
        //}

        public string Title { get; set; }
        public string Category { get; set; }
    }
}
