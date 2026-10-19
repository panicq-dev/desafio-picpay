using DesafioPicPay.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioPicPay.Infra;

public class AplicacaoDbContext(DbContextOptions<AplicacaoDbContext> options) : DbContext(options)
{
    public DbSet<CarteiraEntidade> Carteiras => Set<CarteiraEntidade>();
    public DbSet<TransferenciaEntidade> Transferencias => Set<TransferenciaEntidade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CarteiraEntidade>(entity =>
        {
            entity.Property<int>("Id");
            entity.HasKey("Id");

            entity.HasIndex(c => c.CPFCNPJ)
                .IsUnique();
            entity.HasIndex(c => c.Email)
                .IsUnique();

            entity.Property(c => c.SaldoConta)
                .HasPrecision(18, 2);
            entity.Property(c => c.TipoEntidade)
                .HasConversion<string>();
        });

        modelBuilder.Entity<TransferenciaEntidade>(entity =>
        {
            entity.HasKey(t => t.IdTransferencia);

            entity.HasOne(t => t.CarteiraRemetente)
                .WithMany()
                .HasForeignKey(t => t.IdRemetente)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Transferencias_CarteiraRemetente");

            entity.HasOne(t => t.CarteiraDestinatario)
                .WithMany()
                .HasForeignKey(t => t.IdDestino)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Transferencias_CarteiraDestinatario");
        });
    }
}