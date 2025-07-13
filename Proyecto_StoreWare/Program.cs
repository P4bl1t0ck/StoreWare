using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con cadena de conexión (ejemplo con SQL Server)
/*builder.Services.AddDbContext<StoreWare>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));*/


builder.Services.AddDbContext<StoreWare>(options =>
{
#if ANDROID || IOS
    options.UseSqlite("Filename=storeware.db");
#else
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
#endif
});

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
