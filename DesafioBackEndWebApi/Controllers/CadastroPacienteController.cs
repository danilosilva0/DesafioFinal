using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEndWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroPacienteController
    {

        private readonly IPacienteService _pacienteService;

        public CadastroPacienteController(IPacienteService pacienteService){
            _pacienteService = pacienteService;
        }

        [HttpGet("ListarPacientes")]
        public async Task<List<PacienteDTO>> ListarPacientes(){
            return await _pacienteService.ListarPacientes();
        }

        [HttpPost("InserirPaciente")]
        public async Task<List<PacienteDTO>> InserirPaciente(PacienteModel novoPaciente){
            return await _pacienteService.InserirPaciente(novoPaciente);
        }

        [HttpPut("AlterarPaciente")]
        public async Task<List<PacienteDTO>> AlterarPaciente(string nomePaciente, string novoNomePaciente){
            return await _pacienteService.AlterarPaciente(nomePaciente, novoNomePaciente);
        }

        [HttpDelete("DeletarPaciente")]
        public async Task<List<PacienteDTO>> DeletarPaciente(string nomePaciente){
            return await _pacienteService.DeletarPaciente(nomePaciente);
        }
    }
}