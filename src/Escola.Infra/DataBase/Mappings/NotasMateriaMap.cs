using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escola.Infra.DataBase.Mappings
{
    public class NotasMateriaMap: IEntityTypeConfiguration<NotasMateria>
    {
        public void Configure(EntityTypeBuilder<NotasMateria> builder)
        {
            builder.ToTable("NotasMateria");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

            builder.Property(x => x.Nota)
                    .HasColumnType("float")
                    .HasColumnName("Notas");

            builder.HasOne(x => x.Materia)
                    .WithMany( x => x.NotasMaterias)
                    .HasForeignKey( x=> x.MateriaId)
                    .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.Boletim)
                    .WithMany(x => x.Notas)
                    .HasForeignKey(x => x.BoletimId);
                    
        }
    }
}