using ASPNETSimple.BLL.Models;
using ASPNETSimple.DAL.Entities;
using AutoMapper;

namespace ASPNETSimple.BLL.Infrastructure.Mappings
{

    public class ModelsToEntitiesProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ModelsToEntitiesProfile"; }
        }

        protected override void Configure()
        {
            CreateMap<User, UserModel>();

            CreateMap<UserModel, User>();
        }
    }
}
