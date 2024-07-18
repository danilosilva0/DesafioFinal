using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Entity.DTO
{
    public class PacienteDTO
    {
    public int? IdPaciente { get; set; }
    public string? Nome { get; set; }
    public DateTime? DataNascimento { get; set; }
    public DateTime? DataCriacao { get; set; }
    }
}