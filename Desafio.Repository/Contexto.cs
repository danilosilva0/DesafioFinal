using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Repository
{
    public class Contexto : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure a conexão com o banco de dados
            optionsBuilder.UseSqlServer("sua_string_de_conexao_aqui");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("tb_paciente");
                entity.HasKey(e => e.IdPaciente);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.DataNascimento).IsRequired();
                entity.Property(e => e.DataCriacao).IsRequired();

                entity.HasMany(e => e.Agendamentos)
                    .WithOne(e => e.Paciente)
                    .HasForeignKey(e => e.IdPaciente)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.ToTable("tb_agendamento");
                entity.HasKey(e => e.IdAgendamento);
                entity.Property(e => e.DataAgendamento).IsRequired();
                entity.Property(e => e.HoraAgendamento).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DataCriacao).IsRequired();

                entity.HasOne(e => e.Paciente)
                    .WithMany(e => e.Agendamentos)
                    .HasForeignKey(e => e.IdPaciente)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }    
    }
}