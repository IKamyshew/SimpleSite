using ASPNETSimple.DAL.Context;

namespace ASPNETSimple.DAL.Interfaces
{
    public interface IDbFactory
    {
        EFContext GetInstance();
    }
}
