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
        public double Prezzo { get; set; }
        public string TipoPiatto { get; set; }
        public string IngPrincipale { get; set; }
        public int Persone { get; set; }
        public string Preparazione { get; set; }
        public string Note { get; set; }

        public List<DolceInVendita> DolciInVendita { get; set; }
        public List<IngredientiDolce> IngredientiDolce { get; set; }
    }
}
