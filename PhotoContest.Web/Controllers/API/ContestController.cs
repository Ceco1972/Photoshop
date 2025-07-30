using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Common.Exceptions;
using PhotoContest.Common.Helpers;
using PhotoContest.Models.DTOMapper;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Models.Models.eNums;
using PhotoContest.Service.Contracts;
using System.Collections.Generic;

namespace PhotoContest.Web.Controllers.API

{
    [ApiController]
    [Route("api/[controller]")]
    public class ContestController : ControllerBase
    {
        private readonly IContestService contestService;
        private readonly AuthorisationHelper authorisationHelper;
        public ContestController(IContestService contestService, AuthorisationHelper authorisationHelper)
        {
            this.contestService = contestService;
            this.authorisationHelper = authorisationHelper;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var contests = this.contestService.ReturnAll();
            return this.StatusCode(StatusCodes.Status200OK, contests);
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle([FromHeader] string username, [FromQuery] string title)
        {
            try
            {
                User user = authorisationHelper.TryGetUser(username);
                if (user == null)
                {
                    throw new AuthenticationException("This is not existing user!");
                }
                var contests = this.contestService.GetByTitle(title);
                return this.StatusCode(StatusCodes.Status200OK, contests);
            }
            catch (AuthorisationException e)
            {

                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);

            }



        }

        [HttpGet("GetByCategory")]
        public IActionResult GetByCategory([FromQuery] string category, [FromHeader] string username)
        {
            try
            {
                User user = authorisationHelper.TryGetUser(username);
                if (user == null)
                {
                    throw new AuthenticationException("This is not existing user!");
                }
                var contests = this.contestService.GetByCategory(category);
                return this.StatusCode(StatusCodes.Status200OK, contests);
            }
            catch (AuthorisationException e)
            {

                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);

            }

        }
        [HttpGet("GetByContestType")]
        public IActionResult GetByContestType([FromQuery] ContestType contestType, [FromHeader] string username)
        {
            try
            {
                User user = authorisationHelper.TryGetUser(username);
                if (user == null)
                {
                    throw new AuthenticationException("This is not existing user!");
                }
                var contests = this.contestService.GetByContestType(contestType);
                return this.StatusCode(StatusCodes.Status200OK, contests);
            }
            catch (AuthorisationException e)
            {

                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);

            }

        }
        [HttpGet("GetByPhase")]
        public IActionResult GetByPhase([FromQuery] Phase phase, [FromHeader] string username)
        {
            try
            {
                User user = authorisationHelper.TryGetUser(username);
                if (user == null)
                {
                    throw new AuthenticationException("This is not existing user!");
                }
                var contests = this.contestService.GetByPhase(phase);
                return this.StatusCode(StatusCodes.Status200OK, contests);
            }
            catch (AuthorisationException e)
            {

                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);

            }

        }

        //Organisers can view Contests, which are in Phase I
        [HttpGet("OpenContests")]
        public IActionResult GetOpenContests([FromHeader] string username)
        {
            try
            {
                User user = authorisationHelper.TryGetUser(username);
                if (user == null)
                {
                    throw new AuthenticationException("This is not existing user!");
                }
                var openContests = this.contestService.ContestsInPh1(user);

                return this.StatusCode(StatusCodes.Status200OK, openContests);

            }
            catch (AuthorisationException e)
            {

                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);

            }

        }
        //Organisers can view Contests, which are in Phase II
        [HttpGet("VoteContests")]
        public IActionResult GetVoteContests([FromHeader] string username)
        {
            try
            {
                User user = this.authorisationHelper.TryGetUser(username);
                if (user == null)
                {
                    throw new AuthenticationException("This is not existing user!");
                }
                var voteContests = this.contestService.ContestsInPh2(user);
                return this.StatusCode(StatusCodes.Status200OK, voteContests);
            }
            catch (AuthorisationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);

            }

        }
        //Organisers can view Contests, which are finished

        [HttpGet("FinishedContests")]
        public IActionResult GetFinishedContests([FromHeader] string username)
        {
            try
            {
                User user = this.authorisationHelper.TryGetUser(username);
                if (user == null)
                {
                    throw new AuthenticationException("This is not existing user!");
                }
                var finishedContests = this.contestService.FinishedContests(user);
                return this.StatusCode(StatusCodes.Status200OK, finishedContests);
            }
            catch (AuthorisationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);

            }

        }
        [HttpGet("GetByQueryParameters")]
        public IActionResult Get([FromQuery] ContestQueryParameters queryParameters)
        {
            IEnumerable<ContestDTO> result = this.contestService.Get(queryParameters);
            return this.StatusCode(StatusCodes.Status200OK, result);
        }


        //Organisers can setup new Contests
        [HttpPost("Create")]
        public IActionResult Create([FromBody] ContestDTO contestDto, [FromHeader] string username)
        {
            try
            {
                User user = this.authorisationHelper.TryGetUser(username);
                if (user == null)
                {
                    throw new AuthenticationException("This is not existing user!");
                }
                var contest = contestDto.GetContest();
                contest.OrganizerId = user.Id;
                this.contestService.Create(contestDto, user);
                return this.StatusCode(StatusCodes.Status201Created, contest);
            }
            catch (AuthorisationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }


        }
        [HttpPut("UpdateStatus")]
        public IActionResult UpdateStatus()
        {
            this.contestService.ChangeStatus();
            return this.StatusCode(StatusCodes.Status202Accepted);
        }


    }
}
