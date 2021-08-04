using BookKeepingAppDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookKeepingAppDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReconciliationController : ControllerBase
    {
        private readonly BookKeepingDbContext _context;
        public ReconciliationController(BookKeepingDbContext context)
        {
            _context = context;
        }

        //[Route("api/[controller]/GetMonthlyIncomesByYear")]
        [HttpGet("{year}")]
        public async Task<ActionResult<IEnumerable<Income>>> GetMonthlyIncomesByYear(int year)
        {
            var FilteredIncomes = await _context.Incomes.Where(i => i.Year == year).GroupBy(x => x.Month, (key, group) => new Income { Amount = group.Sum(g => g.Amount), Month = key }).ToListAsync();
            for (int i = 1; i <= 12; i++)
            {
                var monthlyValue = FilteredIncomes.Where(f => (int)f.Month == i).FirstOrDefault();
                if (monthlyValue == null)
                {
                    FilteredIncomes.Add(new Income() { Month = (Month)i, Amount = 0 });
                }
            }
            return FilteredIncomes.OrderBy(f => f.Month).ToList();
        }

        [HttpGet("{year}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetMonthlyExpensesByYear(int year)
        {
            var FilteredExpenses = await _context.Expenses.Where(i => i.Year == year).GroupBy(x => x.Month, (key, group) => new Expense { Amount = group.Sum(g => g.Amount), Month = key }).ToListAsync();
            for (int i = 1; i <= 12; i++)
            {
                var monthlyValue = FilteredExpenses.Where(f => (int)f.Month == i).FirstOrDefault();
                if (monthlyValue == null)
                {
                    FilteredExpenses.Add(new Expense() { Month = (Month)i, Amount = 0 });
                }
            }
            return FilteredExpenses.OrderBy(f => f.Month).ToList();
        }

        [HttpGet("{year}")]
        public async Task<ActionResult<IEnumerable<Income>>> GetMonthlyTypeWiseIncomesByYear(int year)
        {
            var FilteredIncomes = await _context.Incomes.Where(i => i.Year == year).GroupBy(x => new { x.Month, x.IncomeTypeId }, (key, group) => new Income { Amount = group.Sum(g => g.Amount), Month = key.Month, IncomeTypeId = key.IncomeTypeId }).OrderBy(o => o.Month).ToListAsync();
            var allIncomeTypes = _context.IncomeTypes.ToList();
            foreach (var type in allIncomeTypes)
            {
                for (int i = 1; i <= 12; i++)
                {
                    var monthlyValue = FilteredIncomes.Where(f => (int)f.Month == i && f.IncomeTypeId == type.Id).FirstOrDefault();
                    if (monthlyValue == null)
                    {
                        FilteredIncomes.Add(new Income() { Month = (Month)i, Amount = 0, IncomeTypeId = type.Id });
                    }
                }
            }

            return FilteredIncomes.OrderBy(f => f.Month).ToList();
        }

        [HttpGet("{year}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetMonthlyTypeWiseExpensesByYear(int year)
        {
            var FilteredExpenses = await _context.Expenses.Where(i => i.Year == year).GroupBy(x => new { x.Month, x.ExpenseTypeId }, (key, group) => new Expense { Amount = group.Sum(g => g.Amount), Month = key.Month, ExpenseTypeId = key.ExpenseTypeId }).OrderBy(o => o.Month).ToListAsync();
            var allExpensesTypes = _context.ExpenseTypes.ToList();
            foreach (var type in allExpensesTypes)
            {
                for (int i = 1; i <= 12; i++)
                {
                    var monthlyValue = FilteredExpenses.Where(f => (int)f.Month == i && f.ExpenseTypeId == type.Id).FirstOrDefault();
                    if (monthlyValue == null)
                    {
                        FilteredExpenses.Add(new Expense() { Month = (Month)i, Amount = 0, ExpenseTypeId = type.Id });
                    }
                }
            }

            return FilteredExpenses.OrderBy(f => f.Month).ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeType>>> GetIncomeTypes()
        {
            return await _context.IncomeTypes.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseType>>> GetExpenseTypes()
        {
            return await _context.ExpenseTypes.ToListAsync();
        }
    }
}
