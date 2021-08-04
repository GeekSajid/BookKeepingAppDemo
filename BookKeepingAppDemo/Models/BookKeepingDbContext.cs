using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookKeepingAppDemo.Models
{
    public class BookKeepingDbContext : DbContext
    {
        public BookKeepingDbContext(DbContextOptions<BookKeepingDbContext> options) : base(options)
        { }

        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
