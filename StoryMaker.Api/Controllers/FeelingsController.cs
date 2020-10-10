using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryMaker.Api.Services;
using StoryMaker.Core.Models;

namespace StoryMaker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeelingsController : ControllerBase
    {
        private readonly FeelingService _feelingService;

        public FeelingsController(FeelingService feelingService)
        {
            _feelingService = feelingService;
        }

        // GET: api/Feeling
        [HttpGet]
        public ActionResult<List<Feeling>> Get() =>
            _feelingService.Get();

        // GET: api/Feeling/5
        [HttpGet("{id}", Name = "GetFeeling")]
        public ActionResult<Feeling> Get(string id)
        {
            var feeling = _feelingService.Get(id);

            if (feeling == null)
            {
                return NotFound();
            }

            return feeling;
        }

        // POST: api/Feeling
        [HttpPost]
        public ActionResult<Feeling> Create(Feeling feeling)
        {
            _feelingService.Create(feeling);

            return CreatedAtRoute("GetFeeling", new { id = feeling.Id.ToString() }, feeling);
        }

        // PUT: api/Feeling/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, Feeling feelingIn)
        {
            var feeling = _feelingService.Get(id);

            if (feeling == null)
            {
                return NotFound();
            }

            _feelingService.Update(id, feelingIn);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var feeling = _feelingService.Get(id);

            if (feeling == null)
            {
                return NotFound();
            }

            _feelingService.Remove(feeling.Id);

            return NoContent();
        }
    }
}