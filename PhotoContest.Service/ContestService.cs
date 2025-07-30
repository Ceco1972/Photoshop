using PhotoContest.Common.Exceptions;
using PhotoContest.Models.DTOMapper;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Models.Models.eNums;
using PhotoContest.Repository.Contracts;
using PhotoContest.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace PhotoContest.Service
{
    public class ContestService : IContestService
    {
        private readonly IContestRepository _contestRepository;
        public ContestService(IContestRepository contestRepository)
        {
            _contestRepository = contestRepository;
        }

        public List<Vote> ReturnAllVotes()
        {
            return this._contestRepository.ReturnAllVotes();
        }
        public IEnumerable<ContestDTO> GetContests()
        {
            return this._contestRepository.GetAllContests().GetDTO();
        }

        public IEnumerable<ContestDTO> GetByTitle(string title)
        {
            return this._contestRepository.GetByTitle(title).GetDTO();
        }

        public IEnumerable<ContestDTO> GetByCategory(string category)
        {
            return this._contestRepository.GetByCategory(category).GetDTO();
        }
        public IEnumerable<ContestDTO> GetByContestType(ContestType contestType)
        {
            return this._contestRepository.GetByContestType(contestType).GetDTO();
        }
        public IEnumerable<ContestDTO> GetByPhase(Phase phase)
        {
            return this._contestRepository.GetByPhase(phase).GetDTO();
        }
        public IEnumerable<ContestDTO> UserFinishedContests(string username)
        {
            IEnumerable<Contest> userContests = this._contestRepository.UserContests(username);
            IEnumerable<Contest> contests = userContests.Where(c => c.Phase == Phase.Closed);
            return contests.GetDTO();
        }
        public IEnumerable<ContestDTO> UserOpenedContests(string username)
        {
            IEnumerable<Contest> userContests = this._contestRepository.UserContests(username);
            IEnumerable<Contest> contests = userContests.Where(c => c.Phase == Phase.Open);
            return contests.GetDTO();
        }
        public IEnumerable<ContestDTO> UserVotingContests(string username)
        {
            IEnumerable<Contest> userContests = this._contestRepository.UserContests(username);
            IEnumerable<Contest> contests = userContests.Where(c => c.Phase == Phase.Voting);
            return contests.GetDTO();
        }
        public void ChangeStatus()
        {
            this._contestRepository.ChangeStatus();
        }
        public async Task Create(ContestDTO contestDTO, User user)
        {
            if (user.IsOrganizer == false)
            {
                throw new AuthorisationException("Only organisers are authorised");
            }
            var contest = contestDTO.GetContest();
            await this._contestRepository.Create(contest);
        }
        public IEnumerable<ContestDTO> ContestsInPh1(User user)
        {
            if (user.IsOrganizer == false)
            {
                throw new AuthorisationException("Only organisers are authorised");
            }
            var contest = this._contestRepository.ContestsInPh1();

            return contest.GetDTO();
        }
        public IEnumerable<ContestDTO> ContestsInPh2(User user)
        {
            if (user.IsOrganizer == false)
            {
                throw new AuthorisationException("Only organisers are authorised");
            }
            var contest = this._contestRepository.ContestsInPh2();
            return contest.GetDTO();
        }
        public IEnumerable<ContestDTO> FinishedContest()
        {
            var contest = this._contestRepository.FinishedContests();
            return contest.GetDTO();
        }
        public async Task<ContestDTO> FinishedContestWinners(int id)
        {
            var contests = await this._contestRepository.FinishedContestWinners(id);
            return contests.GetDTO();
        }
        public IEnumerable<ContestDTO> FinishedContests(User user)
        {
            if (user.IsOrganizer == false)
            {
                throw new AuthorisationException("Only organisers are authorised");
            }
            var contests =  this._contestRepository.FinishedContests();
            return contests.GetDTO();
        }
        public IEnumerable<ContestDTO> Get(ContestQueryParameters contestQueryParameters)
        {
            return this._contestRepository.Get(contestQueryParameters).GetDTO();
        }
        public ContestDTO GetById(int id)
        {
            var contest = this._contestRepository.GetById(id);
            return contest.GetDTO();
        }
        public ContestDTO GetContestByTitle(string title)
        {
            return this._contestRepository.GetContestByTitle(title).GetDTO();
        }
        public IEnumerable<ContestDTO> UserContests(string username)
        {
            var contests = this._contestRepository.UserContests(username);
            return contests.GetDTO();
        }

        public IEnumerable<ContestDTO> ReturnAll()
        {
            return this._contestRepository.ReturnAll().GetDTO();
        }
    }
}
