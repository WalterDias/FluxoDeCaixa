using FluxoDeCaixa.Core.DataBase;
using FluxoDeCaixa.Core.Services;
using FluxoDeCaixa.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.API.ServicesExtension;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FluxoDeCaixaContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFluxoDeCaixaService, FluxoDeCaixaService>();

var app = builder.Build();

//Deixei essa parte aqui so para efeito demostrativo mas o comando da criação da database já deve ter sido realizado pelo docker compose
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FluxoDeCaixaContext>();
    db.Database.Migrate();
}

// configure exception middleware
app.UseStatusCodePages(async statusCodeContext
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
        .ExecuteAsync(statusCodeContext.HttpContext));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adicionado para fins educativos
app.UseSwagger();
app.UseSwaggerUI();

app.MapearRotasServicoFluxoDeCaixa();

app.UseHttpsRedirection();

app.Run();