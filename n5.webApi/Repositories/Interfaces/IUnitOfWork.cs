namespace n5.webApi.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        PermissionContext Context { get; }

        void Commit();
    }
}
