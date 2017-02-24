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

        public ViewModelsToModelsProfile()
        {
            CreateMap<RegisterViewModel, UserModel>();

            CreateMap<UserModel, RegisterViewModel>();
        }
    }
}