using Microsoft.EntityFrameworkCore;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Models.Models.eNums;
using PhotoContest.Repository.Contracts;
using PhotoContest.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Repository
{
    public class ContestRepository : IContestRepository
    {
        private readonly ApplicationDbContext _context;

        public ContestRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public List<Vote> ReturnAllVotes()
        {
            return this._context.Votes.ToList();
        }
        private IEnumerable<Contest> ContestQuery
        {
            get
            {
                return this._context.Contests;
            }

        }
        public IEnumerable<Contest> GetAllContests()
        {
            return this._context
                .Contests
                .Include(c => c.Pictures)
                .Where(c => c.Phase == Phase.Open);
        }

        public IEnumerable<Contest> ReturnAll()
        {
            return this.ContestQuery;
        }
        public IEnumerable<Contest> GetByTitle(string title)
        {
            return this._context.Contests
                          .Where(p => p.Title == title)
                          .OrderBy(p => p.Title)
                          .Include(c => c.Pictures)
                          .ThenInclude(p => p.Vote);
        }
        public IEnumerable<Contest> GetByCategory(string category)
        {
            IQueryable<Contest> result = this._context.Contests
                                    .Where(c => c.Category.Contains(category))
                                    .OrderBy(c => c.Category);
            return result;
        }
        public IEnumerable<Contest> GetByContestType(ContestType contestType)
        {
            IQueryable<Contest> result = this._context.Contests
                                    .Where(c => c.ContestType == contestType)
                                    .OrderBy(c => c.ContestType);
            return result;

        }
        public IEnumerable<Contest> GetByPhase(Phase phase)
        {
            IQueryable<Contest> result = this._context.Contests
                                    .Where(c => c.Phase == phase)
                                    .OrderBy(c => c.Phase);
            return result;
        }
        public IEnumerable<Contest> Get(ContestQueryParameters contestQueryParameters)
        {
            string title = !string.IsNullOrEmpty(contestQueryParameters.Title) ? contestQueryParameters.Title.ToLowerInvariant() : string.Empty;
            string category = !string.IsNullOrEmpty(contestQueryParameters.Category) ? contestQueryParameters.Category.ToLowerInvariant() : string.Empty;
            //string dueDate = !string.IsNullOrEmpty(contestQueryParameters.DueDate.ToString()) ? contestQueryParameters.DueDate.ToString().ToLowerInvariant() : string.Empty;
            //string firstPhaseEnd = !string.IsNullOrEmpty(contestQueryParameters.FirstPhaseEnd.ToString()) ? contestQueryParameters.FirstPhaseEnd.ToString().ToLowerInvariant() : string.Empty;
            string sortCriteria = !string.IsNullOrEmpty(contestQueryParameters.SortBy) ? contestQueryParameters.SortBy.ToLowerInvariant() : string.Empty;
            string sortOder = !string.IsNullOrEmpty(contestQueryParameters.SortOrder) ? contestQueryParameters.SortOrder.ToLowerInvariant() : string.Empty;

            IQueryable<Contest> result = this.GetContests();

            result = FilterByTitle(result, title);
            result = FilterByCategory(result, category);
            //result = FilterByDueDate(result, dueDate);
            //result = FilterByfirstPhaseEnd(result, firstPhaseEnd);
            result = SortBy(result, sortCriteria);
            result = Order(result, sortOder);
            return result;
        }
        private static IQueryable<Contest> SortBy(IQueryable<Contest> contests, string sortCriteria)
        {
            switch (sortCriteria)
            {
                case "title":
                    return contests.OrderBy(contest => contest.Title);
                case "category":
                    return contests.OrderBy(contest => contest.Category);
                //case "duedate":
                //    return contests.OrderBy(contest => contest.DueDate);
                //case "firstPhaseEnd":
                //    return contests.OrderBy(contest => contest.FirstPhaseEnd);
                default:
                    return contests;
            }
        }
        private static IQueryable<Contest> Order(IQueryable<Contest> contests, string sortOrder)
        {
            return (sortOrder == "desc") ? contests.Reverse() : contests;
        }


        private static IQueryable<Contest> FilterByfirstPhaseEnd(IQueryable<Contest> result, string firstPhaseEnd)
        {
            return result
                .Where(contest => contest.DueDate.ToString().Contains(firstPhaseEnd));
        }
        private static IQueryable<Contest> FilterByDueDate(IQueryable<Contest> result, string duedate)
        {
            return result
                .Where(contest => contest.DueDate.ToString().Contains(duedate));
        }

        private static IQueryable<Contest> FilterByTitle(IQueryable<Contest> result, string title)
        {
            return result
                .Where(contest => contest.Title.Contains(title));
        }
        private static IQueryable<Contest> FilterByCategory(IQueryable<Contest> result, string category)
        {
            return result
                .Where(contest => contest.Category.Contains(category));
        }

        private static IQueryable<Contest> FilterByPhase(IQueryable<Contest> result, Phase phase)
        {
            return result
                 .Where(contest => contest.Phase == phase);
        }
        public static IQueryable<Contest> FilterByContestType(IQueryable<Contest> results, ContestType contestType)
        {
            return results
                .Where(contest => contest.ContestType == contestType);
        }
        public async Task<Contest> Create(Contest contest)
        {
            this._context.Contests.Add(contest);
            await this.Save();
            return contest;
        }
        public async Task Update(Contest contest)
        {
            this._context
              .Entry(contest)
              .State = EntityState.Modified;
            await this.Save();
        }


        public async Task ChangeStatus()
        {
            foreach (var contest in this._context.Contests)
            {
                if (contest.FirstPhaseEnd > DateTime.Now)
                {
                    contest.Phase = Models.Models.eNums.Phase.Open;
                }
                if (contest.FirstPhaseEnd < DateTime.Now && contest.DueDate >= DateTime.Now)
                {
                    contest.Phase = Models.Models.eNums.Phase.Voting;
                }
                if (contest.DueDate < DateTime.Now)
                {
                    if (contest.Phase == Phase.Voting || contest.Phase == Phase.Open)
                    {
                        contest.Phase = Phase.Closed;

                        var cont = this.GetById(contest.Id);
                        var pictures = cont.Pictures.OrderByDescending(p => p.Rating).ToList();
                        bool sharedFirst = false;
                        bool sharedSecond = false;
                        bool sharedThird = false;
                        int picCount = pictures.Count;
                        if (picCount > 0)
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                if (pictures[i].Rating != 0)
                                {
                                    //first place
                                    if (i == 0 && i + 1 < pictures.Count())
                                    {
                                        if (pictures[i].Rating == pictures[i + 1].Rating)
                                        {
                                            sharedFirst = true;
                                            var user = pictures[i].Author;
                                            var user1 = pictures[i + 1].Author;
                                            user.Score += 40;
                                            user1.Score += 40;
                                        }
                                        if (pictures[i].Rating > pictures[i + 1].Rating)
                                        {
                                            sharedFirst = false;
                                            var user = pictures[i].Author;
                                            user.Score += 50;
                                        }
                                    }
                                    //second place if first is not shared
                                    if (i == 1 && sharedFirst == false && i + 1 < pictures.Count)
                                    {
                                        if (pictures[i].Rating == pictures[i + 1].Rating)
                                        {
                                            sharedSecond = true;
                                            var user = pictures[i].Author;
                                            var user1 = pictures[i + 1].Author;
                                            user.Score += 25;
                                            user1.Score += 25;
                                        }
                                        if (pictures[i].Rating > pictures[i + 1].Rating)
                                        {
                                            sharedSecond = false;
                                            var user = pictures[i].Author;
                                            user.Score += 35;
                                        }
                                    }
                                    //second place if first IS shared
                                    if (i == 2 && sharedFirst == true && i + 1 < pictures.Count)
                                    {
                                        if (pictures[i].Rating == pictures[i + 1].Rating)
                                        {
                                            sharedSecond = true;
                                            var user = pictures[i].Author;
                                            var user1 = pictures[i + 1].Author;
                                            user.Score += 25;
                                            user1.Score += 25;
                                        }
                                        if (pictures[i].Rating > pictures[i + 1].Rating)
                                        {
                                            sharedSecond = false;
                                            var user = pictures[i].Author;
                                            user.Score += 35;
                                        }
                                    }
                                    //third place if first and second are not shared
                                    if (i == 2 && sharedFirst == false && sharedSecond == false && i + 1 < pictures.Count)
                                    {
                                        if (pictures[i].Rating == pictures[i + 1].Rating)
                                        {
                                            sharedThird = true;
                                            var user = pictures[i].Author;
                                            var user1 = pictures[i + 1].Author;
                                            user.Score += 10;
                                            user1.Score += 10;
                                        }
                                        if (pictures[i].Rating > pictures[i + 1].Rating)
                                        {
                                            sharedThird = false;
                                            var user = pictures[i].Author;
                                            user.Score += 20;
                                        }
                                    }
                                    //third place if first or second is shared
                                    if (i == 3 && sharedFirst == true || sharedSecond == true && i + 1 < pictures.Count)
                                    {
                                        if (pictures[i].Rating == pictures[i + 1].Rating)
                                        {
                                            sharedThird = true;
                                            var user = pictures[i].Author;
                                            var user1 = pictures[i + 1].Author;
                                            user.Score += 10;
                                            user1.Score += 10;
                                        }
                                        if (pictures[i].Rating > pictures[i + 1].Rating)
                                        {
                                            sharedThird = false;
                                            var user = pictures[i].Author;
                                            user.Score += 20;
                                        }
                                    }
                                    //third if first and second are shared
                                    if (i == 4 && sharedFirst == true && sharedSecond == true && i + 1 < pictures.Count)
                                    {
                                        if (pictures[i].Rating == pictures[i + 1].Rating)
                                        {
                                            sharedThird = true;
                                            var user = pictures[i].Author;
                                            var user1 = pictures[i + 1].Author;
                                            user.Score += 10;
                                            user1.Score += 10;
                                        }
                                        if (pictures[i].Rating > pictures[i + 1].Rating)
                                        {
                                            sharedThird = false;
                                            var user = pictures[i].Author;
                                            user.Score += 20;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            this._context.SaveChanges();
        }
        public IEnumerable<Contest> ContestsInPh1()
        {
            return this._context
                .Contests
                .Where(contest => contest.Phase == Models.Models.eNums.Phase.Open);
        }
        public IEnumerable<Contest> ContestsInPh2()
        {
            return this._context
                .Contests
                .Where(contest => contest.Phase == Models.Models.eNums.Phase.Voting);
        }
        public IEnumerable<Contest> FinishedContests()
        {
            var contests = this.GetContests();
            return contests.Where(c => c.Phase == Phase.Closed);
        }
        public async Task<Contest> FinishedContestWinners(int id)
        {
            await this.ChangeStatus();
            //var contest = await this.GetById(id);
            var contests = this._context
            .Contests
            .Include(p => p.Pictures)
            .ThenInclude(p => p.Vote)
            .Include(c => c.Pictures)
            .ThenInclude(p => p.Author)
            .Where(c => c.Id == id)
            .FirstOrDefault();
            bool sharedFirst = false;
            bool sharedSecond = false;
            bool sharedThird = false;
            int places = 3;
            var pictures = contests.Pictures.ToList();
            var picCount = pictures.Count;
            if (pictures.Any())
            {

                for (int i = 0; i < picCount - 1; i++)
                {
                    if (i == 0 && i + 1 < picCount)
                    {
                        if (pictures[i].Rating == pictures[i + 1].Rating)
                        {
                            sharedFirst = true;
                            //var user = pictures[i].Author;
                            //var user1 = pictures[i + 1].Author;
                            //user.Score += 40;
                            //user1.Score += 40;
                            //await this._userRepository.UpdateUser(user);
                            //await this._userRepository.UpdateUser(user1);
                        }
                        if (pictures[i].Rating > pictures[i + 1].Rating)
                        {
                            sharedFirst = false;
                            //var user = pictures[i].Author;
                            //user.Score += 50;
                            //await this._userRepository.UpdateUser(user);
                        }
                    }
                    if (i == 1 && sharedFirst == false && i + 1 < pictures.Count)
                    {
                        if (pictures[i].Rating == pictures[i + 1].Rating)
                        {
                            sharedSecond = true;
                            //var user = pictures[i].Author;
                            //var user1 = pictures[i + 1].Author;
                            //user.Score += 25;
                            //user1.Score += 25;
                            //await this._userRepository.UpdateUser(user);
                            //await this._userRepository.UpdateUser(user1);
                        }
                        if (pictures[i].Rating > pictures[i + 1].Rating)
                        {
                            sharedSecond = false;
                            //var user = pictures[i].Author;
                            //user.Score += 35;
                            //await this._userRepository.UpdateUser(user);
                        }
                    }
                    if (i == 2 && sharedFirst == true && i + 1 < pictures.Count)
                    {
                        if (pictures[i].Rating == pictures[i + 1].Rating)
                        {
                            sharedSecond = true;
                            //var user = pictures[i].Author;
                            //var user1 = pictures[i + 1].Author;
                            //user.Score += 10;
                            //user1.Score += 10;
                            //await this._userRepository.UpdateUser(user);
                            //await this._userRepository.UpdateUser(user1);
                        }
                        if (pictures[i].Rating > pictures[i + 1].Rating)
                        {
                            sharedSecond = false;
                            //var user = pictures[i].Author;
                            //user.Score += 20;
                            //await this._userRepository.UpdateUser(user);
                        }
                    }
                    if (i == 2 && sharedFirst == false && sharedSecond == false && i + 1 < pictures.Count)
                    {
                        if (pictures[i].Rating == pictures[i + 1].Rating)
                        {
                            sharedThird = true;
                        }
                        if (pictures[i].Rating > pictures[i + 1].Rating)
                        {
                            sharedThird = false;
                        }
                    }
                    if (i == 3 && sharedFirst == true && sharedSecond == false && i + 1 < pictures.Count)
                    {
                        if (pictures[i].Rating == pictures[i + 1].Rating)
                        {
                            sharedThird = true;
                        }
                        if (pictures[i].Rating > pictures[i + 1].Rating)
                        {
                            sharedThird = false;
                        }
                    }
                    if (i == 3 && sharedFirst == false && sharedSecond == true && i + 1 < pictures.Count)
                    {
                        if (pictures[i].Rating == pictures[i + 1].Rating)
                        {
                            sharedThird = true;
                        }
                        if (pictures[i].Rating > pictures[i + 1].Rating)
                        {
                            sharedThird = false;
                        }
                    }
                    if (i == 4 && sharedFirst == true && sharedSecond == true && i + 1 < pictures.Count)
                    {
                        if (pictures[i].Rating == pictures[i + 1].Rating)
                        {
                            sharedThird = true;
                        }
                        if (pictures[i].Rating > pictures[i + 1].Rating)
                        {
                            sharedThird = false;
                        }
                    }

                }
                if (!sharedFirst && !sharedSecond && !sharedThird)
                {
                    places = 3;
                }
                if (sharedFirst && !sharedSecond && !sharedThird)
                {
                    places = 4;
                }
                if (!sharedFirst && sharedSecond && !sharedThird)
                {
                    places = 4;
                }
                if (!sharedFirst && !sharedSecond && sharedThird)
                {
                    places = 4;
                }
                if (sharedFirst && sharedSecond && !sharedThird)
                {
                    places = 5;
                }
                if (!sharedFirst && sharedSecond && sharedThird)
                {
                    places = 5;
                }
                if (sharedFirst && !sharedSecond && sharedThird)
                {
                    places = 5;
                }
                if (sharedFirst && sharedSecond && sharedThird)
                {
                    places = 6;
                }
            }
            var pics = pictures.ToList().Take(places);
            contests.Pictures = pics;
            return contests;
        }



        public Contest GetById(int id)
        {
            return this._context
                .Contests
                .Where(c => c.Id == id)
                .Include(c => c.Pictures)
                .ThenInclude(p => p.Author)
                .Include(c => c.Pictures)
                .ThenInclude(p => p.Vote)
                .ThenInclude(v => v.User)
                .FirstOrDefault();
        }
        public IQueryable<Contest> GetContests()
        {
            return this._context.Contests
                .Include(c => c.Pictures)
                    .ThenInclude(p => p.Vote);
        }
        public Contest GetContestByTitle(string title)
        {
            return this._context
                .Contests
                .Where(c => c.Title.Equals(title))
                .Include(c => c.Pictures)
                .ThenInclude(p => p.Vote)
                .FirstOrDefault();
        }
        public IEnumerable<Contest> UserContests(string username)
        {
            IQueryable<Picture> userPics = this._context.Pictures.Where(p => p.Author.Username == username);
            IEnumerable<Contest> userContests = userPics.Select(p => p.Contest);

            return userContests;

        }
        private async Task Save()
        {
            await this._context.SaveChangesAsync();
        }
    }

}
