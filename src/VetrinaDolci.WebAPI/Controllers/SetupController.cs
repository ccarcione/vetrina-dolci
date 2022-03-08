using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VetrinaDolci.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SetupController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<SetupController> logger;

        public SetupController(ApplicationContext applicationContext, ILogger<SetupController> logger)
        {
            this._applicationContext = applicationContext;
            this.logger = logger;
        }

        // GET: api/<SetupController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (_applicationContext.Dolci.Any()
                || _applicationContext.Ingredienti.Any())
            {
                return StatusCode(410, "seed already done");
            }

            //seed here
            await SeedHelper.SeedFromCsv(this.logger, _applicationContext);

            return Ok("seed done");
        }
    }
}
