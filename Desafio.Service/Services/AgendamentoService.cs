using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Task<List<AgendamentoDTO>> ListarAgendamentos(List<string> agendamentos)
        {
            if (agendamentos == null)
            {
                return _agendamentoRepository.ListarTodas();
            }
            else
            {
                agendamentos = agendamentos.Select(e => e.ToUpper())
                                 .ToList();

                return _agendamentoRepository.ListarAgendamentos(agendamentos);
            }
        }

        public async Task<List<AgendamentoDTO>> InserirAgendamento(CadastroAgendamentoModel novoAgendamento)
        {
            ValidarAgendamento(novoAgendamento.Titulo);

            var agendamento = await _agendamentoRepository.ObterAgendamento(novoAgendamento.Titulo);

            if (agendamento != null)
                throw new BusinessException(string.Format(BusinessMessages.RegistroExistente, "Título"));
            
            agendamento = new Agendamento(novoAgendamento.Titulo);

            await _agendamentoRepository.Inserir(agendamento);

            // _log.InfoFormat("O agendamento '{0}' foi inserida.", novoAgendamento);
            
            return await _agendamentoRepository.ListarTodas();
        }

        public async Task<List<AgendamentoDTO>> AlterarAgendamento(string nomeAgendamento, string novoNomeAgendamento)
        {
            ValidarAgendamento(novoNomeAgendamento);
            var agendamento = await _agendamentoRepository.ObterAgendamento(nomeAgendamento);

            if (agendamento != null)
            {
                agendamento.Titulo = novoNomeAgendamento;
                await _agendamentoRepository.Atualizar(agendamento);
                _log.InfoFormat("A agendamento '{0}' foi atualizada para o nome {1}.", nomeAgendamento, novoNomeAgendamento);
            }
            else
            {
                _log.InfoFormat("A agendamento '{0}' não existe na base.", nomeAgendamento);
                throw new BusinessException($"A agendamento '{nomeAgendamento}' não existe na base.");
            }

            return await _agendamentoRepository.ListarTodas();
        }

        public async Task<List<AgendamentoDTO>> DeletarAgendamento(string nomeAgendamento)
        {
            ValidarAgendamento(nomeAgendamento);
            var agendamento = await _agendamentoRepository.ObterAgendamento(nomeAgendamento);

            if (agendamento != null)
            {
                await _agendamentoRepository.Deletar(agendamento);
                _log.InfoFormat("A agendamento '{0}' foi removida.", nomeAgendamento);
            }
            else
            {
                _log.InfoFormat("A agendamento '{0}' não existe na base.", nomeAgendamento);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, nomeAgendamento));
            }

            return await _agendamentoRepository.ListarTodas();
        }

        private static void ValidarAgendamento(string titulo)
        {
            if (string.IsNullOrEmpty(titulo))
                throw new BusinessException(string.Format(BusinessMessages.CampoObrigatorio, "Título"));
        }
    }
}