using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_LMS.Models
{
    public class Copi
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string AccessionNo { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal PurchasePrice { get; set; }
        public string UserLastUpdated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Boolean LogicalDeleted { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
