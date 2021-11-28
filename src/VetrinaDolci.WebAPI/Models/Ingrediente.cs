using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetrinaDolci.WebAPI.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double? Proteine { get; set; }
        public double? Zuccheri { get; set; }
        public double? Grassi { get; set; }
        public double? Colesterolo { get; set; }
        public double? Fibra { get; set; }
        public double? Kcal { get; set; }

        public List<IngredientiDolce> IngredientiDolce { get; set; }
    }
}
