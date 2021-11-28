using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetrinaDolci.WebAPI.Models
{
    public class Dolce
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Prezzo { get; set; }

        public int? DolceInVenditaId { get; set; }
        public DolceInVendita DolceInVendita { get; set; }
        public List<IngredientiDolce> IngredientiDolce { get; set; }
    }
}
