using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetrinaDolci.WebAPI.Models;
using VetrinaDolci.WebAPI.Paginazione;

namespace VetrinaDolci.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DolciInVenditaController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DolciInVenditaController(ApplicationContext context)
        {
            this._context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DolceInVendita>> GetDolceInVendita(int id)
        {
            var dolceInVendita = await _context.DolciInVendita.FindAsync(id);

            if (dolceInVendita == null)
            {
                return NotFound();
            }

            return dolceInVendita;
        }

        [HttpGet]
        [HttpPost("GetPaginazione")]
        public IActionResult GetAllDolciInVendita([FromQuery] QueryParameters queryParameters)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            DateTime now = DateTime.Now;
            IQueryable<DolceInVendita> data = _context.DolciInVendita
                .Include(c => c.Dolce)
                .Where(c => (now - c.InVenditaDa).Hours < 72)   // solo dolci commestibili (max 3gg)
                //.Where(c => filtri.DataDa.HasValue ? c.Data.Date >= filtri.DataDa.Value.Date : true)
                //.Where(c => filtri.DataA.HasValue ? c.Data.Date <= filtri.DataA.Value.Date : true)
                //.Where(c => !string.IsNullOrWhiteSpace(filtri.TableName) ? c.TableName.Equals(filtri.TableName) : true)
                ;

            PagedList<DolceInVendita> pagedList = PagedList<DolceInVendita>.ToPagedList(data, queryParameters);
            // calcolo prezzo di v  endita in base al tempo trascorso
            pagedList.Data.ToList().ForEach(f =>
            {
                f.Prezzo = GetPrezzo(f);
            });
            return Ok(pagedList);
        }

        // PUT: api/Values/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDolceInVendita(int id, DolceInVendita dolceInVendita)
        {
            if (id != dolceInVendita.Id)
            {
                return BadRequest();
            }

            _context.Entry(dolceInVendita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DolceInVenditaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DolceInVendita>> PostDolceInVendita(DolceInVendita dolceInVendita)
        {
            _context.DolciInVendita.Add(dolceInVendita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDolceInVendita", new { id = dolceInVendita.Id }, dolceInVendita);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDolceInVendita(int id)
        {
            DolceInVendita dolceInVendita = await _context.DolciInVendita.FindAsync(id);
            if (dolceInVendita == null)
            {
                return NotFound();
            }

            _context.DolciInVendita.Remove(dolceInVendita);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool DolceInVenditaExists(int id)
        {
            return _context.DolciInVendita.Any(e => e.Id == id);
        }

        private double? GetPrezzo(DolceInVendita item)
        {
            DateTime now = DateTime.Now;
            switch ((now - item.InVenditaDa).Hours)
            {
                case <= 24:
                    return item.Dolce.Prezzo;
                case <= 48:
                    return item.Dolce.Prezzo * 0.8;
                case <= 72:
                    return item.Dolce.Prezzo * 0.2;
                default:
                    return null;
            }
        }
    }
}
