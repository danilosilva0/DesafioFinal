using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Repository.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(Contexto contexto) : base(contexto) { }

        public Task<List<PacienteDTO>> ListarTodos()
        {
            var query = EntitySet.OrderBy(paciente => paciente.Nome)
                                .Select(paciente => new PacienteDTO
                                {
                                    IdPaciente = paciente.IdPaciente,
                                    Nome = paciente.Nome,
                                    DataNascimento = paciente.DataNascimento
                                });

            return query.ToListAsync();
        }

        public Task<Paciente> ObterPorNome(string nome)
        {
            return EntitySet.FirstOrDefaultAsync(paciente => paciente.Nome == nome);
        }

        public Task<List<PacienteDTO>> BuscarPorNome(string nome)
        {
            var query = EntitySet.Where(paciente => paciente.Nome.Contains(nome))
                                .Select(paciente => new PacienteDTO
                                {
                                    IdPaciente = paciente.IdPaciente,
                                    Nome = paciente.Nome,
                                    DataNascimento = paciente.DataNascimento
                                });

            return query.ToListAsync();
        }
    }
}