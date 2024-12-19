using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.ORM.Mapping;

public class TaskConfiguration : IEntityTypeConfiguration<Tasks>
{
    public void Configure(EntityTypeBuilder<Tasks> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        builder.Property(t => t.Status)
            .IsRequired();

        builder.Property(t => t.Priority)
            .IsRequired();

        builder.Property(t => t.DueDate)
            .IsRequired();

        // Relacionamento de Tarefa com Projeto (N:1)
        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento de Tarefa com Historico (1:N)
        builder.HasMany(t => t.History)
            .WithOne(h => h.Tasks)
            .HasForeignKey(h => h.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Relacionamento de Tarefa com Comentarios (1:N)
        builder.HasMany(t => t.Comentarios)
            .WithOne(c => c.Tarefa)
            .HasForeignKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}