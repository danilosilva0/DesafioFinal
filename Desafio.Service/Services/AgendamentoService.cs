using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Helper.Messages;
using Desafio.Entity.DTO;
using Desafio.Entity.Entities;
using Desafio.Entity.Model;
using Desafio.Helper.Exceptions;
using Desafio.Repository.Interface.IRepositories;
using Desafio.Service.Interface.Services;
using log4net;

namespace Desafio.Service.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(AgendamentoService));
        private readonly IAgendamentoRepository _agendamentoRepository;
        public AgendamentoService(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        public Task<List<AgendamentoDTO>> ListarAgendamentos()
        {
            // if (agendamentos == null)
            // {
                return _agendamentoRepository.ListarTodos();
            // }
            // else
            // {
            //     agendamentos = agendamentos.Select(e => e.ToUpper())
            //                      .ToList();

            //     // return _agendamentoRepository.ListarAgendamentos(agendamentos);
            //     return _agendamentoRepository.ListarTodos();
            // }
        }

        public async Task InserirAgendamento(AgendamentoModel novoAgendamento)
        {
            // ValidarAgendamento(novoAgendamento.Titulo);

            var agendamentoDTO = await _agendamentoRepository.ObterAgendamentoPorId(novoAgendamento.IdAgendamento);

            if (agendamentoDTO != null)
                throw new ServiceException(string.Format(ServiceMessages.ExistentRegister, "Id"));
            
            var agendamento = CriarAgendamento(novoAgendamento);

            await _agendamentoRepository.Inserir(agendamento);

            _log.InfoFormat(ServiceMessages.SuccessfulInsertion, novoAgendamento);
        }

        private static Agendamento CriarAgendamento(AgendamentoModel novoAgendamento)
        {
            var agendamento = new Agendamento
            {
                IdPaciente = novoAgendamento.IdPaciente,
                DataAgendamento = novoAgendamento.DataAgendamento,
                HoraAgendamento = novoAgendamento.HoraAgendamento,
                Status = novoAgendamento.Status,
                DataCriacao = DateTime.Now
            };
            return agendamento;
        }

        public async Task AlterarAgendamento(AgendamentoModel novoAgendamento)
        {
            ValidarAgendamento(novoAgendamento);
            var agendamento = await _agendamentoRepository.ObterAgendamentoPorId(novoAgendamento.IdAgendamento);

            if (agendamento != null)
            {
                agendamento.IdPaciente = novoAgendamento.IdPaciente;
                agendamento.DataAgendamento = novoAgendamento.DataAgendamento;
                agendamento.HoraAgendamento = novoAgendamento.HoraAgendamento;
                agendamento.Status = novoAgendamento.Status;
                await _agendamentoRepository.Atualizar(agendamento);
                _log.InfoFormat(ServiceMessages.SuccessfulChange, agendamento.IdAgendamento, novoAgendamento.IdAgendamento);
            }
            else
            {
                _log.InfoFormat(ServiceMessages.RegisterNotFound, novoAgendamento.IdAgendamento);
                throw new ServiceException(string.Format(ServiceMessages.RegisterNotFound, novoAgendamento.IdAgendamento));
            }
        }

        public async Task DeletarAgendamento(int id)
        {
            var agendamento = await _agendamentoRepository.ObterAgendamentoPorId(id);

            if (agendamento != null)
            {
                await _agendamentoRepository.Deletar(agendamento);
                _log.InfoFormat(ServiceMessages.RemovedRegister, id);
            }
            else
            {
                _log.InfoFormat(ServiceMessages.RegisterNotFound, id);
                throw new ServiceException(string.Format(ServiceMessages.RegisterNotFound, id));
            }
        }

        private static void ValidarAgendamento(AgendamentoModel novoAgendamento)
        {
            if (novoAgendamento.DataAgendamento < DateTime.Now)
                throw new ServiceException(string.Format(ServiceMessages.InvalidInput, "Data do Agendamento"));

            if (novoAgendamento.DataAgendamento < DateTime.Now && novoAgendamento.HoraAgendamento < DateTime.Now.TimeOfDay)
                throw new ServiceException(string.Format(ServiceMessages.InvalidInput, "Hopa do Agendamento"));
        }
    }
}