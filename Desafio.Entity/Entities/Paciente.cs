using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Entity.Entities
{
    public class Paciente
    {
        public int IdPaciente {get; set;}
        public string Nome {get; set;}
        public DateTime DataNascimento {get; set;}
        public DateTime DataCriacao {get; set;}
        public ICollection<Agendamento> Agendamentos {get; set;}
    }
}