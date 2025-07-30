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
    public class PictureVoteConfig : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(v => v.Picture)
                .WithOne(p => p.Vote)
                .HasForeignKey<Vote>(v => v.PictureId);

        }
    }
}
