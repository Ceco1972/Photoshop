using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Models.Models.eNums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Service.Contracts
{
    public interface IContestService
    {
        public List<Vote> ReturnAllVotes();

        IEnumerable<ContestDTO> GetContests();
        IEnumerable<ContestDTO> ReturnAll();
        IEnumerable<ContestDTO> GetByTitle(string title);
        ContestDTO GetById(int id);
        ContestDTO GetContestByTitle(string title);
        IEnumerable<ContestDTO> GetByCategory(string category);
        IEnumerable<ContestDTO> GetByContestType(ContestType contestType);
        IEnumerable<ContestDTO> GetByPhase(Phase phase);                 
        IEnumerable<ContestDTO> ContestsInPh1(User user);
        IEnumerable<ContestDTO> ContestsInPh2(User user);
        Task Create(ContestDTO contest, User user);
        void ChangeStatus();
        IEnumerable<ContestDTO> FinishedContests(User user);
        IEnumerable<ContestDTO> Get(ContestQueryParameters contestQueryParameters);
        IEnumerable<ContestDTO> UserContests(string username);
        IEnumerable<ContestDTO> UserFinishedContests(string username);
        IEnumerable<ContestDTO> UserOpenedContests(string username);
        IEnumerable<ContestDTO> UserVotingContests(string username);
        IEnumerable<ContestDTO> FinishedContest();
        Task<ContestDTO> FinishedContestWinners(int id);

    }
}
