global using ExpensesTracker.Shared.Models;
global using ExpensesTracker.Shared.Extensions;
global using Microsoft.EntityFrameworkCore;
global using ExpensesTracker.Server.Repositories.Interfaces;
using ExpensesTracker.Server.Data;
using ExpensesTracker.Server.Data.Repositories;
using ExpensesTracker.Server.Services;
using ExpensesTracker.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFile(builder.Configuration.GetSection("LoggingSer"));
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); // finds the connection: DefaultConnection that we named in appsettings.json
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IExpense, ExpenseRepo>();
builder.Services.AddScoped<IIncome, IncomeRepo>();
builder.Services.AddScoped<IGoal, GoalRepo>();
builder.Services.AddScoped<ILoggerService, LoggerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
