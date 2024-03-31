using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ATM> ATMs { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<TipoTransaccion> TipoTransaccions { get; set; }
        public DbSet<Transaccion> Transaccions { get; set; }
        public DbSet<CreditoTipo> CreditosTipo { get; set; }
        public DbSet<CreditoPlazo> CreditosPlazos { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<CreditoPagos> CreditosPagos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
