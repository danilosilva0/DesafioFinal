using Desafio.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Repository
{
    public class Contexto : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("database=dbdesafio;server=localhost\\sqlexpress;Trusted_Connection=True;Trust Server Certificate=true", options =>
                {
                    options.UseRelationalNulls();
                });
            }
        }
    }
}
