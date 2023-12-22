using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.AccesoDatos.Repositorio;
using SistemaMerck.AccesoDatos.Repositorio.Interfaces;
using SistemaMerck.Helpers;
using SistemaMerck.Helpers.Interface;
using SistemaMerck.Modelos;
using SistemaMerck.Negocio.Interface;
using SistemaMerck.Negocio;
using SistemaMerck.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<MerckContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql")));



// builder.Services.AddScoped<ILocacionRepository, BaseDatosLocacionRepository>();
builder.Services.AddScoped<ILocacionRepository, ArchivoLocacionRepository>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var filePath = config.GetValue<string>("FileUrls:LocacionRepositoryUrl");
    return new ArchivoLocacionRepository(filePath);
});

builder.Services.AddScoped<LocacionService>();
builder.Services.AddTransient<ICorreoService, CorreoService>();
builder.Services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
builder.Services.AddScoped<IFormularioBusiness, FormularioBusiness>();



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Bienvenida}/{id?}");

app.Run();
