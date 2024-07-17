using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Entity.Entities;

namespace Desafio.Entity.Model
{
    public class PacienteModel
    {
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public DateTime DataCriacao { get; set; }
    public ICollection<Agendamento> Agendamentos { get; set; }
    }
}