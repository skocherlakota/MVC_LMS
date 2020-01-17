using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_LMS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_LMS.Models
{
    public class Reserve
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ReserveID")]
        public int ReserveID { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserID { get; set; }
        //public virtual ApplicationUser UserProfile { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReserveDate { get; set; }
        [ScaffoldColumn(false)]
        public string UserLastUpdated { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DateLastUpdated { get; set; }
        [ScaffoldColumn(false)]
        public Boolean LogicalDeleted { get; set; }
        public virtual Book Book { get; set; }

        //public int SelectedProductId { get { return 2; } }
        //public SelectList ProductList { get; set; }

        /*
         *Entity Framework can't automatically recognize BookID as the primary key of this entity because its name doesn't follow the ID or classname ID naming convention. Therefore, the Key attribute is used to identify it as the key:
         * The ForeignKey Attribute can be applied to the dependent class to establish the relationship
         */

    }
}
