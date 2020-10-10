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
    public class HistoriesController : ControllerBase
    {
        private readonly HistoryService _historyService;
        public HistoriesController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        // GET: api/Histories
        [HttpGet]
        public ActionResult<List<History>> Get() =>
            _historyService.Get();

        // GET: api/Histories/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<History> Get(string id)
        {
            var history = _historyService.Get(id);

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        // POST: api/Histories
        [HttpPost]
        public ActionResult<History> Create(History history)
        {
            _historyService.Create(history);

            return CreatedAtRoute("GetHistory", new { id = history.Id.ToString() }, history);
        }

        // PUT: api/Histories/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, History historyIn)
        {
            var history = _historyService.Get(id);

            if (history == null)
            {
                return NotFound();
            }

            _historyService.Update(id, historyIn);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var history = _historyService.Get(id);

            if (history == null)
            {
                return NotFound();
            }

            _historyService.Remove(history.Id);

            return NoContent();
        }

        //GET: api/Histories/GetRandomHistory
        // Get random history
        [HttpGet]
        [Route("GetRandomHistory")]
        public ActionResult<History> GetRandomHistory() =>
            _historyService.GetRandomHistory();

        // POST: api/Histories/CreateHistoryByPhrases
        [HttpPost]
        [Route("CreateHistoryByPhrases")]
        public IActionResult CreateHistoryByPhrases(List<Phrase> phrases)
        {
            var history = _historyService.CreateHistoryByPhrases(phrases);

            return Ok(history);
        }

        // POST: api/Histories/AddHistoryByPhrases
        [HttpPost]
        [Route("AddHistoryByPhrases")]
        public ActionResult<History> AddHistoryByPhrases(List<string> phrases)
        {
            _historyService.AddHistoryByPhrases(phrases);

            return CreatedAtRoute("AddHistoryByPhrases", phrases);
        }

        // GET: api/Histories/GetTextHistory
        [HttpPost]
        [Route("GetTextHistory")]
        public ActionResult<HistoryText> GetTextHistory([FromBody]History history)
        {
            var phrases = _historyService.JoinHistoryPhrases(history);
            return new HistoryText()
            {
                Id = history.Id,
                textPhrases = phrases
            };
        }
            
    }
}
