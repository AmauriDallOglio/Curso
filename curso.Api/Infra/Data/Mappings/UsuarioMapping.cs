using curso.Api.Business.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace curso.Api.Infra.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIO");
            builder.HasKey(p => p.Codigo);
            builder.Property(p => p.Codigo).ValueGeneratedOnAdd();
            builder.Property(p => p.Email);
            builder.Property(p => p.Login);
            builder.Property(p => p.Senha);
        }
    }
}