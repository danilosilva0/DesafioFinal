using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.DTO;
using Desafio.Entity.Model;

namespace Desafio.Service.Interface.Services
{
    public interface IPacienteService
    {
        void InserirPaciente(PacienteModel novoPaciente);
        void AlterarPaciente(int id, PacienteModel pacienteAtualizado);
        void DeletarPaciente(int id);
        Task<PacienteDTO> ObterPacientePorId(int id);
        Task<List<PacienteDTO>> ListarPacientes();
    }
}
