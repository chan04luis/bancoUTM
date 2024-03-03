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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=banco;Trusted_Connection=True;TrustServerCertificate=True;User Id=sa;Password=chan04luis;");
        }
    }
}
