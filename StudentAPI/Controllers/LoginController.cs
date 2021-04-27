using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{

    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly studentsContext context;
        private readonly IConfiguration _config;
        public LoginController(studentsContext _context, IConfiguration config)
        {
            context = _context;
            _config = config;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> StoreUserDetail([FromBody] Login model)
        {

            await context.LoginModel.AddAsync(model);
            await context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPost]
        [Route("createtoken")]
        public IActionResult CreateTokenForUser([FromBody] Login model)
        {
            if (IsUserExisting(model.Username))
            {
                if (IsUserAuthenticated(model.Username, model.Password))
                {
                    try
                    {
                        var claims = new[]
                           {
                            new Claim(ClaimTypes.NameIdentifier,model.Username)
                        };
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.Now.AddDays(30),
                            SigningCredentials = signinCredentials
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        return Ok(new { token = tokenHandler.WriteToken(token) });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                else
                {
                    return Unauthorized("Password is incorrect");
                }
            }
            else
            {
                return NotFound("User doesn't Exist. Please check the Username.");
            }

        }

        private bool IsUserAuthenticated(string username, string password)
        {
            string pwd = context.LoginModel.Where(w => w.Username == username).Select(s => s.Password).First().ToString();
            /*
             Difference between == and Equals
             */
            if (pwd.Equals(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsUserExisting(string username)
        {
            bool result = context.LoginModel.Any(a => a.Username == username);
            return result;
        }

        // public ActionResult UserLogin()
        // {

        //     return Ok();
        // }
    }
}
