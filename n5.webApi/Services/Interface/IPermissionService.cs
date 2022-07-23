using n5.webApi.Models;
using System.Collections.Generic;

namespace n5.webApi.Services.Interface
{
    public interface IPermissionService
    {
        IEnumerable<Permission> Get();

        Permission GetById(int id);

        Permission Save(Permission permission);

        Permission Update(int id, Permission permission);

        IEnumerable<PermissionType> GetPermissionTypes();
    }
}
