using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

public class StoreWareSQLite : DbContext
{
    public StoreWareSQLite(DbContextOptions<StoreWareSQLite> options) : base(options) { }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Transaccion> Transacciones { get; set; }
    // ... demás DbSets

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if(!options.IsConfigured)
            options.UseSqlite("Data Source=localstore.db");
        //Evite que pase un error si no se configura la cadena de conexión.
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Productos
        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasColumnType("decimal(18,2)");
        // Configura relación Proveedor → Administrador
        modelBuilder.Entity<Proveedor>()
            .HasOne(p => p.Administrador)
            .WithMany() // Si Administrador no tiene colección de Proveedores
            .HasForeignKey(p => p.AdministradorId)
            .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada

        // Configura relación Producto → Proveedor
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Proveedor)
            .WithMany(prov => prov.Productos)
            .HasForeignKey(p => p.ProveedorId);
        //Usuarios
        modelBuilder.Entity<Usuario>()
        .HasMany(u => u.Transacciones)
        .WithOne(t => t.Usuario)
        .HasForeignKey(t => t.UsuarioId);
        // Transacciones
        modelBuilder.Entity<Pago>()
        .HasMany(p => p.Transacciones)
        .WithOne(t => t.Pago)
        .HasForeignKey(t => t.PagoId)
        .OnDelete(DeleteBehavior.Cascade);

    }
}