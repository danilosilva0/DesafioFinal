using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Repository.Map
{
    public class AgendamentoMap : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("tb_agendamento");

            builder.HasKey(e => e.IdAgendamento);

            builder.Property(e => e.IdAgendamento)
                   .HasColumnName("id_agendamento")
                   .IsRequired();

            builder.Property(e => e.IdPaciente)
                   .HasColumnName("id_paciente")
                   .IsRequired();

            builder.Property(e => e.DataAgendamento)
                   .HasColumnName("data_agendamento")
                   .IsRequired();

            builder.Property(e => e.HoraAgendamento)
                   .HasColumnName("hora_agendamento")
                   .IsRequired();

            builder.Property(e => e.Status)
                   .HasColumnName("status")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(e => e.DataCriacao)
                   .HasColumnName("data_criacao")
                   .IsRequired();

            builder.HasOne(e => e.Paciente)
                   .WithMany(b => b.Agendamentos)
                   .HasForeignKey(e => e.IdPaciente);
        }
    }
}