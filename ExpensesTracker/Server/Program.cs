global using ExpensesTracker.Shared;
global using ExpensesTracker.Shared.Models;
global using ExpensesTracker.Shared.Extensions;
global using Microsoft.EntityFrameworkCore;
global using ExpensesTracker.Server.Data;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); // finds the connection: DefaultConnection that we named in appsettings.json
builder.Services.AddSwaggerGen();

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
