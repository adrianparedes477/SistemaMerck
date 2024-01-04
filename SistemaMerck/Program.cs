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
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));



// builder.Services.AddScoped<ILocacionRepository, BaseDatosLocacionRepository>();
builder.Services.AddScoped<ILocacionRepository, ArchivoLocacionRepository>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var filePath = config.GetValue<string>("FileUrls:LocacionRepositoryUrl");
    return new ArchivoLocacionRepository(filePath);
});

builder.Services.AddDistributedMemoryCache(); // Puedes cambiar a otro proveedor de caché si es necesario

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Puedes ajustar el tiempo de expiración según tus necesidades
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<LocacionService>();
builder.Services.AddTransient<ICorreoService, CorreoService>();
builder.Services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
builder.Services.AddScoped<IFormularioBusiness, FormularioBusiness>();
builder.Services.AddHttpContextAccessor();



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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Administrador}/{action=Login}/{id?}");

IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "..\\Rotativa\\Windows\\");

app.Run();
