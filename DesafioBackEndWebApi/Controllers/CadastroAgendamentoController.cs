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
    public class CadastroAgendamentoController
    {
        private readonly IAgendamentoService _agendamentoService;

        public CadastroAgendamentoController(IAgendamentoService agendamentoService){
            _agendamentoService = agendamentoService;
        }

        [HttpGet("ListarAgendamentos")]
        public async Task<List<AgendamentoDTO>> ListarAgendamentos(){
            return await _agendamentoService.ListarAgendamentos();
        }

        [HttpPost("InserirAgendamento")]
        public void InserirAgendamento(AgendamentoModel novoAgendamento){
            _agendamentoService.InserirAgendamento(novoAgendamento);
        }

        [HttpPut("AlterarAgendamento")]
        public void AlterarAgendamento(AgendamentoModel novoAgendamento){
            _agendamentoService.AlterarAgendamento(novoAgendamento);
        }

        [HttpDelete("DeletarAgendamento")]
        public void DeletarAgendamento(AgendamentoModel novoAgendamento){
            _agendamentoService.DeletarAgendamento(novoAgendamento.IdAgendamento);
        }
    }
}