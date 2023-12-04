using AutoMapper;
using eBanking.Areas.Identity.Data;
using eBanking.Models;
using eBanking.ViewModels;

namespace eBanking.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<BankUser, BankUserViewModel>();

            CreateMap<BankUserViewModel, BankUser>();

            CreateMap<AccountModel, AccountViewModel>();

            CreateMap<AccountViewModel, AccountModel>();
        }
    }
}
