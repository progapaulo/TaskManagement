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

        // Definindo a propriedade de Comentário
        builder.Property(c => c.Comment)
            .IsRequired()
            .HasMaxLength(500);

        // Relacionamento de Comentario com Tarefa (N:1)
        builder.HasOne(c => c.Tarefa)
            .WithMany(t => t.Comentarios)
            .HasForeignKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}