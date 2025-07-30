using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Models.Models.eNums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Repository.Contracts
{
    public interface IContestRepository
    {
        public List<Vote> ReturnAllVotes();

        IEnumerable<Contest> ReturnAll();
        IEnumerable<Contest> GetAllContests();
        IEnumerable<Contest> GetByTitle(string title);
        Contest GetById(int id);
        Contest GetContestByTitle(string title);
        IEnumerable<Contest> GetByCategory(string category);
        IEnumerable<Contest> GetByContestType(ContestType contestType);

        IEnumerable<Contest> GetByPhase(Phase phase);

        Task<Contest> Create(Contest contest);
        Task Update(Contest contest);
        Task ChangeStatus();
        IEnumerable<Contest> ContestsInPh1();
        IEnumerable<Contest> ContestsInPh2();
        IEnumerable<Contest> FinishedContests();
        Task<Contest> FinishedContestWinners(int id);
        IEnumerable<Contest> Get(ContestQueryParameters contestQueryParameters);
        IEnumerable<Contest> UserContests(string username);








    }
}
