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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


//Add cookies
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // rules for validating incoming token:
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  //is server that created token trusted?
            ValidateAudience = true,    //was token generated for this specific application?
            ValidateLifetime = true,    //is token expired?
            ValidateIssuerSigningKey = true,    //check if token was signed with trusted key

            // set the expected issuer and audience values from the configuration file
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // define security key used to verify token signature
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!))
        };

        // custom logic that makes server to search for token in cookies!
        options.Events = new JwtBearerEvents
        {
            // trigerred before token is validated 
            OnMessageReceived = context =>
            {
                // instruct the middleware to read the token from a cookie instead of the standard authorization header
                context.Token = context.Request.Cookies["AuthToken"];
                return Task.CompletedTask;
            }
        };
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

    // Seeder
    var userSeeder = scope.ServiceProvider.GetRequiredService<IUserSeeder>();
    await userSeeder.Seed();
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