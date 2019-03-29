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
    public class SalesOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SalesOrdersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IList<SalesOrdersDTO> Get()
        {
            var salesOrders = context.SalesOrders.ToList();

            return mapper.Map<List<SalesOrders>, List<SalesOrdersDTO>>(salesOrders);
        }

        [HttpGet("{id}")]
        public async Task<SalesOrdersDTO> Get(int id)
        {
            if (id > 0)
            {
                var salesOrder = await context.SalesOrders.Include(s => s.Customer).FirstOrDefaultAsync(p => p.SalesOrdersId == id);

                return mapper.Map<SalesOrders, SalesOrdersDTO>(salesOrder);
            }

            return null;
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody] SalesOrdersDTO salesOrder)
        {
            if (salesOrder != null)
            {
                var customer = context.Customers.FirstOrDefault(p => p.CustomerId == salesOrder.CustomerId);

                if (customer != null)
                {
                    var salesOrderModel = mapper.Map<SalesOrdersDTO, SalesOrders>(salesOrder);
                    context.Add(salesOrderModel);
                    context.SaveChanges();
                    return Ok(new { success = true, id = salesOrderModel.SalesOrdersId });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to find the customer." });
                }
            }

            return BadRequest(new { success = false });
        }
    }
}