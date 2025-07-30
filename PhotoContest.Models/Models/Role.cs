using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoContest.Models.Models.eNums;
namespace PhotoContest.Models.Models
{
    public class Role
    {
        public int Id { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public RoleType RoleType { get; set; }
    }
}
