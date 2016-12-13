using System;
using ASPNETSimple.DAL.Entities;

namespace ASPNETSimple.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        void Save();
    }
}
