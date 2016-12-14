using ASPNETSimple.BLL.Models;
using ASPNETSimple.WEB.Models.Account;
using AutoMapper;

namespace ASPNETSimple.WEB.Infrastructure.Mappings
{
    public class ViewModelsToModelsProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelsToModelsProfile"; }
        }

        protected override void Configure()
        {
            CreateMap<RegisterViewModel, UserModel>();

            CreateMap<UserModel, RegisterViewModel>();
        }
    }
}