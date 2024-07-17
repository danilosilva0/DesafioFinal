using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Desafio.Helper.PacienteContexto
{
    public interface ISourceInfo
    {
        /// <summary>
        /// Contém HEADERS da requisição.
        /// </summary>
        Hashtable Data { get; set; }

        /// <summary>
        /// Origem da requisição.
        /// </summary>
        IPAddress IP { get; set; }
    }
}