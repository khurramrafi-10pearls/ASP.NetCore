using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrders.CustomServices;
using CustomerOrders.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public string Get()
        {
            return "Test";
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO form)
        {
            var jwtToken = await _accountService.Login(form);

            if (jwtToken == null)
            {
                return Unauthorized();
            }

            return Ok(jwtToken);
        }
    }
}