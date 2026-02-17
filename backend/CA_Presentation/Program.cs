using Microsoft.EntityFrameworkCore;
using CA_Infrastructure;
using CA_Infrastructure.Extensions;
using CA_Infrastructure.DataSeeders;
using CA_Application.Extensions;
using CA_Application.DTOs;
using CA_Application;
using Middlewares;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


//Add cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "AuthToken";
        options.Cookie.HttpOnly = true;
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Host.UseSerilog((context, configuration) =>
    configuration
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
        .WriteTo.Console()
);

builder.Services.AddAutoMapper(typeof(DrinkProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseMiddleware<ErrorHandlingMiddleware>();


//Configure initialization od bd for docker
using (var scope = app.Services.CreateScope())
{
    // get DbContext
    var dbContext = scope.ServiceProvider.GetRequiredService<CA_Infrastructure.Database.MainDbContext>();
    // Use migration taht creates tables if they dont exist
    dbContext.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
//enable auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();