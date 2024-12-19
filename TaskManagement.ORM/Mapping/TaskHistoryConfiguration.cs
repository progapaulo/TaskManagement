using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.ORM.Mapping;

public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
{
    public void Configure(EntityTypeBuilder<TaskHistory> builder)
    {
        builder.ToTable("TaskHistories");

        builder.HasKey(th => th.Id);

        builder.Property(th => th.ChangeDetails)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(th => th.ChangeDate)
            .IsRequired();

        // Relacionamento de Historico com Tarefa (N:1)
        builder.HasOne(th => th.Tasks)
            .WithMany(t => t.History)
            .HasForeignKey(th => th.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}