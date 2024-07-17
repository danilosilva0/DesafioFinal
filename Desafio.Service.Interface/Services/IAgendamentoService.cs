using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entities;

namespace Desafio.Service.Interface.Services
{
    public interface IAgendamentoService
    {
        Task<List<AgendamentoDTO>> CriarAgendamento(CadastroAgendamentoModel novoAgendamento);
        Task<List<AgendamentoDTO>> AtualizarAgendamento(int id, CadastroAgendamentoModel agendamentoAtualizado);
        Task<List<AgendamentoDTO>> DeletarAgendamento(int id);
        Task<AgendamentoDTO> ObterAgendamentoPorId(int id);
        Task<List<AgendamentoDTO>> ListarAgendamentos();
    }
}
