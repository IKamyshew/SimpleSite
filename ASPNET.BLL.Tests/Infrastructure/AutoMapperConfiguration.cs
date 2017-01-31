using AutoMapper;

namespace ASPNETSimple.BLL.Tests.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {

            Mapper.Initialize(x =>
            {
                ASPNETSimple.BLL.Infrastructure.AutoMapperConfiguration.ProjectMappings.Invoke(x);
            });
        }
    }
}
