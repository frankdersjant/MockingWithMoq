using Domain;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplMoqIntro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiversController : ControllerBase
    {
        private readonly IDivingService _divingService;
        public DiversController(IDivingService divingService)
        {
            _divingService = divingService;
        }

        public IEnumerable<Dive> GetDives()
        {
            IEnumerable<Dive>  model = _divingService.GetAll();
            return  model;
        }


        //Pure Core 3.1
        [HttpGet(Name ="GetAllDivesP2")]

        public IActionResult GetDivesPart2()
        {
            IEnumerable<Dive> model = _divingService.GetAll();
            return Ok(model);
        }

        public IActionResult GetDive(int id)
        {
           Dive foundDive = _divingService.GetDive(id);

            if (foundDive == null)
            {
                return NotFound();
            }
            else return Ok(foundDive);

        }

        public IActionResult Post([FromBody]Dive dive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _divingService.AddDive(dive);

            return CreatedAtAction("/api/getdives", dive);
        }


        public IActionResult Delete(int id)
        {
            Dive foundDive = _divingService.GetDive(id);

            if (foundDive == null)
            {
                return NotFound();
            }
            else return Ok();
        }
    }
}
