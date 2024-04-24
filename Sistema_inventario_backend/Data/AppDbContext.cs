using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sistema_inventario_backend.Models;


namespace Sistema_inventario_backend.Data
{
    public class AppDbContext: DbContext
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Proveedor>? Proveedores { get; set; }
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<EntradaInventario>? EntradasInventario { get; set; }
        public DbSet<SalidaInventario>? SalidasInventario { get; set; }
        public DbSet<Sucursal>? Sucursales { get; set; }
        public DbSet<Transaccion>? Transacciones { get; set; }

 
    }
}
