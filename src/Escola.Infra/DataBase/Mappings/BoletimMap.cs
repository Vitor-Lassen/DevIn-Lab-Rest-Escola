using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escola.Infra.DataBase.Mappings
{
    public class BoletimMap : IEntityTypeConfiguration<Boletim>
    {
        public void Configure(EntityTypeBuilder<Boletim> builder)
        {
            builder.ToTable("Boletim");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

            builder.Property(x => x.Faltas)
                    .HasColumnType("int")
                    .HasColumnName("Faltas");

            builder.Property(x => x.Periodo)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .HasColumnName("Periodo");
            
            builder.HasOne( x => x.Aluno)
                    .WithMany(x => x.Boletins)
                    .HasForeignKey( x=> x.AlunoId);

            builder.HasMany( x => x.Notas)
                    .WithOne( x => x.Boletim);
                    
                    

        }
        
    }
}