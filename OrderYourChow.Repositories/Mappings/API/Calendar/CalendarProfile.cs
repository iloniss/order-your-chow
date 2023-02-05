using AutoMapper;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Models.API.Calendar;

namespace OrderYourChow.CORE.Mappings.API.Calendar
{
    public class CalendarProfile : Profile
    {
        public CalendarProfile()
        {
            CreateMap<DDietDay, DietDayDTO>()
                .ForMember(d => d.Day, opt => opt.MapFrom(src => src.SDateDay.SDay.Name + ' ' + src.SDateDay.DateDay.ToString("dd.MM.yy")))
                .ForMember(d => d.DietDayId, opt => opt.MapFrom(scr => scr.DietDayId));
        }
    }
}
