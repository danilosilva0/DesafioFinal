using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Repository.Interface.IRepositories
{
    public interface IBaseRepository<TEntidade> where TEntidade : class
    {
        Task<TEntidade> Atualizar(TEntidade entidade);
        Task Deletar(TEntidade entidade);
        Task Deletar(IEnumerable<TEntidade> entidades);
        Task DeletarPorId(object id);
        Task<TEntidade> Inserir(TEntidade entidade);
        Task Inserir(IEnumerable<TEntidade> entidades);
        Task<TEntidade> ObterPorId(object id);
        Task<List<TEntidade>> Todos();
    }
}