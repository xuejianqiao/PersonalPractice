using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.JWT.AuthHelper.Authentication;
using Authentication.JWT.Contract;
using Authentication.JWT.Contract.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Authentication.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var users = TestUsers.Users.Where(r => r.UserName.Equals(request.UserName));
            if (users.Count() <= 0)
            {
                ModelState.AddModelError("login_failure", "Invalid username.");
                return BadRequest(ModelState);
            }
            var user = users.First();
            if (!request.Password.Equals(user.Password))
            {
                ModelState.AddModelError("login_failure", "Invalid password.");
                return BadRequest(ModelState);
            }
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);
            var token = await _jwtFactory.GenerateEncodedToken(user.UserName, claimsIdentity);
            return new OkObjectResult(token);
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return Ok(claimsIdentity.Claims.ToList().Select(r => new { r.Type, r.Value }));
        }


     
    }
}