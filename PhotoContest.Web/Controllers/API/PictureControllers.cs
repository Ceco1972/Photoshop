using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Common.Exceptions;
using PhotoContest.Common.Helpers;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Service;
using PhotoContest.Service.Contracts;

namespace PhotoContest.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class PicturesControllers : ControllerBase
    {
        private readonly IPictureService _pictureService;
        private readonly IContestService _contestService;
        private readonly AuthorisationHelper _authorisationHelper;

        public PicturesControllers(IPictureService pictureService, AuthorisationHelper authorisationHelper, IContestService contestService)
        {
            _pictureService = pictureService;
            _authorisationHelper = authorisationHelper;
            _contestService = contestService;
        }

        [HttpGet("contest/{id}")]
        public IActionResult GetPictures([FromQuery] PictureQueryParameters filterParameters, int contestId)
        {
            var picture = this._pictureService.GetAllPictures(contestId,filterParameters);
            return this.StatusCode(StatusCodes.Status200OK, picture);
        }
        [HttpGet("id")]
        public IActionResult GetPictureById([FromRoute] int id)
        {
            try
            {
                var picture = this._pictureService.GetPictureById(id);
                return this.StatusCode(StatusCodes.Status200OK, picture);
            }
            catch (EntityNotFoundException)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "This picture does not exist!");
            }
        }
        [HttpGet("{username}/pictures")]
        public IActionResult GeUserPictures([FromHeader] string username)
        {
            try
            {
                var user = this._authorisationHelper.TryGetUser(username);
                var pictures = this._pictureService.GetUserPictures(user.Id);
                return this.StatusCode(StatusCodes.Status200OK, pictures);
            }
            catch (EntityNotFoundException)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Bad request");
            }
        }
        [HttpPost("contest/{id}")]
        public IActionResult CreatePicture([FromHeader] string username, [FromBody] PictureDTO dto, int id)
        {
            try
            {
                var user = this._authorisationHelper.TryGetUser(username);
                //var picture = dto.GetPicture();
                dto.Author = user;
                dto.ContestId = id;
               // this._pictureService.Insert(dto, username);
                return this.StatusCode(StatusCodes.Status200OK, dto);
            }
            catch (UnauthorisedOperationsException)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "User not authorised");
            }
            catch (DuplicateEntityException)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, "Picture allready uploded");
            }

        }
      // [HttpGet]
      // public IActionResult Upload()
      // {
      //      return UploadView;
      // }

       // [HttpPost]
       // public IActionResult Upload(Picture pictureModel)
       // {
       //     string fileName = Path.GetFileNameWithoutExtension(Picture. );
       // }
    }
}
