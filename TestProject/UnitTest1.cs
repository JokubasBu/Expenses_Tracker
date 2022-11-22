using Bunit;
using ExpensesTracker.Client.Pages;
using ExpensesTracker.Client.Services.ExpensesService;
using ExpensesTracker.Client.Services.IncomesService;
using ExpensesTracker.Server.Data;
using ExpensesTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace TestProject
{
    public class UnitTest1 : TestContext
    {
        private readonly DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DataContext")
                .Options;

        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;
        Expense b = new Expense
        {
            Id = 400,
            Money = 500,
            Comment = "hi",
            Year = 2022,
            Month = 10,
            Day = 15,
            CategoryId = 2
        };

        [Fact]
        public void Adding()
        {
            ExpensesService a = new ExpensesService(http, navigationManager);

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new DataContext(options))
            {
                context.AllExpenses.Add(b);
                context.SaveChanges();
            }
            using (var context = new DataContext(options))
            {
                Assert.NotNull(a.GetSingleExpense(400));
                Assert.False(b.Equals(a.GetSingleExpense(12)));
                
                //var actionResult = await a.GetSingleExpense(400);
                //Assert.True(b.Equals(actionResult));
                //Assert.Equal(b, (a.GetSingleExpense(400).Result as CreatedAtActionResult).Value);
                //Assert.Equal(b, (a.GetSingleExpense(400).Result));
            }
        }

        //[Fact]
        //public void GetExp()
        //{
        //    ExpensesService a = new ExpensesService(http, navigationManager);
        //    Expense c = new Expense
        //    {
        //        Id = 2,
        //        Money = 50,
        //        Comment = "hello",
        //        Year = 2021,
        //        Month = 11,
        //        Day = 5,
        //        CategoryId = 3
        //    };
        //    Expense d = new Expense
        //    {
        //        Id = 5,
        //        Money = 10,
        //        Comment = "hey",
        //        Year = 2020,
        //        Month = 1,
        //        Day = 8,
        //        CategoryId = 1
        //    };

        //    using (var context = new DataContext(options))
        //    {
        //        context.Database.EnsureCreated();
        //    }

        //    using (var context = new DataContext(options))
        //    {
        //        //context.AllExpenses.Add(b);
        //        context.AllExpenses.Add(c);
        //        context.AllExpenses.Add(d);
        //        context.SaveChanges();
        //    }
        //    using (var context = new DataContext(options))
        //    {
        //        var result = a.GetExpenses();
        //        //Assert.Contains(result, (IEnumerable<Task>)c); // casting issue
        //    }
        //}

        [Fact]
        public void Deleting()
        {
            ExpensesService a = new ExpensesService(http, navigationManager);
            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new DataContext(options))
            {
                context.AllExpenses.Remove(b);
                context.SaveChanges();
            }
            using (var context = new DataContext(options))
            {
                Assert.True(a.GetSingleExpense(400).IsFaulted);

                //Assert.Equal(404, a.GetSingleExpense(400).Status);
                //Assert.IsType<StatusCodes.Status404NotFound>(a.GetSingleExpense(400));
                //Assert.IsType<NotFoundObjectResult>(a.GetSingleExpense(400));
            }
        }
    }
}