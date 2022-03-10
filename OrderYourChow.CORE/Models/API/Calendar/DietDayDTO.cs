namespace OrderYourChow.CORE.Models.API.Calendar
{
    public class DietDayDTO
    {
        public string Day { get; set; }
        public int DietDayId { get; set; }
    }

    public sealed class EmptyDietDayDTO : DietDayDTO
    {

    }
}
