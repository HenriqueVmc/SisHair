using SisHair.CoreContext.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisHair.CoreContext
{
    public abstract class ServiceBase<TEntity, TRepository> : IServiceBase<TEntity> where TEntity : class
        where TRepository : class, IRepositoryBase<TEntity>
    {
        protected TRepository Repository { get; set; }

        protected ServiceBase(TRepository repository)
        {
            Repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        public void Dispose() => Repository.Dispose();

        public void Adicionar(TEntity obj) => Repository.Add(obj);

        public TEntity BuscarPorId(int id) => Repository.GetById(id);

        public IEnumerable<TEntity> BuscarTodos() => Repository.GetAll().ToList();

        public void Atualizar(TEntity obj) => Repository.Update(obj);

        public void Remover(TEntity obj) => Repository.Remove(obj);

        public async Task<bool> SaveChanges() => await Repository.UnitOfWork.Commit();
    }
}
