using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using aspnetcoreapp.Models;

namespace aspnetcoreapp.Controllers
{
    [Route("api/[controller]")]
    public class KrakenController : Controller
    {
        public KrakenController(IRepository items)
        {
            Items = items;
        }
        public IRepository Items { get; set; }

        [HttpGet]
        public IEnumerable<Item> GetAll()
        {
            return Items.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}