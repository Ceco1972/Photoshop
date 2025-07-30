using Microsoft.EntityFrameworkCore;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.DataBase
{
    public static class ModelBuilderExtensions
    {
        public static IEnumerable<Contest> Contests { get; }
        public static IEnumerable<Picture> Pictures { get; }
        public static IEnumerable<User> Users { get; }

        static ModelBuilderExtensions()
        {
            Contests = new List<Contest>
            {
                new Contest
                {
                    Id = 1,
                    Title = "First",
                    Phase = Models.Models.eNums.Phase.Voting
                },
                new Contest
                {
                    Id = 2,
                    Title = "Initial",
                    Phase = Models.Models.eNums.Phase.Open
                },
                new Contest
                {
                     Id = 3,
                     Title = "Second",
                     Phase = Models.Models.eNums.Phase.Open
                }
            };

            Pictures = new List<Picture>
            {
                new Picture
                {
                    Id = 1,
                    Title = "Vacation",
                    Story = "My tennis vacation"
                },
                new Picture
                {
                    Id = 2,
                    Title ="MyFirstPhoto",
                    Story = "Camera"
                },
                new Picture
                {
                    Id = 3,
                    Title = "FirstPhoto",
                    Story = "My photo"
                }
            };

            Users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "photodictator",
                    Password = "Password123!",
                    FirstName = "James",
                    LastName = "Pumphrey"

                },
                new User
                {
                    Id = 2,
                    Username = "Tosho",
                    Password = "123",
                    FirstName = "Todor",
                    LastName = "Toshev"
                }
            };


        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(Users);
            modelBuilder.Entity<Picture>().HasData(Pictures);
            modelBuilder.Entity<Contest>().HasData(Contests);
        }


    }
}
