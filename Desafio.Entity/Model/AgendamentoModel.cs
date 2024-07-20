using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.Entities;

namespace Desafio.Entity.Model
{
    public class AgendamentoModel
    {
    public int IdAgendamento { get; set; }
    public int IdPaciente { get; set; }
    public DateTime DataAgendamento { get; set; }
    public TimeSpan HoraAgendamento { get; set; }
    public string Status { get; set; }
    }
}