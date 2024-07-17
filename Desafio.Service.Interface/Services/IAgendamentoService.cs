using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.DTO;
using Desafio.Entity.Model;

namespace Desafio.Service.Interface.Services
{
    public interface IAgendamentoService
    {
        void InserirAgendamento(AgendamentoModel novoAgendamento);
        void AlterarAgendamento(AgendamentoModel novoAgendamento);
        void DeletarAgendamento(int id);
        Task<List<AgendamentoDTO>> ListarAgendamentos();
    }
}
