using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.DTO;
using Desafio.Entity.Model;
using Desafio.Service.Interface.Services;
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
        public void InserirPaciente(PacienteModel novoPaciente){
            _pacienteService.InserirPaciente(novoPaciente);
        }

        [HttpPut("AlterarPaciente")]
        public void AlterarPaciente(int id, PacienteModel pacienteModel){
            _pacienteService.AlterarPaciente(id, pacienteModel);
        }

        [HttpDelete("DeletarPaciente")]
        public void DeletarPaciente(int id){
            _pacienteService.DeletarPaciente(id);
        }
    }
}