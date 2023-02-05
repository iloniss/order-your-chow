import http from '../http-common';
import {CalendarDay} from '../models/calendar/calendarDay';
import {CalendarQuery} from '../models/calendar/calendarQuery';

class CalendarService {
  async getAll(calendarQuery: CalendarQuery) {
    return await http.get<Array<CalendarDay>>('/calendar/dietDays', {
      params: {dateMin: calendarQuery.dateMin, dateMax: calendarQuery.dateMax},
    });
  }
}

export default new CalendarService();
