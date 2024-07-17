using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Service.Interface;
using Desafio.Entities;
using Desafio.Repository.Interface;
using Desafio.Helper.Exceptions;
using Desafio.Helper.Messages;
using log4net;

namespace Desafio.Service.Services
{
    public class PacienteService : IPacienteService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PacienteService));
        private readonly IPacienteRepository _pacienteRepository;
        public PacienteNegocio(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<List<PacienteDTO>> InserirPaciente(CadastroPacienteModel novoPaciente)
        {
            var paciente = await _pacienteRepository.ObterPaciente(new PacienteFiltro() { Email = novoPaciente.Email });

            if (paciente != null)
            {
                _log.InfoFormat(BusinessMessages.RegistroExistente, paciente.Nome);
                throw new BusinessException(string.Format(BusinessMessages.RegistroExistente, paciente.Nome));
            }

            paciente = CriarPaciente(novoPaciente);

            await _pacienteRepository.Inserir(paciente);

            //_log.InfoFormat("A tarefa '{0}' foi inserida.", novaTarefa); 

            return await _pacienteRepository.ListarTodos();
        }

        private static Paciente CriarPaciente(CadastroPacienteModel novoPaciente)
        {
            var paciente = new Paciente
            {
                Nome = novoPaciente.Nome,
                DataNascimento = novoPaciente.Perfil,
                DataCriacao = DateTime.Now
            };

            return paciente;
        }

        public async Task<List<PacienteDTO>> AlterarPaciente(int id, CadastroPacienteModel pacienteAtualizado)
        {
            ValidarPaciente(pacienteAtualizado.Nome);
            var paciente = await _pacienteRepository.ObterPorId(id);

            if (paciente != null)
            {
                paciente.Nome = pacienteAtualizado.Nome;
                paciente.DataNascimento = pacienteAtualizado.DataNascimento;
                await _pacienteRepository.Atualizar(paciente);
                _log.InfoFormat("O paciente com ID '{0}' foi atualizado.", id);
            }
            else
            {
                _log.InfoFormat("O paciente com ID '{0}' não existe.", id);
                throw new BusinessException($"O paciente com ID '{id}' não existe.");
            }

            return await ListarPacientes();
        }

        public async Task<List<PacienteDTO>> DeletarPaciente(int id)
        {
            var paciente = await _pacienteRepository.ObterPorId(id);

            if (paciente != null)
            {
                await _pacienteRepository.Deletar(paciente);
                _log.InfoFormat("O paciente com ID '{0}' foi removido.", id);
            }
            else
            {
                _log.InfoFormat("O paciente com ID '{0}' não existe.", id);
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, id));
            }

            return await ListarPacientes();
        }

        public async Task<PacienteDTO> ObterPacientePorId(int id)
        {
            var paciente = await _pacienteRepository.ObterPorId(id);

            if (paciente == null)
                throw new BusinessException(string.Format(BusinessMessages.RegistroNaoEncontrado, id));

            return new PacienteDTO
            {
                Id = paciente.Id,
                Nome = paciente.Nome,
                DataNascimento = paciente.DataNascimento,
                DataCriacao = paciente.DataCriacao
            };
        }

        public async Task<List<PacienteDTO>> ListarPacientes()
        {
            var pacientes = await _pacienteRepository.Todos();
            var pacientesDTO = new List<PacienteDTO>();

            foreach (var paciente in pacientes)
            {
                pacientesDTO.Add(new PacienteDTO
                {
                    Id = paciente.Id,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento,
                    DataCriacao = paciente.DataCriacao
                });
            }

            return pacientesDTO;
        }

        private static void ValidarPaciente(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new BusinessException(string.Format(BusinessMessages.CampoObrigatorio, "Nome"));
        }
    }
}
