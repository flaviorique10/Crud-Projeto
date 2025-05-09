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
            // Configuração das tabelas
            modelBuilder.Entity<Historico>().ToTable("Historicos");
            modelBuilder.Entity<Investimento>().ToTable("Investimentos");

            // Chave primária composta para Historico
            modelBuilder.Entity<Historico>()
                .HasKey(h => new { h.cpf_cliente, h.num_conta });

            // Relacionamento com Cliente e Conta
            modelBuilder.Entity<Historico>()
                .HasOne(h => h.Cliente)
                .WithMany()
                .HasForeignKey(h => h.cpf_cliente)
                .OnDelete(DeleteBehavior.Restrict); // Evita cascata se o cliente for deletado

            modelBuilder.Entity<Historico>()
                .HasOne(h => h.Conta)
                .WithMany()
                .HasForeignKey(h => h.num_conta)
                .OnDelete(DeleteBehavior.Restrict);

            // Chave primária composta para Investimento
            modelBuilder.Entity<Investimento>()
                .HasKey(i => new { i.cpf_cliente, i.num_conta });

            // Relacionamento com Cliente e Conta
            modelBuilder.Entity<Investimento>()
                .HasOne(i => i.Cliente)
                .WithMany()
                .HasForeignKey(i => i.cpf_cliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Investimento>()
                .HasOne(i => i.Conta)
                .WithMany()
                .HasForeignKey(i => i.num_conta)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}