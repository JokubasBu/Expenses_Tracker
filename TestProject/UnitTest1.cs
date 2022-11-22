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
            //Expense b = new Expense {
            //    Id = 400,
            //    Money = 500,
            //    Comment = "hi",
            //    Year = 2022,
            //    Month = 10,
            //    Day = 15,
            //    CategoryId = 2
            //};

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
            }
        }

        [Fact]
        public void Deleting()
        {
            ExpensesService a = new ExpensesService(http, navigationManager);
            //Expense b = new Expense
            //{
            //    Id = 400,
            //    Money = 500,
            //    Comment = "hi",
            //    Year = 2022,
            //    Month = 10,
            //    Day = 15,
            //    CategoryId = 2
            //};

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