using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Utilities;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HenwoniDataModifierAPI.Areas.User.ViewModels;
using Microsoft.EntityFrameworkCore;
using HenwoniDataModifierAPI.Areas.User.SystemServices;
using HenwoniDataModifierAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HenwoniDataModifierAPI.Areas.User.Controllers
{
    [Route("user/auth")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("User")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IJWTTokenService _jwttokenservice;
        public AuthController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IJWTTokenService jWTTokenServices)
        {
            _userManager = userManager;
            _context = context;
            _jwttokenservice = jWTTokenServices;
        }

        /// <summary>
        /// This checks whether the user is logged in
        /// </summary>
        /// <returns></returns>
        [HttpPost("verify")]
        public async Task<ActionResult> VerifyToken()
        {
            GenericResponse res = new GenericResponse();
            // The sender has to pay the fee
            var userId = User.FindFirst("sub")?.Value; // 'sub' is typically used for user ID
            var userName = User.Identity?.Name;
            ApplicationUser currentUser = null;
            if (userName != null) currentUser = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (currentUser == null)
            {
                res.Success = false;
                res.Message = "Token invalid";
                return BadRequest(res);
            }
            else
            {
                return Ok(new AuthResponseViewModel
                {
                    Success = true,
                    Message = "Token is valid",
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.FirstName,
                    Email = currentUser.Email,
                    Username = currentUser.UserName
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        // [Route("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthRequestViewModel request)
        {
            AuthResponseViewModel res = new() { };
            if (!ModelState.IsValid)
            {
                res.Success = false;
                res.Message = "Bad Request";
                return BadRequest(res);
            }
            ApplicationUser managedUser = null;
            if (!String.IsNullOrEmpty(request.Email)) managedUser = await _userManager.FindByEmailAsync(request.Email);
            if (!String.IsNullOrEmpty(request.Username)) managedUser = await _userManager.FindByNameAsync(request.Username);

            if (managedUser == null)
            {
                res.Success = false;
                res.Message = "Login Failed. Bad credentials";
                return BadRequest(res);
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
            if (!isPasswordValid)
            {
                res.Success = false;
                res.Message = "Bad credentials: " + request.Password;
                return BadRequest(res);
            }
            // ApplicationUser? userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            // if (userInDb is null) return BadRequest();
            var accessToken = _jwttokenservice.CreateToken2(managedUser);

            var user = await _context.Users.Where(x => x.UserName == managedUser.UserName).FirstOrDefaultAsync();
            await _context.SaveChangesAsync();
            return Ok(new AuthResponseViewModel
            {
                Success = true,
                Message = "Login successful!",
                FirstName = managedUser.FirstName,
                LastName = managedUser.LastName,
                Email = managedUser.Email,
                Token = accessToken,
                Username = managedUser.UserName
            });
        }

        /*/// <summary>
        /// @TODO: Update to match default create user function
        /// Missing functions
        /// </summary>
        /// <param name="postUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserPostRequest postUser)
        {
            var user = CreateUser();
            user.CopyPropertiesFrom(postUser);
            user.UserName = postUser.Email;
            Debug.WriteLine("Using password: " + postUser.Password);
            var result = await _userManager.CreateAsync(user, postUser.Password);

            if (result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                UserResponseViewModel res = new UserResponseViewModel();
                res.Id = userId;
                res.UserName = postUser.Email;
                res.CopyPropertiesFrom(postUser);
                return Ok(res);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }*/

    }
}
