using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Repository.Repositories
{
    public class AgendamentoRepository: BaseRepository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(Contexto contexto) : base(contexto) { }

        public Task<List<AgendamentoDTO>> ListarTodos()
        {
            var query = EntitySet.OrderBy(agendamento => agendamento.DataAgendamento)
                                .Select(agendamento => new AgendamentoDTO
                                {
                                    IdAgendamento = agendamento.IdAgendamento,
                                    DataAgendamento = agendamento.DataAgendamento,
                                    HoraAgendamento = agendamento.HoraAgendamento,
                                    Status = agendamento.Status,
                                    IdPaciente = agendamento.IdPaciente
                                });

            return query.ToListAsync();
        }

        public Task<List<AgendamentoDTO>> ListarPorPaciente(int idPaciente)
        {
            var query = EntitySet.Where(agendamento => agendamento.IdPaciente == idPaciente)
                                .OrderBy(agendamento => agendamento.DataAgendamento)
                                .Select(agendamento => new AgendamentoDTO
                                {
                                    IdAgendamento = agendamento.IdAgendamento,
                                    DataAgendamento = agendamento.DataAgendamento,
                                    HoraAgendamento = agendamento.HoraAgendamento,
                                    Status = agendamento.Status,
                                    IdPaciente = agendamento.IdPaciente
                                });

            return query.ToListAsync();
        }

        public Task<Agendamento> ObterPorDataEHora(int idPaciente, DateTime data, TimeSpan hora)
        {
            return EntitySet.FirstOrDefaultAsync(agendamento => agendamento.IdPaciente == idPaciente 
                                                            && agendamento.DataAgendamento == data 
                                                            && agendamento.HoraAgendamento == hora);
        }
    }
}