using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Helper.PacienteContexto
{
    public interface IPacienteContexto
    {
        /// <summary>
        /// Horário em que o contexto foi aberto.
        /// </summary>
        DateTime StartDateTime { get; set; }

        /// <summary>
        /// Informações da requisição como IP e HTTP HEADERS.
        /// </summary>
        ISourceInfo SourceInfo { get; set; }

        /// <summary>
        /// GUID para identificar unicamente uma requisição.
        /// </summary>
        Guid RequestId { get; set; }

        /// <summary>
        /// Dados adicionais que podem ser armazenados no Contexto.
        /// </summary>
        Hashtable AdditionalData { get; set; }

        /// <summary>
        /// Coleção de exceções não tratadas pelo desenvolvedor. A chave é um valor único para cada instância de <see cref="Exception"/>.
        /// </summary>
        Hashtable UnhandledExceptions { get; set; }

        /// <summary>
        /// Identificador único do paciente no contexto.
        /// </summary>
        int PacienteId { get; set; }

        /// <summary>
        /// Nome do paciente no contexto.
        /// </summary>
        string Nome { get; set; }

        /// <summary>
        /// Estado do paciente no contexto.
        /// </summary>
        string Status { get; set; }
    }
}