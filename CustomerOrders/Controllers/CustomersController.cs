using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerOrders.Data;
using CustomerOrders.DTO;
using CustomerOrders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var customers = context.Customers.ToList();

            return mapper.Map<List<Customer>, List<CustomerDTO>>(customers);
        }
    }
}