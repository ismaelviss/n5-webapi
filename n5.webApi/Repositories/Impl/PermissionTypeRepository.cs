using Microsoft.Extensions.Logging;
using n5.webApi.Models;
using n5.webApi.Repositories.Interfaces;

namespace n5.webApi.Repositories.Impl
{
    public class PermissionTypeRepository : BaseRepository<PermissionType>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
