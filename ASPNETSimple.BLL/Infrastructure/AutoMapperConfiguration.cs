using ASPNETSimple.BLL.Infrastructure.Mappings;
using AutoMapper;

namespace ASPNETSimple.BLL.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ModelsToEntitiesProfile>();
            });
        }
    }
}