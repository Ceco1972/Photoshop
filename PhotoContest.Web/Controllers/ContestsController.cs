using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Common.Helpers;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.DTOs.ListModels;
using PhotoContest.Models.Models;
using PhotoContest.Models.Models.eNums;
using PhotoContest.Service;
using PhotoContest.Service.Contracts;
using PhotoContest.Web.Models;
using PhotoContest.Web.Models.ViewModelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    public class ContestsController : Controller
    {
        private readonly IContestService _contestService;
        private readonly AuthorisationHelper _authHelper;
        private readonly IPictureService _pictureService;
        private readonly IUserService _userService;


        public ContestsController(IContestService contestService, AuthorisationHelper authHelper, IPictureService pictureService, IUserService userService)
        {
            this._contestService = contestService;
            this._authHelper = authHelper;
            this._pictureService = pictureService;
            this._userService = userService;
        }

        public IActionResult Index([FromQuery] ContestQueryParameters contestQueryParameters)
        {
            if (!this.ModelState.IsValid)
            {
                contestQueryParameters = new ContestQueryParameters();
            }

            this.ViewData["SortOrder"] = string.IsNullOrEmpty(contestQueryParameters.SortOrder) ? "desc" : "";

            IEnumerable<ContestDTO> openContests2 = this._contestService.Get(contestQueryParameters);
            var openContests = openContests2.Where(c => c.Phase == Phase.Open);


            var contests = new ContestIndex
            {
                ContestList = openContests
            };
            return this.View(model: contests);
        }


        public IActionResult ClosedUserContests()
        {
            string username = TempData["username"].ToString();
            TempData.Keep();

            IEnumerable<ContestDTO> userContests = this._contestService.UserFinishedContests(username);
            var contests = new ContestIndex
            {
                ContestList = userContests
            };
            return this.View(model: contests);
        }

        public IActionResult OpenAndVoteUserContests()
        {
            string username = TempData["username"].ToString();
            TempData.Keep();
            IEnumerable<ContestDTO> userContests = this._contestService.UserOpenedContests(username);
            var contests = new ContestIndex
            {
                ContestList = userContests
            };
            return this.View(model: contests);

        }
        public IActionResult SecondPhaseContests()
        {
            var user = this._authHelper.TryGetUser(this.TempData["username"].ToString());
            TempData.Keep();
            var viewModel = this._contestService.ContestsInPh2(user);
            return this.View(model: viewModel);

        }
        public IActionResult FinishedContests()
        {
            IEnumerable<ContestDTO> closedContests = this._contestService.GetByPhase(Phase.Closed);

            ContestIndex contests = new ContestIndex
            {
                ContestList = closedContests
            };
                       
            return this.View(model: contests);
        }

        public IActionResult FinishedContestsRated()
        {
            IEnumerable<ContestDTO> closedContests = this._contestService.GetByPhase(Phase.Closed).OrderBy(c => c.DueDate);

            ContestIndex contests = new ContestIndex
            {
                ContestList = closedContests
            };

            return this.View(model: contests);

        }
        public IActionResult VoteContests()
        {
            IEnumerable<ContestDTO> voteContests = this._contestService.GetByPhase(Phase.Voting);
            var contests = new ContestIndex
            {
                ContestList = voteContests
            };
            return this.View(model: contests);


        }
        public IActionResult CreateContest()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContest(CreateContestModel createContestModel)
        {
            if (ModelState.IsValid)
            {
                string username = TempData["username"].ToString();
                TempData.Keep();
                var user = this._authHelper.TryGetUser(username);
                var contestDTO = new ContestDTO
                {
                    Title = createContestModel.Title,
                    Category = createContestModel.Category,
                    ContestType = createContestModel.ContestType,
                    FirstPhaseEnd = createContestModel.FirstPhaseEnd,
                    DueDate = createContestModel.DueDate,
                };
                await this._contestService.Create(contestDTO, user);
            }
            return RedirectToAction(actionName: "Index", controllerName: "Contests");
        }

        public IActionResult Details([FromRoute] int id)
        {
            ContestDTO viewModel = this._contestService.GetById(id);
            string username = TempData["username"].ToString();
            TempData.Keep();
            // Checking if this user has already applied a picture in this contest
            if (_pictureService.IsUserPictures(id, username))
            {
                viewModel.IsUserContest = true;
            }

            return this.View(model: viewModel);
        }
        public async Task<IActionResult> DetailsRated([FromRoute] int id)
        {
            var viewModel = await this._contestService.FinishedContestWinners(id);
            return this.View(model: viewModel);
            //ContestDTO viewModel = this._contestService.GetById(id);
            //var votes = this._contestService.ReturnAllVotes();
            //var votesContest = votes.Where(v => v.ContestId == id);
            //foreach (var picture in viewModel.Pictures)
            //{
            //    foreach (var vote in votesContest)
            //    {
            //        if (vote.PictureId==picture.Id)
            //        {
            //            picture.Rating = vote.Rating;
            //        }
            //    }
            //}

            //var Pictures = viewModel.Pictures.ToArray();
            //var PicturesWinners = Pictures.OrderBy(p => p.Rating).Take(3).ToArray();

            //for (int i = 0; i < 3; i++)
            //{
            //    if (i==0)
            //    {
            //        PhotoContest.Models.Models.User user = PicturesWinners[i].Author;
            //        user.Score += 20;
            //        this._userService.UpdateUser(user);
            //    }
            //    if (i == 1)
            //    {
            //        PhotoContest.Models.Models.User user = PicturesWinners[i].Author;
            //        user.Score += 35;
            //        this._userService.UpdateUser(user);

            //    }
            //    if (i == 2)
            //    {
            //        PhotoContest.Models.Models.User user = PicturesWinners[i].Author;
            //        user.Score += 50;
            //        this._userService.UpdateUser(user);

            //    }
            //}
            //return this.View(model: viewModel);
        }




        //public IActionResult Details(int id)
        //{
        //    var viewModel = this._contestService.GetById(id);

        //    return this.View(model: viewModel);
        //    //This should be public we are showing the opened contest, but not to participate.
        //    //try
        //    //{
        //    //    string username = TempData["username"].ToString();
        //    //    TempData.Keep();
        //    //    var user = this._authHelper.TryGetUser(username);
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return this.NotFound();
        //    //}

        //}
        public IActionResult Vote(int id)
        {
            try
            {
                var viewModel = this._contestService.GetById(id);
                return this.View(model: viewModel);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        public IActionResult ModalTest()
        {
            return this.View();
        }


    }
}
