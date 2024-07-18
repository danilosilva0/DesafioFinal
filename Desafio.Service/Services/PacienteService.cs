using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Service.Interface;
using Desafio.Repository.Interface;
using Desafio.Helper.Exceptions;
using Desafio.Service.Interface.Services;
using Desafio.Entity.Model;
using Desafio.Entity.DTO;
using Desafio.Repository.Interface.IRepositories;
using log4net;
using Desafio.Entity.Entities;
using Desafio.Helper.Messages;

namespace Desafio.Service.Services
{
    public class PacienteService : IPacienteService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PacienteService));
        private readonly IPacienteRepository _pacienteRepository;
        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task InserirPaciente(PacienteModel novoPaciente)
        {
            var pacienteDTO = await _pacienteRepository.BuscarPorNome(novoPaciente.Nome);

            if (pacienteDTO.Count > 0)
            {
                _log.InfoFormat(ServiceMessages.ExistentRegister, pacienteDTO[0].Nome);
                throw new ServiceException(string.Format(ServiceMessages.ExistentRegister, pacienteDTO[0].Nome));
            }

            var paciente = CriarPaciente(novoPaciente);

            await _pacienteRepository.Inserir(paciente);

            //_log.InfoFormat("A tarefa '{0}' foi inserida.", novaTarefa); 
        }

        private static Paciente CriarPaciente(PacienteModel novoPaciente)
        {
            var paciente = new Paciente
            {
                Nome = novoPaciente.Nome,
                DataNascimento = novoPaciente.DataNascimento,
                DataCriacao = DateTime.Now
            };

            return paciente;
        }

        public async Task AlterarPaciente(int id, PacienteModel pacienteAtualizado)
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
                throw new ServiceException($"O paciente com ID '{id}' não existe.");
            }
        }

        public async Task DeletarPaciente(int id)
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
                throw new ServiceException(string.Format(ServiceMessages.RegisterNotFound, id));
            }
        }

        public async Task<PacienteDTO> ObterPacientePorId(int id)
        {
            var paciente = await _pacienteRepository.ObterPorId(id);

            if (paciente == null)
                throw new ServiceException(string.Format(ServiceMessages.RegisterNotFound, id));

            return new PacienteDTO
            {
                IdPaciente = paciente.IdPaciente,
                Nome = paciente.Nome,
                DataNascimento = paciente.DataNascimento,
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
                    IdPaciente = paciente.IdPaciente,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento,
                });
            }

            return pacientesDTO;
        }

        private static void ValidarPaciente(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ServiceException(string.Format(ServiceMessages.Required, "Nome"));
        }
    }
}
