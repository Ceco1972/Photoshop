using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PhotoContest.Common.Exceptions;
using PhotoContest.Common.Helpers;
using PhotoContest.Models.DTOMapper;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.DTOs.ListModels;
using PhotoContest.Models.Models;
using PhotoContest.Service.Contracts;
using PhotoContest.Web.Models;
using PhotoContest.Web.Models.ViewModelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AuthorisationHelper _authorisationHelper;
        private readonly IUserService _userService;
        private readonly IContestService _contestService;

        public UserController(AuthorisationHelper authorisationHelper, IUserService userService, IContestService contestService)
        {
            this._authorisationHelper = authorisationHelper;
            this._userService = userService;
            this._contestService = contestService;
        }

        public IActionResult GetUsers()
        {
            IEnumerable<UserDTO> users = this._userService.GetAllUsers();
            var userList = new UserIndex
            {
                UserList = users
            };
            return this.View(model: userList);
        }
        public IActionResult LoginUser()
        {
            var viewModel = new LoginViewModel();
            return this.View(model: viewModel);
        }

        [HttpPost]
        public IActionResult LoginUser(LoginViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model: viewModel);
            }
            try
            {
                var user = this._authorisationHelper.TryGetUser(viewModel.Username, viewModel.Password);
                var userDTO = user.GetDTO();

                this.HttpContext.Session.SetString("CurrentUser", user.Username);

                TempData["username"] = viewModel.Username;
                TempData.Keep();

                if (user.IsOrganizer == true)
                {
                    this.HttpContext.Session.SetString("IsOrganiser", string.Empty);
                }
                if (user.RoleId == 1 || user.IsPhotoJunkey == true)
                {
                    this.HttpContext.Session.SetString("IsPhotoJunkey", string.Empty);
                }

                return this.RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            catch (AuthenticationException)
            {
                //this.ModelState.AddModelError("Username", e.Message);
                this.ModelState.AddModelError("Password", "Invalid credentials!");
                return this.View(model: viewModel);
            }
            catch (ArgumentException)
            {
                this.ModelState.AddModelError("Password", "Invalid credentials!");
                return this.View(model: viewModel);
            }
        }

        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("CurrentUser");
            this.HttpContext.Session.Clear();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel();
            return this.View(model: viewModel);
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(model: viewModel);
            }
            if (this._userService.Exist(viewModel.Username))
            {
                this.ModelState.AddModelError("Username", "User with same username already exists.");
                return this.View(model: viewModel);
            }
            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                return this.View(model: viewModel);
            }

            User user = viewModel.GetUser();
            this._userService.InsertUser(user);

            return this.RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        public IActionResult Details()
        {
            //var user = this._userService.GetUser(id);
            string username = TempData["username"].ToString();
            TempData.Keep();
            var user = this._authorisationHelper.TryGetUser(username);
            var viewModel = new UserViewModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Score = user.Score,
                CreatedOn = user.CreatedOn,
                OpenContests =  this._contestService.UserOpenedContests(user.Username),
                ContestsInVotePhase =  this._contestService.UserVotingContests(user.Username),
                FinishedContests =  this._contestService.UserFinishedContests(user.Username),
            };
            return this.View(model: viewModel);
            //var userDto = user.GetDTO();
            //return this.View(model: userDto);
        }
        public IActionResult AuthorDetails(int id)
        {
            var user = this._userService.GetUser(id);
            var viewModel = new UserViewModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Score = user.Score,
                CreatedOn = user.CreatedOn,
                OpenContests =  this._contestService.UserOpenedContests(user.Username),
                ContestsInVotePhase =  this._contestService.UserVotingContests(user.Username),
                FinishedContests =  this._contestService.UserFinishedContests(user.Username),
            };
            return this.View(model: viewModel);
        }

        public IActionResult DetailsUser(int id)
        {
            var user = this._userService.GetUser(id);
            var viewModel = new UserViewModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Score = user.Score,
                CreatedOn = user.CreatedOn,
                OpenContests = this._contestService.UserOpenedContests(user.Username),
                ContestsInVotePhase = this._contestService.UserVotingContests(user.Username),
                FinishedContests = this._contestService.UserFinishedContests(user.Username),
            };
            return this.View(model: viewModel);
        }
        public IActionResult FinishedContest()
        {
            string username = TempData["username"].ToString();
            TempData.Keep();
            var finished =  this._contestService.UserFinishedContests(username);
            var contests = new ContestIndex
            {
                ContestList = finished
            };
            return this.View(model: contests);
        }
        public IActionResult VoteContests()
        {
            string username = TempData["username"].ToString();
            TempData.Keep();
            var finished =  this._contestService.UserVotingContests(username);
            var contests = new ContestIndex
            {
                ContestList = finished
            };
            return this.View(model: contests);
        }
        public IActionResult OpenContests()
        {
            string username = TempData["username"].ToString();
            TempData.Keep();
            var finished =  this._contestService.UserOpenedContests(username);
            var contests = new ContestIndex
            {
                ContestList = finished
            };
            return this.View(model: contests);
        }
    }
}

