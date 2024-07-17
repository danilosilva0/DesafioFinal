using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Repository.Interface
{
    public interface ITransactionManager
    {
        Task BeginTransactionAsync(IsolationLevel isolationLevel);
        Task CommitTransactionsAsync();
        Task RollbackTransactionsAsync();
    }
}