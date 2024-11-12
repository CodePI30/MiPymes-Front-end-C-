//nos permite utilizar metodos de transferencia de datos usando el protocolo http
using MiPymes_Front_end_C_.Services;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace MiPymes_Front_end_C_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Agregar HttpClient para consumir la API
            builder.Services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/"); // URL del api
            });

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
            options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantiene la capitalización de las propiedades
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Si usas enumeraciones
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllers();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=MiPymes}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
