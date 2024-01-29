using HousekeepingBook.DbContexts;
using HousekeepingBook.Interfaces;
using HousekeepingBook.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Enable Cors
builder.Services.AddCors( c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Json Serializer
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IInvoiceRepository, SQLInvoiceRepository>();
builder.Services.AddScoped<IMonthlyInvoiceSummaryRepository, SQLMonthlyInvoiceSummaryRepository>();
builder.Services.AddScoped<IStoreRepository, SQLStoreRepository>();
builder.Services.AddScoped<ISettingRepository, SQLSettingRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Enable Cors
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
