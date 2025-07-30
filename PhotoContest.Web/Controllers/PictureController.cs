using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Common.Helpers;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Service;
using PhotoContest.Service.Contracts;
using PhotoContest.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    public class PictureController : Controller
    {
        private readonly IPictureService _pictureService;
        private readonly IWebHostEnvironment _webHostEnvironment; 
        private readonly AuthorisationHelper _authHelper;

        public PictureController(IPictureService pictureService, IWebHostEnvironment webHostEnvironment, AuthorisationHelper authHelper)
        {
            this._pictureService = pictureService;
            _webHostEnvironment = webHostEnvironment;
            _authHelper = authHelper;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult UploadPicture(int id)
        {
            var viewModel = new PictureViewModel
            {
                ContestId = id
            };
            return View(model:viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPicture(PictureViewModel viewModel, int id)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadModel(viewModel);
                string username = TempData["username"].ToString();
                TempData.Keep();
                User author = this._authHelper.TryGetUser(username);
                bool isUserPictures = this._pictureService.IsUserPictures(id, username);

                if (!isUserPictures)
                {
                    PictureDTO pictureDTO = new PictureDTO
                    {
                        Title = viewModel.Title,
                        Story = viewModel.Story,
                        CreatedOn = DateTime.Now,
                        AuthorId = author.Id,
                        ImageFile = uniqueFileName,
                        ContestId = id,
                    };
                    await this._pictureService.Insert(pictureDTO);
                }
                else this.HttpContext.Session.SetString("SecondPhoto", string.Empty);

            }


            return RedirectToAction(actionName: "Index", controllerName: "Contests");
        }
        [HttpPost]
        public async Task<IActionResult> VotePicture(int id, VotePictureModel voteModel)
        {
            var picture = this._pictureService.GetPictureById(id);
            string username = TempData["username"].ToString();
            TempData.Keep();
            User author = this._authHelper.TryGetUser(username);
            var voteDTO = new VoteDTO
            {
                PictureId = id,
                UserId = author.Id,
                ContestId = picture.ContestId,
                Comment = voteModel.Comment,
                Rating = voteModel.Rating,
            };
            await this._pictureService.Vote(voteDTO);
            return RedirectToAction(actionName: "Vote", controllerName: "Contests", new { @id = picture.ContestId });
        }
        [HttpPost]
        public async Task<IActionResult> WrongCategory(int id)
        {
            var picture = this._pictureService.GetPictureById(id);
            string username = TempData["username"].ToString();
            TempData.Keep();
            User author = this._authHelper.TryGetUser(username);
            var voteDTO = new VoteDTO
            {
                PictureId = id,
                UserId = author.Id,
                ContestId = picture.ContestId,
                Comment = "Wrong category!",
                Rating = 0,
            };
            await this._pictureService.Vote(voteDTO);
            return RedirectToAction(actionName: "Vote", controllerName: "Contests", new { @id = picture.ContestId });
        }
        private string UploadModel(PictureViewModel viewModel)
        {
            string uniqueFileName = null;
            if (viewModel.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.ImageFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}


//if (pictureViewModel.ImageFile != null)
//{

//    string username = TempData["username"].ToString();
//    //string contestTitle = TempData["contestTitle"].ToString();
//    //convert image to string and send to database
//    string contestTitle = pictureViewModel.ContestTitle;
//    string pictureTitle = pictureViewModel.Title;
//    string pictureStory = pictureViewModel.Story;
//    using (MemoryStream ms = new MemoryStream())
//    {
//        pictureViewModel.ImageFile.CopyTo(ms);
//        byte[] fileBytes = ms.ToArray();
//        string convertedPhoto = Convert.ToBase64String(fileBytes);

//        this.pictureService.Insert(convertedPhoto, username, contestTitle, pictureTitle, pictureStory);
//    }

//}