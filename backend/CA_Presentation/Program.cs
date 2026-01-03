using Microsoft.EntityFrameworkCore;
using CA_Infrastructure;
using CA_Infrastructure.Extensions;
using CA_Infrastructure.DataSeeders;
using CA_Application.Extensions;
using CA_Application.DTOs;
using CA_Application;


var builder = WebApplication.CreateBuilder(args);

// rejestracja serwisów z Application i Infrastructure
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(DrinkProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();