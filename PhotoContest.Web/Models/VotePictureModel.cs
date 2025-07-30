using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models
{
    public class VotePictureModel
    {
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
