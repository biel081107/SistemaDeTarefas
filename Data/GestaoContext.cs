using Microsoft.EntityFrameworkCore;
using _2MVC.Models;

public class GestaoContext : DbContext
{
    public GestaoContext(DbContextOptions<GestaoContext> options) : base(options)
    {
    }

    public DbSet<Projeto> Projetos { get; set; } = null!;
    public DbSet<Tarefa> Tarefas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Projeto>(
            entity =>
            {
                entity.HasKey(e => e.IdProjeto);
                entity.Property(e => e.DataFim).HasColumnType("datetime");
                entity.Property(e => e.DataInicio).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.Descricao).HasMaxLength(500);
                entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Status).HasDefaultValue(0).IsRequired();
            }
        );

        modelBuilder.Entity<Tarefa>(
            entity =>
            {
                entity.HasKey(e => e.IdTarefa);
                entity.Property(e => e.DataEntrega).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.Descricao).HasMaxLength(500);
                entity.Property(e => e.Titulo).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Status).HasDefaultValue(0).IsRequired();
            }
        );

        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Projeto)
            .WithMany(p => p.Tarefas)
            .HasForeignKey(t => t.IdProjeto)
            .OnDelete(DeleteBehavior.Cascade);
    }
}