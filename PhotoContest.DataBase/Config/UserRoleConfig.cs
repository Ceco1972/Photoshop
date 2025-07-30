using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoContest.Models.Models;

namespace PhotoContest.DataBase.Config
{
    internal class UserRoleConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Role)
                 .WithMany(r => r.Users);
        }
    }
}