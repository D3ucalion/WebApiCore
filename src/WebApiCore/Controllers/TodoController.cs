using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApiCore.Models;
using WebApiCore.Models.TodoApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        public TodoController(ITodoRepository todoItems)
        {
            TodoItems = todoItems;
        }
        public ITodoRepository TodoItems { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var item = await TodoItems.GetAll();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            TodoItems.Add(item);
            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            TodoItems.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] TodoItem item, string id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            //item.Key = todo.Key;

            TodoItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var todo = await TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            TodoItems.Remove(id);
            return new NoContentResult();
        }
    }
}
