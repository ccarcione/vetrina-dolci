using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public SetupController(ApplicationContext applicationContext)
        {
            this._applicationContext = applicationContext;
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
            await SeedHelper.SeedFromCsv(_applicationContext);

            return Ok("seed done");
        }
    }
}
