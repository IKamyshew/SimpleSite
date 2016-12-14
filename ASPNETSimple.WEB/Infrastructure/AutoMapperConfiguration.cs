using ASPNETSimple.WEB.Infrastructure.Mappings;
using AutoMapper;

namespace ASPNETSimple.WEB.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {

            Mapper.Initialize(x =>
            {
                ASPNETSimple.BLL.Infrastructure.AutoMapperConfiguration.ProjectMappings.Invoke(x);
                x.AddProfile<ViewModelsToModelsProfile>();
            });
        }
    }
}