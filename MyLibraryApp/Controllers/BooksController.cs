using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLibraryApp.Data.Models;
using MyLibraryApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly IBookService _service;
        public BooksController(IBookService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var result = _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var result = _service.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }



        [HttpPost]
        public ActionResult Post([FromBody] Book item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _service.Add(item);
            return CreatedAtAction("Get", new { id = item.Id }, item);

        }




        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existingItem = _service.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }
                

            _service.Remove(id);
            return Ok();


        }
    }
}
