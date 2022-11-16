global using ExpensesTracker.Client.Services.ExpensesService;
global using ExpensesTracker.Client.Services.FileService;
global using ExpensesTracker.Client.Services.IncomesService;
global using ExpensesTracker.Shared.Models;
global using ExpensesTracker.Shared.Extensions;
global using ExpensesTracker.Shared;
using ExpensesTracker.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IExpensesService, ExpensesService>(); // whenever someone wants to inject IME, then we will use the ME implementation
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IIncomesService, IncomesService>();
await builder.Build().RunAsync();
