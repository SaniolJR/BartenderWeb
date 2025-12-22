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

builder.Services.AddAutoMapper(typeof(DrinkProfile).Assembly);

var app = builder.Build();

// Seedowanie bazy (opcjonalnie, tylko na start)
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IUserSeeder>();
    await seeder.Seed();
}

// Endpointy (przykład)
app.MapPost("/drinks", async (AddDrinkDTO dto, IDrinkService service) =>
{
    var drink = await service.AddDrinkAsync(dto);
    return Results.Created($"/drinks/{drink.Id}", drink);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();