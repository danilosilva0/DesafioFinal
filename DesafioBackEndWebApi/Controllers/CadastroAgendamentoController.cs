using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<AgendamentoDTO>> InserirAgendamento(AgendamentoModel novoAgendamento){
            return await _agendamentoService.InserirAgendamento(novoAgendamento);
        }

        [HttpPut("AlterarAgendamento")]
        public async Task<List<AgendamentoDTO>> AlterarAgendamento(string nomeAgendamento, string novoNomeAgendamento){
            return await _agendamentoService.AlterarAgendamento(nomeAgendamento, novoNomeAgendamento);
        }

        [HttpDelete("DeletarAgendamento")]
        public async Task<List<AgendamentoDTO>> DeletarAgendamento(string nomeAgendamento){
            return await _agendamentoService.DeletarAgendamento(nomeAgendamento);
        }
    }
}