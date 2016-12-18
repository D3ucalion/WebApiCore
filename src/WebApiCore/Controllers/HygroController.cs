using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Models;
using WebApiCore.Models.HygroApi.Models;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    public class HygroController : Controller
    {
        public HygroController(IHygroRepository hygroItems)
        {
            HygroItems = hygroItems;
        }
        public IHygroRepository HygroItems { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var item = await HygroItems.GetAll();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("latest", Name = "GetHygro")]
        public async Task<IActionResult> GetLatest()
        {
            var item = await HygroItems.Find();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] HygroItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            HygroItems.Add(item);
            return CreatedAtRoute("GetHygro", new { id = item.Key }, item);
        }
    }
}
