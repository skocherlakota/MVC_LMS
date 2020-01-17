using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_LMS.Models
{
    public class Borrow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "BorrowID")]
        public int BorrowID { get; set; }

        [ForeignKey("AspNetUsers")]
        [Required]
        public string UserID { get; set; }

        [ForeignKey("Copi")]
        [Display(Name = "CopiID")]
        public int CopyID { get; set; }


        [DataType(DataType.Date)] 
        public DateTime BorrowDate { get; set; }
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ReturnDate {
            get {
                return BorrowDate.AddDays(14);
            }
            private set { }
        }

        [DataType(DataType.Date)]        
        public DateTime? ActualReturnDate { get; set; }
        public double? Fine { get; set; }

        [ScaffoldColumn(false)]
        public string UserLastUpdated { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DateLastUpdated { get; set; }
        [ScaffoldColumn(false)]
        public Boolean LogicalDeleted { get; set; }
        public virtual Copi Copi { get; set; }

    }
}
