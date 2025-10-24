using Exemplo.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Exemplo.Persistence
{
    public class ExemploDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ExemploDbContext(DbContextOptions<ExemploDbContext> options) : base(options) { }
        
        public DbSet<ExemploModel> Exemplo { get; set; }

        public DbSet<UsuarioModel> Usuario { get; set; }

        public DbSet<ServicoModel> Servico { get; set; }

        public DbSet<SedeModel> Sede { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExemploModel>(entity =>
            {
                entity.ToTable("exemplo");
                entity.HasKey(p => p.Campo1);
                entity.Property(p => p.Campo1).ValueGeneratedOnAdd();
                entity.Property(p => p.Campo2).HasMaxLength(500).IsRequired();
                entity.Property(p => p.Campo3);
                entity.HasIndex(p => p.Campo1);
            });

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("usuario");
                entity.HasKey(p => p.Id);
                entity.Property(p=>p.Id).ValueGeneratedOnAdd();
                entity.Property(p=>p.Usuario).HasMaxLength(200).IsRequired();
                entity.Property(p=>p.SenhaHash).HasMaxLength(256).IsRequired();
                entity.Property(p => p.Nome).HasMaxLength(200).IsRequired();
                entity.Property(p => p.IsAdmin).IsRequired(true).HasDefaultValue(false);
                entity.HasIndex(p => p.Usuario).IsUnique();
            });

            modelBuilder.Entity<ServicoModel>(entity =>
            {
                entity.ToTable("servico");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Nome).HasMaxLength(200).IsRequired();
            });

            //Sede
            modelBuilder.Entity<SedeModel>(entity =>
            {
                entity.ToTable("sede");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Nome).HasMaxLength(200).IsRequired();
                entity.Property(p => p.DataInclusao).IsRequired();
                entity.Property(p => p.Ativo).HasDefaultValue(true);
            });
        }
    }
}
