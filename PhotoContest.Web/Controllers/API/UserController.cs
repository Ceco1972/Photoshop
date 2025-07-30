using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Common.Exceptions;
using PhotoContest.Common.Helpers;
using PhotoContest.Models.DTOMapper;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Service.Contracts;
using PhotoContest.Web.Models.ViewModelMapper;
using System;
using System.Security.Authentication;
using AuthenticationException = PhotoContest.Common.Exceptions.AuthenticationException;

namespace PhotoContest.Web.Controllers.API
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthorisationHelper _authorisationHelper;
        //private readonly UserMapper userMapper;
        public UserController(IUserService userService, AuthorisationHelper authorisationHelper)
        {
            this._userService = userService;
            _authorisationHelper = authorisationHelper;
        }
        public UserController()
        { }

        [HttpGet("{id}")]

        public IActionResult GetUserById([FromRoute] int id)
        {
            try
            {
                var user = this._userService.GetUser(id);
                if (user == null)
                {
                    throw new EntityNotFoundException($"No user under id {id}");
                }
                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("username")]
        public IActionResult GetUserByUsername([FromQuery] string username)
        {
            try
            {
                User user = this._userService.CheckUsername(username);

                if (user == null)
                {
                    throw new EntityNotFoundException($"No user with username {username} recorded");
                }
                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }

        }

        [HttpPost("Register")]
        public IActionResult RegisterUser([FromBody] UserDTO userDto)
        {
            var user = userDto.GetUser();
            this._userService.InsertUser(user);

            return this.StatusCode(StatusCodes.Status201Created, userDto);
        }
        [HttpPost("Login")]
        public IActionResult Login([FromHeader] string username, string password)
        {
            try
            {
                var user = this._authorisationHelper.TryGetUser(username, password);
                if (user == null)
                {
                    throw new AuthenticationException();
                }
                var userDTO = user.GetDTO();
                return this.StatusCode(StatusCodes.Status200OK, userDTO);
            }
            catch (AuthenticationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (ArgumentException)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Invalid credentials!");
            }
        }
    }
}
