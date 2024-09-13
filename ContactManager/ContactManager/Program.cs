using ContactManager.Core.Interfaces;
using ContactManager.Data.Persistence;
using ContactManager.Data.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
