using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Micrq.Infra.IoC;
using MicRq.Banking.Data.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddDbContext<BankingDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("BnkingDbConnection"));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
DependencyContainer.RegisterServices(builder.Services);

var app = builder.Build();
 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=> {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","banking microservice V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
 


app.Run();
