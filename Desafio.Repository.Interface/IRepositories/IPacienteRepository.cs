using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Repository.Interface.IRepositories
{
    public interface IPacienteRepository : IBaseRepository<Paciente>
    {
        Task<List<PacienteDTO>> ListarTodos();
        Task<Paciente> ObterPorNome(string nome);
        Task<List<PacienteDTO>> BuscarPorNome(string nome);
    }
}