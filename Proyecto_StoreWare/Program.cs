using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Data.Repositories;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;
using Proyecto_StoreWare.Repositories;

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

var app = builder.Build();

// Middleware para desarrollo (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});


app.Run();
