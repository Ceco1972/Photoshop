using Microsoft.AspNetCore.Http;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Models.DTOs
{
    public class PictureDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int Rating { get; set; }
        public string ImageFile { set; get; }
        public int VoteId { get; set; }
        public VoteDTO Vote { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ContestId { get; set; }
        public bool IsVoted { get; set; }


    }
}
        //[Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        //[StringLength(32, MinimumLength = 4, ErrorMessage = "Value for {0} must be {1} to {2}")]

        //public string Title { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        //[StringLength(150, MinimumLength = 10, ErrorMessage = "Value for {0} must be {1} to {2}")]

        //public string Description { get; set; }
