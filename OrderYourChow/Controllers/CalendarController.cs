using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.API.Calendar;
using OrderYourChow.CORE.Models.API.Calendar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.Controllers
{
    public class CalendarController : BaseController
    {
        private readonly ICalendarRepository _calendarRepository;

        public CalendarController(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        [HttpGet("dietDays")]
        public async Task<ActionResult<IList<DietDayDTO>>> GetDietDays(DateTime? dateMin, DateTime? dateMax) 
            => Ok(await _calendarRepository.GetDietDays(dateMin, dateMax));
    }
}
