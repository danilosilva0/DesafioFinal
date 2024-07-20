using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Repository.Map
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("tb_paciente");

            builder.HasKey(e => e.IdPaciente);

            builder.Property(e => e.IdPaciente)
                   .HasColumnName("id_paciente")
                   .IsRequired();

            builder.Property(e => e.Nome)
                   .HasColumnName("dsc_nome")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.DataNascimento)
                   .HasColumnName("dat_nascimento")
                   .IsRequired();

            builder.Property(e => e.DataCriacao)
                   .HasColumnName("dat_criacao")
                   .IsRequired();
        }
    }
}