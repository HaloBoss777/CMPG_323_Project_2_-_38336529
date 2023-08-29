using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Project2_CMPG323.API.Filter;
using Project2_CMPG323.CORE.DTO;
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

        //https://localhost:1234/api/Customer/Get/All
        [HttpGet]
        [Route("/api/Customer/Get/All")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }

        //https://Localhost:1234/api/Customer/Get/{id}
        [HttpGet]
        [Route("/api/Customer/Get/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> GetCustomer([FromRoute] short id)
        {
            var customerDTO = await _customerService.GetCustomerAsync(id);

            if(customerDTO is null)
            {
                return BadRequest();
            }

            return Ok(customerDTO);
        }

        //https://Localhost:1234/api/Customer/CreateCustomer
        [HttpPost]
        [Route("/api/Customer/CreateCustomer")]
        [AuthorizationFilter]
        public async Task<IActionResult> CreateCustomer([FromBody] AddCustomerDTO _addCustomerDTO)
        {
            var createdCustomerDTO = await _customerService.CreateCustomerAsync(_addCustomerDTO);

            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomerDTO.CustomerId }, createdCustomerDTO);
        }

        //https://localhost:1234/api/Customer/UpdateCustomer/{id}
        [HttpPatch]
        [Route("/api/Customer/UpdateCustomer/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> UpdateCustomer([FromRoute] short id, [FromBody] UpdatedCustomerDTO _updatedCustomerDTO)
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, _updatedCustomerDTO);

            if(updatedCustomer is null)
            {
                return NotFound();
            }

            return Ok(updatedCustomer);
        }

        //https://localhost:1234/api/Customer/DeleteCustomer/{id}
        [HttpDelete]
        [Route("/api/Customer/DeleteCustomer/{id}")]
        [AuthorizationFilter]
        public async Task<IActionResult> DeleteCustomer([FromRoute] short id)
        {
            var deletedRecord = await _customerService.DeleteCustomerAsync(id);

            if(deletedRecord is null)
            {
                return NotFound();
            }

            return Ok(deletedRecord);
        }
    };
}