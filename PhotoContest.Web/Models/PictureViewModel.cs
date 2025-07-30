using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models
{
    public class PictureViewModel
    {
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public int ContestId { get; set; }
    }
}
