using IntraVision_Demo.DBConnector;
using IntraVision_Demo.Interfaces;
using IntraVision_Demo.Models;
using IntraVision_Demo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVendingMachineService, VendingMachineService>();
builder.Services.AddScoped<IRepository<VendingMachine>, VendingMachineRepository>();
builder.Services.AddScoped<DBMyContext>();
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<DBMyContext>(options => options.UseNpgsql(connection));
builder.Services.Configure<VendingMachineSettings>(builder.Configuration.GetSection("VendingMachineSettings"));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
