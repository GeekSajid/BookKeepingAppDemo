using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookKeepingAppDemo.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public int ExpenseTypeId { get; set; }
        public decimal Amount { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }

    }
}
