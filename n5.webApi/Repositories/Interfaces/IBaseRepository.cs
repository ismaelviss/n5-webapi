using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace n5.webApi.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        IUnitOfWork UnitOfWork { get; set; }
        IEnumerable<T> All();
        IQueryable<T> AllQueryable();
        IEnumerable<T> Where(Expression<Func<T, bool>> expression);
        T One(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Attach(T entity);
        void Delete(T entity);

        T FindById(int id);
    }
}
