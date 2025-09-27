using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using ParkingSystem.DBSettings;
using ParkingSystem.Models;
using ParkingSystem.Models.Logger;
using ParkingSystem.Repository;
using ParkingSystem.Services;
using Services;

var builder = WebApplication.CreateBuilder(args);

// ------------------ MongoDB Setup ------------------
// ------------------ MongoDB Setup ------------------
builder.Services.Configure<ParkingDatabaseSetting>(
    builder.Configuration.GetSection(nameof(ParkingDatabaseSetting)));

// Register IParkingDatabase as the concrete class
builder.Services.AddSingleton<IParkingDatabase>(sp =>
    sp.GetRequiredService<IOptions<ParkingDatabaseSetting>>().Value);

// Register MongoClient using the connection string from ParkingDatabaseSetting
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var dbSettings = sp.GetRequiredService<IParkingDatabase>();
    return new MongoClient(dbSettings.ConnectionString);
});

// ------------------ Repository & Services ------------------
builder.Services.AddScoped<IParkingRepository, ParkingRepository>();
builder.Services.AddScoped<IParkingServices, ParkingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

// ------------------ Logging ------------------
builder.Services.AddLogging();
builder.Logging.AddProvider(new SimpleFileLoggerProvider("Log/log.txt"));

// ------------------ JWT Authentication ------------------
var jwtSetting = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSetting["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSetting["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSetting["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// ------------------ Swagger ------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Parking API", Version = "v1" });

    // JWT Authentication for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\r\nExample: \"Bearer eyJhbGci...\"",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new String[]{}
        }
    });
});

var app = builder.Build();

// ------------------ Middleware ------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Must be before Authorization
app.UseAuthorization();

app.MapControllers();
try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}