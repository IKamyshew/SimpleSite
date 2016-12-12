using System;
using ASPNETSimple.DAL.Entities;

namespace ASPNETSimple.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        void Save();
    }
}
