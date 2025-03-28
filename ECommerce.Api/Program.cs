using ECommerce.Application;
using ECommerce.Application.Abstractions;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.DependencyInjections;
using ECommerce.Infrastructure.Seeders;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); Console.WriteLine($"Connection String from Configuration: {builder.Configuration.GetConnectionString("DefaultConnection")}");

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<DatabaseScriptManager>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

var origin = builder.Configuration.GetValue<string>("Config:CORSOriginPath");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins(origin) // Angular URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); // If using cookies/auth
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors("AllowAngular"); // Apply CORS policy
    app.UseDeveloperExceptionPage(); // Add this for detailed error messages
    app.UseStaticFiles();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    dbContext.Database.Migrate();
    var scriptManager = scope.ServiceProvider.GetRequiredService<DatabaseScriptManager>();
    scriptManager.RunDbScripts();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();