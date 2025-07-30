using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models
{
    public class ContestTypeModel
    {
        public ContestTypeModel()
        {
            TypeList = new List<SelectListItem>();
        }
        [Display(Name = "Contest Types")]
        public int TypeId { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }
    }
}
