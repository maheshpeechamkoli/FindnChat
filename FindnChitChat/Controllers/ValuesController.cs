using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindnChitChat.Data;
using FindnChitChat.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindnChitChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
            // if (_context.Values.Count() == 1)
            // {
                // // Create a new TodoItem if collection is empty,
                // // which means you can't delete all TodoItems.
                // _context.Values.Add(new Value { Name = "Item2" });
                // _context.SaveChanges();
            // }
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
             var values = await _context.Values.ToListAsync();
             return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var values = await  _context.Values.FirstOrDefaultAsync(x=> x.Id==id);
             return Ok(values);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
