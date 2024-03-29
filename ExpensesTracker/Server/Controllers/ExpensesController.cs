﻿using ExpensesTracker.Client.Pages;
using ExpensesTracker.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static ExpensesTracker.Shared.Extensions.Delegates;
using static System.Net.WebRequestMethods;

namespace ExpensesTracker.Server.Controllers
{
    [Route("api/[controller]")] // the route will be taken from ExpensesController and it will read everything that is before controller
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpense _expenses;
        static int currentCount = 0; // amomunt of times the button Order was pressed

        private static int _year;
        private static int _month;
        private static int _categoryId;


        public ExpensesController(IExpense _expenses)
        {
            this._expenses = _expenses;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<List<ExpenseSummary>>> GetSummary() 
        {
            var summary = new List<ExpenseSummary>();
            var categories = await _expenses.GetCategoriesAsync();
            List<Expense> allExpenses = await _expenses.GetExpensesAsync();

            allExpenses = allExpenses.FilterBy(year: DateTime.Now.Year);
            allExpenses = allExpenses.FilterBy(month: DateTime.Now.Month);

            foreach (Category category in categories)
            {
                ExpenseSummary temp = new ExpenseSummary();
                temp.totalExpenses = 0;
                foreach (Expense expense in allExpenses)
                {
                    if (expense.CategoryId == category.Id)
                    {
                        temp.totalExpenses += expense.Money;
                    }
                }
                if (temp.totalExpenses > 0)
                {
                    summary.Add(new ExpenseSummary() { category = category.Title, totalExpenses = temp.totalExpenses });
                }
            }

            return Ok(summary);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<List<Statistic>>> GetStatistics()
        {
            var stats = new Statistic();
            List<Expense> allExpenses = await _expenses.GetExpensesAsync();
            StatsExpense del = CalculateExpense;

            allExpenses = allExpenses.FilterBy(year: DateTime.Now.Year);
            stats.yearStat = del(allExpenses);

            allExpenses = allExpenses.FilterBy(month: DateTime.Now.Month-1);
            stats.monthStat = del(allExpenses);

            return Ok(stats);
        }

        [HttpGet] 
        public async Task<ActionResult<List<Expense>>> GetExpenses()
        {
            return Ok(await GetFilteredExpenses()); 
        }

        [HttpGet("currentCount")] // http methods should all be different, otherwise: The request matched multiple endpoints
        public async Task<ActionResult<List<Expense>>> GetOrderedExpenses()
        {
            currentCount++;
            return Ok(await GetFilteredExpenses());
        }

        [HttpPost]
        public async Task<ActionResult<List<Expense>>> ShowFilter(Expense expenseFilter)
        {
            _categoryId = expenseFilter.CategoryId;
            _month =expenseFilter.Month;
            _year =expenseFilter.Year;

            return Ok(await GetFilteredExpenses());

        }


        [HttpGet("categories")] 
        public async Task<ActionResult<List<Category>>> GetCategories() 
        {
            return Ok(await _expenses.GetCategoriesAsync()); 
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Expense>> GetSingleExpense(int id) 
        {
            var expense = await _expenses.GetSingleExpenseAsync(id);

            if (expense == null) 
            {
                return NotFound("no entry..."); 
            }
            return Ok(expense.Value);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<List<Expense>>> CreateExpense(Expense expense)
        {
            await _expenses.CreateExpenseAsync(expense);
            return Ok(await GetFilteredExpenses());
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Expense>>> DeleteExpense(int id)
        {
            var dbExpense = await _expenses.GetSingleExpenseAsync(id);
            if (dbExpense == null)
                return NotFound("There is no such expense :/");

            await _expenses.DeleteExpenseAsync(dbExpense.Value);

            return Ok(await GetFilteredExpenses());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Expense>>> UpdateExpense(Expense expense, int id)
        {
            var dbExpense = await _expenses.GetSingleExpenseAsync(id);
            if (dbExpense == null)
                return NotFound("Sorry, but no expense for you. :/");

            await _expenses.UpdateExpenseAsync(expense, dbExpense.Value);

            return Ok(await GetFilteredExpenses());
        }

        async Task<List<Expense>> GetFilteredExpenses()
        {
            var expenses = await _expenses.GetExpensesAsync();

            var currentExpenses = expenses.FilterBy(id: _categoryId);
            currentExpenses = currentExpenses.FilterBy(month: _month);
            currentExpenses = currentExpenses.FilterBy(year: _year);

            currentExpenses.Sort(); //ascending
            if (currentCount % 2 == 0)
            {
                currentExpenses.Reverse(); //descending (have to use sort beforehand for reverse to work)
            }

            return currentExpenses;
        }

    }
}
