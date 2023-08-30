using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.CORE.DTO;
using Project2_CMPG323.CORE.Services;

namespace Project2_CMPG323.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Implement the Auth Service (Dependency Ingection)
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            //Injects only the Dependencys that are needed
            _authService = authService;
        }

        [HttpPost]
        [Route("~/api/Authentication/Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerdetails)
        {
            var foundUser = await _authService.Register(registerdetails);

            if(foundUser is null)
            {
                return BadRequest();
            }

            return Ok(foundUser);
        }

        [HttpPost]
        [Route("~/api/Authentication/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(RegisterDTO registerdetails)
        {
            var foundUser = await _authService.Login(registerdetails);

            if (foundUser is null)
            {
                return BadRequest();
            }

            return Ok(foundUser);
        }
    }
}
