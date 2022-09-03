using Escola.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.Infra.DataBase.Mappings
{
    public class MateriaMap: IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.ToTable("Materia");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                    .HasColumnType("varchar")
                    .HasMaxLength(80)
                    .HasColumnName("Nome");

        }
    }
}
