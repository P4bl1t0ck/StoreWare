using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Data.Repositories;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;
using Proyecto_StoreWare.Repositories;

using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

    // Limpiar tablas existentes (opcional)
    db.Database.ExecuteSqlRaw("DELETE FROM Pagos");
    db.Database.ExecuteSqlRaw("DELETE FROM Usuarios");
    db.Database.ExecuteSqlRaw("DELETE FROM Productos");
    db.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence");

    // Seed Administrador
    if (!db.Set<Administrador>().Any())
    {
        db.Set<Administrador>().Add(new Administrador
        {
            Nombre = "Admin Demo",
            Email = "admin@demo.com",
            Contraseña = "123456"
            // Se eliminó Activo
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
            // Se eliminó Activo
        });
        db.SaveChanges();
    }

    var proveedorId = db.Set<Proveedor>().Select(p => p.Id).First();

    // Seed Productos
    if (!db.Set<Producto>().Any())
    {
        db.Set<Producto>().AddRange(
            new Producto
            {
                Nombre = "Laptop Gamer",
                Descripcion = "Laptop con RTX 4060 y 16GB RAM",
                Precio = 1500.00m,
                Stock = 10,
                Categoria = "Computadoras",
                ProveedorId = proveedorId,
                AdminId = adminId
                // Se eliminó Activo
            },
            new Producto
            {
                Nombre = "Mouse Inalámbrico",
                Descripcion = "Óptico Bluetooth",
                Precio = 25.50m,
                Stock = 100,
                Categoria = "Periféricos",
                ProveedorId = proveedorId,
                AdminId = adminId
                // Se eliminó Activo
            },
            new Producto
            {
                Nombre = "Teclado Mecánico",
                Descripcion = "RGB y Anti-Ghosting",
                Precio = 80.00m,
                Stock = 50,
                Categoria = "Periféricos",
                ProveedorId = proveedorId,
                AdminId = adminId
                // Se eliminó Activo
            },
            new Producto
            {
                Nombre = "Monitor 27\"",
                Descripcion = "4K UHD 60Hz",
                Precio = 300.00m,
                Stock = 20,
                Categoria = "Monitores",
                ProveedorId = proveedorId,
                AdminId = adminId
                // Se eliminó Activo
            }
        );
        db.SaveChanges();
    }

    // Seed Usuarios
    if (!db.Set<Usuario>().Any())
    {
        db.Set<Usuario>().AddRange(
            new Usuario
            {
                Nombre = "Pablo Montalvo",
                Email = "pablo.montalvo@example.com",
                Contraseña = "1234",
                Direccion = "Av. Principal 123",
                Telefono = "0987654321"
                // Se eliminó Activo
            },
            new Usuario
            {
                Nombre = "Ana Lopez",
                Email = "ana.lopez@example.com",
                Contraseña = "abcd",
                Direccion = "Calle Secundaria 456",
                Telefono = "0998765432"
                // Se eliminó Activo
            },
            new Usuario
            {
                Nombre = "Carlos Perez",
                Email = "carlos.perez@example.com",
                Contraseña = "xyz987",
                Direccion = "Av. Central 789",
                Telefono = "0976543210"
                // Se eliminó Activo
            }
        );
        db.SaveChanges();
    }

    // Seed Pagos
    if (!db.Set<Pago>().Any())
    {
        db.Set<Pago>().AddRange(
            new Pago
            {
                Nombre = "Pago #1",
                Tipo = "Tarjeta Crédito"
                // Se eliminó Activo
            },
            new Pago
            {
                Nombre = "Pago #2",
                Tipo = "PayPal"
                // Se eliminó Activo
            },
            new Pago
            {
                Nombre = "Pago #3",
                Tipo = "Tarjeta Débito"
                // Se eliminó Activo
            }
        );
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

app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000") 
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
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