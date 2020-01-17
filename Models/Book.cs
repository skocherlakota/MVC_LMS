using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_LMS.Models
{
    public class Book
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "ISBN is required.")]
        [Display(Name = "ISBN")]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required()]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime Published { get; set; }

        [ScaffoldColumn(false)] 
        public string UserLastUpdated { get; set; }
        [ScaffoldColumn(false)] 
        public DateTime DateLastUpdated { get; set; }
        [ScaffoldColumn(false)] 
        public Boolean LogicalDeleted { get; set; }
        public bool IsAvailable { get; set; }

        //public List<Copi> Copies { get; set; }
        public virtual ICollection<Copi> Copies { get; set; }

    }
}
