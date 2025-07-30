using PhotoContest.Common.Exceptions;
using PhotoContest.Models.Models.eNums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Web.Models
{
    public class CreateContestModel
    {
        private DateTime dueDate;
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public ContestType ContestType { get; set; }
        [Required]
        public DateTime FirstPhaseEnd { get; set; }
        [Required]
        public DateTime DueDate
        {
            get
            {
                return this.dueDate;
            }
            set
            {
                if (value < FirstPhaseEnd.AddHours(1) || value > FirstPhaseEnd.AddDays(1))
                {
                    throw new UnauthorisedOperationsException("End date cannot be more that 1 day after First phase ends.");
                }
                this.dueDate = value;
            }
        }
    }
}
