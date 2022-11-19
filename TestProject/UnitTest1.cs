using Bunit;
using Bunit.TestDoubles;
using ExpensesTracker.Client.Pages;
using ExpensesTracker.Client.Services.ExpensesService;
using ExpensesTracker.Client.Services.FileService;
using ExpensesTracker.Client.Services.IncomesService;
using ExpensesTracker.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject
{
    public class UnitTest1 : TestContext
    {
        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;
        private readonly IExpensesService expensesService;

        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void Find()
        {
            Services.AddSingleton<IIncomesService>(new IncomesService(http, navigationManager));

            // RenderComponent will inject the service in the component when it is instantiated and rendered.
            var smth = RenderComponent<SingleIncome>();
            smth.Find("h3").MarkupMatches("<h3>Income</h3>");

            // Assert that service is injected
            //Assert.NotNull(Instance.Forecasts);
        }

        [Fact]
        public void Navigation()
        {
            Services.AddSingleton<IIncomesService>(new IncomesService(http, navigationManager));

            var nav = Services.GetRequiredService<NavigationManager>();
            var smth = RenderComponent<SingleIncome>();

            smth.Find("button:nth-of-type(2)").MarkupMatches("<button type=\"submit\" class=\"btn btn-danger\" > Cancel</button>");
            
            // this line makes it crash?
            smth.Find("button:nth-of-type(2)").Click();


            Assert.Equal("https://localhost:7172/incomes", nav.Uri);

        }


    }
}