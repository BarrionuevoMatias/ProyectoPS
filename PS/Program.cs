using System;
using Infraestructura;
using Infraestructura.datos;
using Aplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Aplicacion.commands.Proposals;
using MediatR;


using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddInfrastructure(context.Configuration);
    services.AddApplication();


});


var app = builder.Build();

// Ejecutar migraciones al inicio
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        // Prueba simple de conexión (no requiere que existan tablas)
        var canConnect = dbContext.Database.CanConnect();
        Console.WriteLine(canConnect
            ? "Conexión exitosa a la base de datos"
            : "❌ No se pudo conectar a la base de datos");

        // Alternativa: Ejecutar una consulta simple si la DB tiene tablas
        if (canConnect && dbContext.users.Any()) // Reemplaza "Users" con una tabla existente
        {
            Console.WriteLine("Conexión y consulta funcionando correctamente");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Error al conectar a la base de datos: {ex.Message}");
    }

}


app.Run();
