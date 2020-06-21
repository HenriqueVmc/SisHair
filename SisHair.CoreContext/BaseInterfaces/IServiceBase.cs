using System;
using System.Collections.Generic;

namespace SisHair.CoreContext.BaseInterfaces
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : class
    {
        void Adicionar(TEntity obj);
        TEntity BuscarPorId(int id);
        IEnumerable<TEntity> BuscarTodos();
        void Atualizar(TEntity obj);
        void Remover(TEntity obj);
        bool SaveChanges();
    }
}
