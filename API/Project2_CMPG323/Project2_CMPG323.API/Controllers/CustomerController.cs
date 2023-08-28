using Microsoft.AspNetCore.Mvc;
using Project2_CMPG323.CORE.Services;

namespace Project2_CMPG323.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        public readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //https://localhost:1234/api/
        [HttpGet]
        [Route("/api/Customer/GetAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }
    };
}