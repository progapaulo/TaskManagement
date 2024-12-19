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

        // Relacionamento com o projeto (Task -> Project)
        builder.HasOne(t => t.Project)  // Cada Task pertence a um Project
            .WithMany(p => p.Tasks)  // Um Project pode ter muitas Tasks
            .HasForeignKey(t => t.ProjectId)  // Chave estrangeira no lado da Task
            .OnDelete(DeleteBehavior.Cascade);  // Quando o Project for excluído, excluir as Tasks associadas

        // Relacionamento com os comentários (Task -> Comment)
        builder.HasMany(t => t.Comentarios)  // Uma Task pode ter muitos Comments
            .WithOne(c => c.Tarefa)  // Cada Comment pertence a uma Task
            .HasForeignKey(c => c.TTaskId)  // Chave estrangeira no lado do Comment
            .OnDelete(DeleteBehavior.Cascade);  // Quando a Task for excluída, excluir os Comments associados
    }
}