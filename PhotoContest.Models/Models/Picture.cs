using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Models.Models
{

    public class Picture
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { set; get; }
        //[Required]

        public string Story { set; get; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
        public virtual User Author { set; get; }
        public int ContestId { get; set; }
        public virtual Contest Contest { set; get; }
        public int Rating { get; set; }
        public int VoteId { get; set; }
        public Vote Vote { get; set; }
        public string ImageFile { set; get; }
        public bool IsVoted { get; set; }
        
        
    }

}
