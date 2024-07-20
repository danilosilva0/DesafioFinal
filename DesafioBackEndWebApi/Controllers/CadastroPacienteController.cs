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
        public async Task InserirPaciente(PacienteModel novoPaciente){
            await _pacienteService.InserirPaciente(novoPaciente);
        }

        [HttpPut("AlterarPaciente")]
        public async Task AlterarPaciente(int id, PacienteModel pacienteModel){
            await _pacienteService.AlterarPaciente(id, pacienteModel);
        }

        [HttpDelete("DeletarPaciente")]
        public async Task DeletarPaciente(int id){
            await _pacienteService.DeletarPaciente(id);
        }
    }
}