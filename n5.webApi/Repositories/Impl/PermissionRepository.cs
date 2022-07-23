using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using n5.webApi.Models;
using n5.webApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace n5.webApi.Repositories.Impl
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<Permission> All()
        {
            return UnidadTrabajo.Context.Permissions.Include(x => x.PermissionType);
        }

        public override Permission FindById(int id)
        {
            return UnidadTrabajo.Context.Permissions.Include(x => x.PermissionType).AsQueryable().FirstOrDefault(x => x.Id == id);
        }
    }
}
