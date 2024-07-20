using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.DTO;
using Desafio.Entity.Entities;

namespace Desafio.Repository.Interface.IRepositories
{
    public interface IAgendamentoRepository : IBaseRepository<Agendamento>
    {
        Task<List<AgendamentoDTO>> ListarTodos();
        Task<List<AgendamentoDTO>> ListarPorPaciente(int idPaciente);
        Task<Agendamento> ObterPorDataEHora(int idPaciente, DateTime data, TimeSpan hora);
        Task<Agendamento> ObterAgendamentoPorId(int id);
    }
}