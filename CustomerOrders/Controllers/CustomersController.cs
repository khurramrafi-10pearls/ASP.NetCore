using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerOrders.Data;
using CustomerOrders.DTO;
using CustomerOrders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IList<CustomerDTO> Get()
        {
            var customers = context.Customers.ToList();

            return mapper.Map<List<Customer>, List<CustomerDTO>>(customers);
        }

        [HttpGet("{id}")]
        public async Task<CustomerDTO> Get(int id)
        {
            if (id > 0)
            {
                var customer = await context.Customers.Include(s => s.Orders).FirstOrDefaultAsync(p => p.CustomerId == id);

                return mapper.Map<Customer, CustomerDTO>(customer);
            }

            return null;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CustomerDTO customer)
        {
            if (customer != null)
            {
                var customerModel = mapper.Map<CustomerDTO, Customer>(customer);
                context.Add(customerModel);
                context.SaveChanges();
                return Ok(new { success = true, id = customerModel.CustomerId });
            }

            return BadRequest(new { success = false });
        }
    }
}