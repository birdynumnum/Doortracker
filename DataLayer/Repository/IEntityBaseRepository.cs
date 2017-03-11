using Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayer.Repository
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        //IQueryable?? - tsk tsk!
        IQueryable<T> GetAll();
        T GetSingle(int id);
        void Add(T entity);
        void Edit(T entity);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
