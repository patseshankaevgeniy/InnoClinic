using Authorization.Api.Constants;
using Authorization.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Controllers
{
    [ApiController]
    [Route(RouteCostants.AuthRoute)]
    public sealed class AuthController : ControllerBase
    {
        public AuthController()
        {
        }

        [HttpPost("sign-up", Name = "SignUp")]
        public async Task<ActionResult<SignUpResultDto>> SignUpAsync(SignUpDto dto)
        {
            return Ok();
        }
    }
}
