using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto_StoreWare.Models;

    public class StoreWare : DbContext
    {
        public StoreWare (DbContextOptions<StoreWare> options)
            : base(options)
        {
        }

        public DbSet<Proyecto_StoreWare.Models.Usuario> Usuario { get; set; } = default!;

public DbSet<Proyecto_StoreWare.Models.Transaccion> Transaccion { get; set; } = default!;

public DbSet<Proyecto_StoreWare.Models.Proveedor> Proveedor { get; set; } = default!;

public DbSet<Proyecto_StoreWare.Models.Producto> Producto { get; set; } = default!;

public DbSet<Proyecto_StoreWare.Models.Pago> Pago { get; set; } = default!;

public DbSet<Proyecto_StoreWare.Models.Administrador> Administrador { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuraciones específicas para SQLite
        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasColumnType("decimal(18,2)"); // Soporte para decimal

        // Configura relaciones si es necesario
        modelBuilder.Entity<Transaccion>()
            .HasOne(t => t.Producto)
            .WithMany(p => p.Transacciones)
            .HasForeignKey(t => t.ProductoId);
    }
}
