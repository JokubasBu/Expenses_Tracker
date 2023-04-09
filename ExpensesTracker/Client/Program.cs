global using ExpensesTracker.Client.Services.ExpensesService;
global using ExpensesTracker.Client.Services.FileService;
global using ExpensesTracker.Client.Services.IncomesService;
global using ExpensesTracker.Client.Services.LoggingService;
global using ExpensesTracker.Shared.Models;
global using ExpensesTracker.Shared.Extensions;
global using ExpensesTracker.Shared;
using ExpensesTracker.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.Toast;
using ExpensesTracker.Client.Services.GoalService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddLogging(logging => {
    var httpClient = builder.Services.BuildServiceProvider().GetRequiredService<HttpClient>();
    logging.SetMinimumLevel(LogLevel.Error);
    //logging.ClearProviders(); //To not show debug information in browser console if exception is caught
    logging.AddProvider(new LoggerProvider(httpClient));
});
builder.Services.AddScoped<IExpensesService, ExpensesService>(); // whenever someone wants to inject IME, then we will use the ME implementation
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IIncomesService, IncomesService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddBlazoredToast();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
await builder.Build().RunAsync();
