using Domain;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using System.Collections.Generic;

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

        public IActionResult GetDives()
        {
            IEnumerable<Dive>  model = _divingService.GetAll();
            var result = new OkObjectResult(model);
            return  result;
        }

        public IActionResult Post([FromBody]Dive dive)
        {
            _divingService.AddDive(dive);

            return Created("/api/getdives", dive);

        }
    }
}
