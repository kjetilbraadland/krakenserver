using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using aspnetcoreapp.Models;
using System.Text.Encodings.Web;

namespace aspnetcoreapp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class KrakenController : Controller
    {
        public KrakenController(IRepository items)
        {
            Items = items;

            //Items.RemoveAll();

        }
        public IRepository Items { get; set; }

        [HttpGet]
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }

        [HttpGet]
        public IEnumerable<Item> GetAll()
        {
            return Items.GetAll();
        }

        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult GetById(string id)
        {
            var item = Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("{client}")]
        public IActionResult GetByClient(string client)
        {
            var item = Items.GetNextItem(client);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Items.Add(item);
            return CreatedAtRoute("GetItem", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Item item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = Items.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            Items.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Item item, string id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = Items.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            item.Key = todo.Key;

            Items.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = Items.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            Items.Remove(id);
            return new NoContentResult();
        }
    }
}