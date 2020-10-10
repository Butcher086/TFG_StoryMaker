using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryMaker.Api.Services;
using StoryMaker.Core.Models;
using StoryMaker.SmartCenter;

namespace StoryMaker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhrasesController : ControllerBase
    {
        private readonly PhraseService _phraseService;
        private SmarCenter smartCenter;

        public PhrasesController(PhraseService phraseService)
        {
            _phraseService = phraseService;
            smartCenter = new SmarCenter();
        }

        // GET: api/Phrases        
        [HttpGet]
        public ActionResult<List<Phrase>> Get() =>
            _phraseService.Get();

        // GET: api/Phrases/5
        [HttpGet("{id}", Name = "GetPhrase")]
        public ActionResult<Phrase> Get(string id)
        {
            var phrase = _phraseService.Get(id);

            if(phrase == null)
            {
                return NotFound();
            }

            return phrase;
        }

        // POST: api/Phrases
        [HttpPost]
        public ActionResult<Phrase> Create(Phrase phrase)
        {
            _phraseService.Create(phrase);

            return CreatedAtRoute("GetPhrase", new { id = phrase.Id.ToString() }, phrase);
        }

        // PUT: api/Phrases/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, Phrase phraseIn)
        {
            var phrase = _phraseService.Get(id);

            if (phrase == null)
            {
                return NotFound();
            }

            _phraseService.Update(id, phraseIn);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var phrase = _phraseService.Get(id);

            if (phrase == null)
            {
                return NotFound();
            }

            _phraseService.Remove(phrase.Id);

            return NoContent();
        }

        //Add Analized Phrase
        [HttpPost]
        [Route("AddAnalycedPhrase")]
        public IActionResult AddAnalycedPhrase([FromBody]string phrase)
        {
            var phrases = smartCenter.textAnalytics.SentimentAnalysis(phrase);

            foreach (var p in phrases)
            {
                p.PhraseType = smartCenter.languageService.ManageIntentions(p.PhraseText);
                _phraseService.Create(p);
            }
            return Ok(phrases);
        }
    }
}
