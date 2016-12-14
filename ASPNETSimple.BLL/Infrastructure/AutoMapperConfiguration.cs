using System;
using ASPNETSimple.BLL.Infrastructure.Mappings;
using AutoMapper;

namespace ASPNETSimple.BLL.Infrastructure
{

    public class AutoMapperConfiguration
    {
        public static Action<IMapperConfigurationExpression> ProjectMappings = new Action<IMapperConfigurationExpression>(x =>
        {
            x.AddProfile<ModelsToEntitiesProfile>();
        });

        public static void Configure()
        {
            Mapper.Initialize(ProjectMappings);
        }
        
    }
}