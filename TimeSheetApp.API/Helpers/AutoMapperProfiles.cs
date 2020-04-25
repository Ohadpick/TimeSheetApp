using System.Linq;
using AutoMapper;
using TimeSheetApp.API.Dtos;
using TimeSheetApp.API.Models;

namespace TimeSheetApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<DayForCreationDto, Day>();
            CreateMap<Day, DayToReturnDto>();
        }
    }
}