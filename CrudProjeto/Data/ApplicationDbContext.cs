using Microsoft.EntityFrameworkCore;
using CrudProjeto.Model;

namespace CrudProjeto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Agencia> Agencia { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Conta> Conta { get; set; }

        public DbSet<Historico> Historico { get; set; }

        public DbSet<Investimento> Investimento { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Historico>()
                .HasKey(h => new { h.cpf_cliente, h.num_conta });
        
            modelBuilder.Entity<Investimento>()
                .HasKey(h => new { h.cpf_cliente, h.num_conta });

            base.OnModelCreating(modelBuilder);
        }
    }
}