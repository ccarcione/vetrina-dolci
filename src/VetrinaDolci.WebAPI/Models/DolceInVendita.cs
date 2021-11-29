using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetrinaDolci.WebAPI.Models
{
    public class DolceInVendita
    {
        public int Id { get; set; }
        public int Disponibilita { get; set; }
        public DateTime InVenditaDa { get; set; }
        public int? DolceId { get; set; }
        public Dolce Dolce { get; set; }
        [NotMapped]
        public double? Prezzo { get; set; }
    }
}
