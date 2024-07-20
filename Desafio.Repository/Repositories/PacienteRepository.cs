using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.DTO;
using Desafio.Entity.Entities;
using Desafio.Repository.Interface.IRepositories;
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

        public Task<Paciente> ObterPorId(int id)
        {
            return EntitySet.FirstOrDefaultAsync(paciente => paciente.IdPaciente == id);
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

        public Task<Paciente> ObterPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}