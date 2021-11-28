using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetrinaDolci.WebAPI.Models
{
    public class IngredientiDolce
    {
        public int Id { get; set; }
        public int DolceId { get; set; }
        public Dolce Dolce { get; set; }
        public int IngredienteId { get; set; }
        public Ingrediente Ingrediente { get; set; }

        public string Quantita { get; set; }
        public string UnitaDiMisura { get; set; }
        public string Note { get; set; }
    }
}
