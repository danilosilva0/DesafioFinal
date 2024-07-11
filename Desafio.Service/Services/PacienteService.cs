using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Service.Services
{
    public class PacienteService : IPacienteService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PacienteService));
        private readonly IPacienteRepositorio _pacienteRepositorio;
        public PacienteNegocio(IPacienteRepositorio pacienteRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
        }

        public async Task<List<PacienteDTO>> InserirPaciente(CadastroPacienteModel novoPaciente)
        {
            var paciente = await _pacienteRepositorio.ObterPaciente(new PacienteFiltro() { Email = novoPaciente.Email });

            if (paciente != null)
            {
                _log.InfoFormat(BusinessMessages.RegistroExistente, paciente.Nome);
                throw new BusinessException(string.Format(BusinessMessages.RegistroExistente, paciente.Nome));
            }

            paciente = CriarPaciente(novoPaciente);

            await _pacienteRepositorio.Inserir(paciente);

            //_log.InfoFormat("A tarefa '{0}' foi inserida.", novaTarefa); 

            return await _pacienteRepositorio.ListarTodos();
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
    }
}