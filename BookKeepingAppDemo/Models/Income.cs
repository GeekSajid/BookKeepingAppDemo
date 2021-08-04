using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookKeepingAppDemo.Models
{
    public class Income
    {
        [Key]
        public int Id { get; set; }
        public int IncomeTypeId { get; set; }
        public decimal Amount { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }
        public virtual IncomeType IncomeType { get; set; }
    }

    public enum Month
    {
        Jan = 1,
        Feb = 2,
        Mar = 3,
        Apr = 4,
        May = 5,
        Jun = 6,
        Jul = 7,
        Aug = 8,
        Sep = 9,
        Oct = 10,
        Nov = 11,
        Dec = 12
    }
}
