using AV2_Biblioteca_Final.Model.Models;
using AV2_BibliotecaDB_Final.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AV2_BibliotecaDB_Final.Areas.Identity.Data;
using AV2_Biblioteca_Final.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddScoped<RepositoryAutores>();
builder.Services.AddScoped<RepositoryLivros>();

builder.Services
    .AddDbContext<BibliotecaDBContext>(options => options.UseSqlServer("DefaultConnection"));

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDbContext<identity_context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<identity_context>();

builder.Services
    .AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
