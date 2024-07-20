using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Helper.PacienteContexto
{
    public class PacienteContexto : IPacienteContexto
    {
       public PacienteContexto() { }

        /// <summary>
        /// Horário em que o contexto foi aberto.
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Informações da requisição como IP e HTTP HEADERS.
        /// </summary>
        public ISourceInfo SourceInfo { get; set; }

        /// <summary>
        /// GUID para identificar unicamente uma requisição.
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Dados adicionais que podem ser armazenados no Contexto.
        /// </summary>
        public Hashtable AdditionalData { get; set; } = new Hashtable();

        /// <summary>
        /// Coleção de exceções não tratadas pelo desenvolvedor. A chave é um valor único para cada instância de <see cref="Exception"/>.
        /// </summary>
        public Hashtable UnhandledExceptions { get; set; } = new Hashtable();

        /// <summary>
        /// Identificador único do paciente no contexto.
        /// </summary>
        public int PacienteId { get; set; }

        /// <summary>
        /// Nome do paciente no contexto.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Estado do paciente no contexto.
        /// </summary>
        public string Status { get; set; } 
    }
}