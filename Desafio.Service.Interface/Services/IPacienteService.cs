using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entities;

namespace Desafio.Service.Interface.Services
{
    public interface IPacienteService
    {
        Task<List<PacienteDTO>> CriarPaciente(CadastroPacienteModel novoPaciente);
        Task<List<PacienteDTO>> AtualizarPaciente(int id, CadastroPacienteModel pacienteAtualizado);
        Task<List<PacienteDTO>> DeletarPaciente(int id);
        Task<PacienteDTO> ObterPacientePorId(int id);
        Task<List<PacienteDTO>> ListarPacientes();
    }
}
