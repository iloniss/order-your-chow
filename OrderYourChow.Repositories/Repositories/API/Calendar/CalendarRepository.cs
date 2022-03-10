using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.CORE.Contracts.API.Calendar;
using OrderYourChow.CORE.Models.API.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.Repositories.Repositories.API.Calendar
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly IMapper _mapper;
        private readonly OrderYourChowContext _orderYourChowContext;

        public CalendarRepository(IMapper mapper, OrderYourChowContext orderYourChowContext)
        {
            _mapper = mapper;
            _orderYourChowContext = orderYourChowContext;

            
        }
        public async Task<List<DietDayDTO>> GetDietDays(DateTime? dateMin, DateTime? dateMax)
        {
            if(dateMin == null || dateMax == null)
            {
                return _mapper.Map<List<DietDayDTO>>(await _orderYourChowContext.DDietDays
                    .Include(x => x.SDateDay)
                    .Include(x => x.SDateDay)
                    .ThenInclude(x => x.SDay)
                    .Where(x => x.SDateDay.DateDay >= DateTime.Now.AddDays(-7) && x.SDateDay.DateDay <= DateTime.Now.AddDays(7))
                    .OrderBy(x => x.DateDayId).ToListAsync());
            }
            return _mapper.Map<List<DietDayDTO>>(await _orderYourChowContext.DDietDays
                        .Include(x => x.SDateDay)
                        .Include(x => x.SDateDay)
                        .ThenInclude(x => x.SDay)
                        .Where(x => x.SDateDay.DateDay >= dateMin && x.SDateDay.DateDay <= dateMax)
                        .OrderBy(x => x.DateDayId).ToListAsync());
        }
    }
}
