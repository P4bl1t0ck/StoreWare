using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Data.Repositories;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;
using Proyecto_StoreWare.Repositories;

using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con cadena de conexión (ejemplo con SQL Server)
/*builder.Services.AddDbContext<StoreWare>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));*/

/*//I was triying to use SQLite for local development, but it was not working as expected.
 * perhaps it was not configured correctly.
 * Please, just ignore this code snippet, and use the one below.
builder.Services.AddDbContext<StoreWare>(options =>
{
#if ANDROID || IOS
    options.UseSqlite("Filename=storeware.db");
#else
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
#endif
});*/

// SQL Server 
builder.Services.AddDbContext<StoreWare>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// SQLite
builder.Services.AddDbContext<StoreWareSQLite>(options =>
    options.UseSqlite("Data Source=StoreWareLocal.db"));

// Añadir servicios de la aplicación
// Registra los repositorios
builder.Services.AddScoped<IProductoService, ProductoRepository>();
builder.Services.AddScoped<IVentaService, VentaRepositorie>();
builder.Services.AddScoped<IUsuarioService, UsuarioRepositorie>();
// Añadir controladores
builder.Services.AddControllers();

// Configurar Swagger (opcional para probar API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de autenticación JWT

builder.Services.AddScoped<IUsuarioService, UsuarioRepositorie>();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;
    var db = sp.GetRequiredService<StoreWareSQLite>();
    db.Database.EnsureCreated();

    // Seed Admin
    if (!db.Set<Administrador>().Any())
    {
        db.Set<Administrador>().Add(new Administrador
        {
            Nombre = "Admin Demo",
            Email = "admin@demo.com",
            Contraseña = "123456"
        });
        db.SaveChanges();
    }

    var adminId = db.Set<Administrador>().Select(a => a.Id).First();

    // Seed Proveedor
    if (!db.Set<Proveedor>().Any())
    {
        db.Set<Proveedor>().Add(new Proveedor
        {
            AdministradorId = adminId,
            Nombre = "Proveedor Demo",
            Email = "proveedor@demo.com",
            Telefono = "+593000000000"
        });
        db.SaveChanges();
    }
}

app.UseCors("AllowAll");

// Middleware para desarrollo (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Si más adelante configuras certificado puedes reactivarlo fuera de dev:
// if (!app.Environment.IsDevelopment())
// {
//     app.UseHttpsRedirection();
// }

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
/*
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});
*/
app.Run();