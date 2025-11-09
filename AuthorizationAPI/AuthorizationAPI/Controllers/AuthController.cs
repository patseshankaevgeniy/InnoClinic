using Authorization.Api.Constants;
using Authorization.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Controllers
{
    [ApiController]
    [Route(RouteCostants.AuthRoute)]
    [Produces("application/json")]
    public sealed class AuthController : ControllerBase
    {
        public AuthController()
        {
        }

        [HttpPost("sign-up", Name = "SignUp")]
        public async Task<ActionResult<SignUpResultDto>> SignUpAsync(SignUpDto dto)
        {
            if (dto.Password != dto.ReEnteredPassword)
            {
                return BadRequest("Passwords do not match.");
            }
            
            return Ok(  );
        }
    }
}
