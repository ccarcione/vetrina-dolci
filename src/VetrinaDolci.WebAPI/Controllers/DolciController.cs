using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetrinaDolci.WebAPI.Models;
using VetrinaDolci.WebAPI.Paginazione;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VetrinaDolci.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DolciController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DolciController(ApplicationContext context)
        {
            this._context = context;
        }

        // GET: api/<DolceController>
        [HttpPost("GetPaginazione")]
        public IActionResult GetAllDolci([FromQuery] QueryParameters queryParameters)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            IQueryable<Dolce> data = _context.Dolci
                //.Where(c => filtri.DataDa.HasValue ? c.Data.Date >= filtri.DataDa.Value.Date : true)
                //.Where(c => filtri.DataA.HasValue ? c.Data.Date <= filtri.DataA.Value.Date : true)
                //.Where(c => !string.IsNullOrWhiteSpace(filtri.TableName) ? c.TableName.Equals(filtri.TableName) : true)
                ;

            PagedList<Dolce> pagedList = PagedList<Dolce>.ToPagedList(data, queryParameters);
            return Ok(pagedList);
        }

        // GET api/<DolceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDolce(int id)
        {
            Dolce dolce = await _context.Dolci
                .Include(x => x.IngredientiDolce)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
            if (dolce == null)
            {
                return NotFound();
            }
            IEnumerable<IngredientiDolce> ingredientiDolce = _context.IngredientiDolce
                .Where(w => w.DolceId == id)
                .ToList();

            return Ok(dolce);
        }
    }
}
