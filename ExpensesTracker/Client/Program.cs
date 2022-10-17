global using ExpensesTracker.Client.Services.MonthlyExpService;
global using ExpensesTracker.Shared.Models;
global using ExpensesTracker.Shared.Extensions;
global using ExpensesTracker.Shared;
global using ExpensesTracker.Client.Services.FileService;
using ExpensesTracker.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IMonthlyExpService, MonthlyExpService>(); // whenever someone wants to inject IME, then we will use the ME implementation
builder.Services.AddScoped<IFileService, FileService>();
await builder.Build().RunAsync();
