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
    public class VotePictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasOne(p => p.Vote)
                .WithOne(v => v.Picture)
                .HasForeignKey<Picture>(p => p.VoteId);

        }
    }
}
