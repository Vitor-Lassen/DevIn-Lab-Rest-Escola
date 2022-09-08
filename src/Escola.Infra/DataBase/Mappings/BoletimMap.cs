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
            builder.ToTable("BOLETIM");

            builder.HasKey(x => x.Id)
                   .HasName("PK_BoletimID");

            builder.Property(x => x.Id)
                    .HasColumnName("ID")
                    .HasColumnType("uniqueidentifier");

            builder.Property(x => x.order_date)
            .HasColumnName("DATA DE PEDIDO")
            .HasColumnType("DATE");

            /* buildere.Entity<>() //o exame
            .HasOne<Cliente>(e => e.Cliente) //possui 1 do tipo cliente - dentro do exame oq representa o cliente
            .WithMany(c => c.Exames) //possui N do tipo exame - dentro do cliente oq representa o exame - navegação inversa
            .HasForeignKey(e => e.ClienteId) //chave estrangeira da relação - exame que possui a fk */


        }
    }
}