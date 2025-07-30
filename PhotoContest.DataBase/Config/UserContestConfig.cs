using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.DataBase.Config
{
    public class UserContestConfig : IEntityTypeConfiguration<UserContest>
    {
        public void Configure(EntityTypeBuilder<UserContest> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.ContestId });

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserContest)
                .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.Contest)
                .WithMany(c => c.Participants)
                .HasForeignKey(uc => uc.ContestId);

        }
    }

}
