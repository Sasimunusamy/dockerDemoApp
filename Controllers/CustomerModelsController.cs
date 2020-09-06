using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dockerDemoApp.Models;

namespace dockerDemoApp.Controllers
{
    [Route("api/CustomerModels")]
    [ApiController]
    public class CustomerModelsController : ControllerBase
    {
        private readonly TestContext _context;

        public CustomerModelsController(TestContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomerItems()
        {
            return await _context.CustomerItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerModel(long id)
        {
            var customerModel = await _context.CustomerItems.FindAsync(id);

            if (customerModel == null)
            {
                return NotFound();
            }

            return customerModel;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerModel(long id, CustomerModel customerModel)
        {
            if (id != customerModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomerModel([FromBody] CustomerModel customerModel)
        {
            _context.CustomerItems.Add(customerModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerModel), new { id = customerModel.Id }, customerModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerModel>> DeleteCustomerModel(long id)
        {
            var customerModel = await _context.CustomerItems.FindAsync(id);
            if (customerModel == null)
            {
                return NotFound();
            }

            _context.CustomerItems.Remove(customerModel);
            await _context.SaveChangesAsync();

            return customerModel;
        }

        private bool CustomerModelExists(long id)
        {
            return _context.CustomerItems.Any(e => e.Id == id);
        }
    }
}
