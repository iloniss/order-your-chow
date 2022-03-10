using OrderYourChow.CORE.Models.API.Calendar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.API.Calendar
{
    public interface ICalendarRepository
    {
        Task<List<DietDayDTO>> GetDietDays(DateTime? dateMin, DateTime? dateMax);
    }
}
