using AutoMapper;
using GymManagementBL.Service.ViewModels.SessionViewModel;
using GymManagementDL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.TrainerName, option => option.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.AvaliableSlot, option => option.Ignore());

            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();

        }
    }
}
