using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Models.Models;

namespace PhotoContest.DataBase.Config
{
    internal class RewardConfig : IEntityTypeConfiguration<Reward>
    {
        public void Configure(EntityTypeBuilder<Reward> builder)
        {
            //builder.HasOne(u => u.User)
            //    .WithMany(r => r.Rewards)
            //    .HasForeignKey(u => u.Id);

            //builder.HasOne(c => c.Contest)
            //    .WithMany(r => r.)

        }
    }
}