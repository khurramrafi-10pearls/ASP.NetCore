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
        public ActionResult<IList<CustomerDTO>> Get()
        {
            var customers = context.Customers.Include(s=>s.Orders).ToList();

            return mapper.Map<List<Customer>, List<CustomerDTO>>(customers);
        }

        [Authorize]
        [HttpGet("test")]
        public void Post()
        {
        }
    }
}