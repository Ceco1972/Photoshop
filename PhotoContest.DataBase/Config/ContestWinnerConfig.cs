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
    public class ContestWinnerConfig : IEntityTypeConfiguration<ContestWinner>
    {
        public void Configure(EntityTypeBuilder<ContestWinner> builder)
        {
            builder.HasKey(cw => new { cw.ContestId, cw.WinnerId });

            builder.HasOne(cw => cw.Winner)
                .WithMany(w => w.WinnedContests)
                .HasForeignKey(cw => cw.WinnerId);

            builder.HasOne(cw => cw.Contest)
                .WithMany(c => c.Winners)
                .HasForeignKey(cw => cw.ContestId);
        }
    }
    
}
