using ASPNETSimple.DAL.Repositories.Interfaces;

namespace ASPNETSimple.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        void Save();
    }
}
