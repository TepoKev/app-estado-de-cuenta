using backend.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<TarjetaDeCredito> TarjetasDeCredito { get; set; }  = null!;
    public DbSet<Compra> Compras { get; set; }  = null!;
    public DbSet<Pago> Pagos { get; set; }  = null!;
    public DbSet<Configuracion> Configuraciones { get; set; } = null!;
}