using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.DTO;
using Desafio.Entity.Entities;

namespace Desafio.Repository.Interface.IRepositories
{
    public interface IPacienteRepository : IBaseRepository<Paciente>
    {
        Task<List<PacienteDTO>> ListarTodos();
        Task<Paciente> ObterPorNome(string nome);
        Task<List<PacienteDTO>> BuscarPorNome(string nome);
        Task<Paciente?> ObterPorId(int id);
    }
}