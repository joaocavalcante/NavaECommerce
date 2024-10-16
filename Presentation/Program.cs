using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar servi�os
builder.Services.AddControllers();

// Configurar o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar reposit�rios e servi�os
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<AutenticacaoService>(provider =>
{
    var repo = provider.GetRequiredService<IUsuarioRepository>();
    var jwtSecret = builder.Configuration["Jwt:Secret"];
    var jwtLifespan = int.Parse(builder.Configuration["Jwt:Lifespan"]);
    return new AutenticacaoService(repo, jwtSecret, jwtLifespan);
});

// Registrar OfertaService e HostedService
builder.Services.AddScoped<OfertaService>();
builder.Services.AddScoped<IOfertaRepository, OfertaRepository>(); // Certifique-se de ter implementado OfertaRepository
builder.Services.AddHostedService<BlackFridayHostedService>();

// Configurar autentica��o JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Ajuste conforme necess�rio
        ValidateAudience = false, // Ajuste conforme necess�rio
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adicione a autentica��o antes da autoriza��o
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
