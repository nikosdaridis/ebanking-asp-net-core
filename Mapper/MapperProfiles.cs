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
            CreateMap<BankUser, BankUserViewModel>().ReverseMap();
            CreateMap<AccountModel, AccountViewModel>().ReverseMap();
        }
    }
}
