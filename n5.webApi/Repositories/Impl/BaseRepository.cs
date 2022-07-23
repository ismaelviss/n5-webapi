using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using n5.webApi.Models;
using n5.webApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace n5.webApi.Repositories
{
    public class BaseRepository<T> where T : EntityBase
    {
        //protected PermissionContext context;
        //internal DbSet<T> dbSet;
        protected readonly ILogger _logger;

        private IUnitOfWork _unitOfWork;
        protected IUnitOfWork UnidadTrabajo
        {
            get
            {
                return _unitOfWork;
            }
            set
            {
                _unitOfWork = value;
            }
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this._unitOfWork;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public virtual IEnumerable<T> All()
        {
            var x = UnidadTrabajo.Context.Set<T>().ToList();
            return x;
        }

        public IQueryable<T> AllQueryable()
        {
            return UnidadTrabajo.Context.Set<T>().AsQueryable();
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return UnidadTrabajo.Context.Set<T>().Where(expression).ToList();

        }

        public virtual T One(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return UnidadTrabajo.Context.Set<T>().Where(expression).FirstOrDefault();
        }

        public void Add(T entity)
        {
            UnidadTrabajo.Context.Set<T>().Add(entity);
        }

        public void Attach(T entity)
        {
            UnidadTrabajo.Context.Set<T>().Attach(entity);
        }

        public void Delete(T entity)
        {
            UnidadTrabajo.Context.Set<T>().Remove(entity);
        }

        public virtual T FindById(int id)
        {
            return UnidadTrabajo.Context.Set<T>().Find(id);
        }
    }
}
