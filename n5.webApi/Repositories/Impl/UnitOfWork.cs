using System;
using Microsoft.Extensions.Logging;
using n5.webApi.Repositories.Interfaces;

namespace n5.webApi.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PermissionContext _context;

        public UnitOfWork(PermissionContext context)
        {
            _context = context;
            //_logger = logger;
        }

        public PermissionContext Context => _context;

        public void Commit()
        {
            this._context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
