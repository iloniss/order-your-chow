using OrderYourChow.CORE.Models.API.Calendar;

namespace OrderYourChow.CORE.Contracts.API.Calendar
{
    public interface ICalendarRepository
    {
        Task<IList<DietDayDTO>> GetDietDays(DateTime? dateMin, DateTime? dateMax);
    }
}
