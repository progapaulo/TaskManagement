using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.ORM.Mapping;

public class ComentarioConfiguration : IEntityTypeConfiguration<Comments>
{
    public void Configure(EntityTypeBuilder<Comments> builder)
    {
        builder.ToTable("Comentarios");

        // Definindo a chave primária
        builder.HasKey(c => c.Id);

        builder.Property(c => c.DueDate)
            .IsRequired();
        
        // Definindo a propriedade de Comentário
        builder.Property(c => c.Comment)
            .IsRequired()
            .HasMaxLength(1000);

        // Relacionamento com a Task
        builder.HasOne(c => c.Tarefa)  // Cada Comment pertence a uma Task
            .WithMany(t => t.Comentarios)  // Uma Task pode ter muitos Comments
            .HasForeignKey(c => c.TTaskId)  // Chave estrangeira no lado do Comment
            .OnDelete(DeleteBehavior.Cascade);  // Quando a Task for excluída, excluir os Comments associados
    }
}