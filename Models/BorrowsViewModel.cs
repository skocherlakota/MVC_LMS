using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_LMS.Models
{
    public class BorrowsViewModel
    {
        public int BorrowID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime BorrowDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ActualReturnDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name="Due Date")]
        public DateTime DueDate { get; set; }
        [DataType(DataType.Currency)]
        public double? Fine { get; set; }


    }
}
