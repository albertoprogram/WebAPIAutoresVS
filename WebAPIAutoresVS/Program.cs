using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebAPIAutoresVS.Filtros;
using WebAPIAutoresVS.Middlewares;
using WebAPIAutoresVS.Servicios;

namespace WebAPIAutoresVS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(opciones =>
            {
                opciones.Filters.Add(typeof(FiltroDeExcepcion));
            }).AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //builder.Services.AddSingleton(builder.Configuration);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

            builder.Services.AddTransient<IServicio, ServicioA>();
            //builder.Services.AddTransient<ServicioA>();

            builder.Services.AddTransient<ServicioTransient>();
            builder.Services.AddScoped<ServicioScoped>();
            builder.Services.AddSingleton<ServicioSingleton>();
            builder.Services.AddTransient<MiFiltroDeAccion>();
            builder.Services.AddHostedService<EscribirEnArchivo>();

            builder.Services.AddResponseCaching();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var servicioLogger = (ILogger<Program>)app.Services.GetService(typeof(ILogger<Program>));

            // Configure the HTTP request pipeline.
            //app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
            app.UseLoguearRespuestaHTTP();

            app.Map("/ruta1", app =>
            {
                app.Run(async contexto =>
                {
                    await contexto.Response.WriteAsync("Estoy interceptando la tubería");
                });
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
