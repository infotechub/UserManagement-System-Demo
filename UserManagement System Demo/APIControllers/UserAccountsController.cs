using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement_System_Demo.Models;

namespace UserManagement_System_Demo.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UserAccountsController(
                UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromForm] LoginModel model)
        {
            if (model.Email == null || model.Password == null)
            {
                return BadRequest("Username and password cannot be empty!");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authclaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),


                };
                foreach (var userRole in userRoles)
                {
                    authclaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = GetToken(authclaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });



            }
            return Unauthorized();
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model.Username == "" || model.Password == "" || model.ConfirmPassword == "" ||
                model.Email == "" || model.ConfirmEmail == "")
            {
                return BadRequest("Some fields are empty. Kindly check the form fields and fill all compulsory fields");
            }

            var useraccount = await _userManager.FindByEmailAsync(model.Username);
            if (useraccount != null)
            {
                return BadRequest("Username exist in our database!");
            }
            var checkemail = await _userManager.FindByEmailAsync(model.Email);
            if (checkemail != null)
            {
                return BadRequest("Email exist in our database!");
            }

            if (model.Email != model.ConfirmEmail)
            {
                return BadRequest("Email and confirm email does not match!");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passowrd and confirm password does not match!");
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });

        }


        [HttpPost]
        [Route("adminregistration")]
        public async Task<IActionResult> AdminRegister([FromBody] RegisterModel model)
        {
            if (model.Username == "" || model.Password == "" || model.ConfirmPassword == "" ||
                model.Email == "" || model.ConfirmEmail == "")
            {
                return BadRequest("Some fields are empty. Kindly check the form fields and fill all compulsory fields");
            }

            var useraccount = await _userManager.FindByEmailAsync(model.Username);
            if (useraccount != null)
            {
                return BadRequest("Username exist in our database!");
            }
            var checkemail = await _userManager.FindByEmailAsync(model.Email);
            if (checkemail != null)
            {
                return BadRequest("Email exist in our database!");
            }

            if (model.Email != model.ConfirmEmail)
            {
                return BadRequest("Email and confirm email does not match!");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passowrd and confirm password does not match!");
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
               
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });

        }



        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }










    }


}
