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
        public async Task<IList<CustomerDTO>> Get()
        {
            var customers = await context.Customers.Include(s => s.Orders).ToListAsync();

            return mapper.Map<List<Customer>, List<CustomerDTO>>(customers);
        }

        [HttpGet("{id}")]
        public async Task<IList<CustomerDTO>> Get(int id)
        {
            if (id > 0)
            {
                var customers = await context.Customers.Include(s => s.Orders).Where(p => p.CustomerId == id).ToListAsync();

                return mapper.Map<List<Customer>, List<CustomerDTO>>(customers);
            }

            return null;
        }

        //[Authorize]
        //[HttpGet("test")]
        //public void Post()
        //{
        //}

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CustomerDTO customer)
        {
            if (customer != null)
            {
                var customerModel = mapper.Map<CustomerDTO, Customer>(customer);
                context.Add(customerModel);
                context.SaveChanges();
                return Ok(customerModel);
            }

            return null;
        }
    }
}