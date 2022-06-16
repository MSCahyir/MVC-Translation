using System.Collections.Generic;
using System.Threading.Tasks;
using Translation.Models;
using Translation.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace Translation.Controllers
{
    [Route("api/funtranslation")]
    [ApiController]
    public class FunTranslationController : Controller
    {
        private readonly IDataRepository<FunTranslation> _dataRepository;
        private readonly RestClient _client;

        public FunTranslationController(IDataRepository<FunTranslation> dataRepository)
        {
            _dataRepository = dataRepository;
            _client = new RestClient("https://api.funtranslations.com/translate/");
        }

        [Authorize]
        public IActionResult Index()
        {
            var listofData = _dataRepository.GetAll();
            return View();
        }

        // GET: api/funtranslation
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<FunTranslation> funTranslations = _dataRepository.GetAll();
            return View(funTranslations);
        }


        // GET: api/funtranslation/5
        [Authorize]
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            FunTranslation funTranslation = _dataRepository.Get(id);

            if (funTranslation == null)
            {
                return NotFound("The funTranslation data record couldn't be found.");
            }

            return Ok(funTranslation);
        }

        // POST: api/funtranslation
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FunTranslation funTranslation)
        {
            var request = new RestRequest("braille.json?text=" + funTranslation.NormalText);
            var response = await _client.ExecuteGetAsync(request);

            FunTranslationJsonType funTranslatedObj = JsonSerializer.Deserialize<FunTranslationJsonType>(response.Content);

            if(funTranslatedObj.success != null)
                funTranslation.TranslatedText = funTranslatedObj.contents.translated;
            else
                return BadRequest(response.StatusDescription);
    
            if (funTranslation == null)
            {
                return BadRequest("Fun Translation is null.");
            }
            _dataRepository.Add(funTranslation);
            return CreatedAtRoute(
                  "Get",
                  new { Id = funTranslation.Id },
                  funTranslation);
        }

        // PUT: api/funtranslation/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] FunTranslation funTranslation)
        {
            if (funTranslation == null)
            {
                return BadRequest("Fun Translation is null.");
            }

            FunTranslation updatedFunTranslation = _dataRepository.Get(id);
            if (updatedFunTranslation == null)
            {
                return NotFound("The Fun Translation record couldn't be found.");
            }

            _dataRepository.Update(updatedFunTranslation, funTranslation);
            return NoContent();
        }

        // DELETE: api/funtranslation/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            FunTranslation funTranslation = _dataRepository.Get(id);
            if (funTranslation == null)
            {
                return NotFound("The Fun Translation record couldn't be found.");
            }

            _dataRepository.Delete(funTranslation);

            return Ok(funTranslation);
        }
    }
}