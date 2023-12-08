using BecarioAPI.Models;
using BecarioAPI.Models.Solicitantes;
using BecarioAPI.Models.Solicitudes;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

#region Nlog Service

var logger = LogManager.Setup().
    LoadConfigurationFromAppSettings().
    GetCurrentClassLogger();

logger.Debug("Init main");
#endregion

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    //Realizamos la conexion a la base de datos
    builder.Services.AddDbContext<BecarioDBContext>(options =>
    {
        options.UseSqlServer("Data Source=nestormartell; Initial Catalog=BecarioDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    });
    //Inyeccion de dependencias
    builder.Services.AddScoped<ISolicitantesRepository, SolicitantesRepository>();
    builder.Services.AddScoped<ISolicitudesRepository, SolicitudesRepository>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Error en la aplicación");
    throw;
}
finally
{
    LogManager.Shutdown();
}
