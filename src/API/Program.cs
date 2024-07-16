using FluxoDeCaixa.Core.DataBase;
using FluxoDeCaixa.Core.Services;
using FluxoDeCaixa.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.API.ServicesExtension;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FluxoDeCaixaContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFluxoDeCaixaService, FluxoDeCaixaService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

var app = builder.Build();

//Deixei essa parte aqui so para efeito demostrativo mas o comando da criação da database já deve ter sido realizado pelo docker compose,
// Se quiser rodar localmente para debugar não esqueça de apotar para o seu banco de dados
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FluxoDeCaixaContext>();
    db.Database.Migrate();
}

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        context.Response.ContentType = Text.Plain;

        await context.Response.WriteAsync("Um erro ocorreu no servidor.");

        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        var innerDetails = exceptionHandlerPathFeature?.Error?.InnerException;

        //So para ver o detalhe do erro.
        if (innerDetails is not null)
            await context.Response.WriteAsync($" {innerDetails.Message}");
        
    });
});

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
app.MapearRotasServicoRelatorio();

app.UseHttpsRedirection();

app.Run();