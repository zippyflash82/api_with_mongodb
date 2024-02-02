
using Api.Interfaces;
using api_with_mongodb.Data;
using api_with_mongodb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mongo_db_demo.Data;


namespace api_with_mongodb.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MongoContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(MongoContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }


        [HttpPost("register")]
        public async Task<ActionResult> Signup(SignupDto signupDto)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MongoContext.Products'  is null.");
            }
            var tempUserModel = new UserModel
            {
                Id = _context.Users.Count() + 1,
                USERNAME = signupDto.USERNAME,
                Password = signupDto.Password,
                EmailAddress = signupDto.EmailAddress
            };
            _context.Users.Add(tempUserModel);
            await _context.SaveChangesAsync();

            return Ok("New User Created");
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            IActionResult response = Unauthorized("Invalid Credentials");

            var userExists = await _context.Users.AnyAsync(appUser => appUser.USERNAME == loginDto.UserName && appUser.Password == loginDto.Password);

            if (userExists)
            {
                return Ok(new { token = _tokenService.CreateToken(loginDto) });
            }
            return response;
        }
    }
}