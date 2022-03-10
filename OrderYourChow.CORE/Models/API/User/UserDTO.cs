namespace OrderYourChow.CORE.Models.API.User
{
    public class UserDTO
    {
        public int? UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int MultiplierDiet { get; set; }

    }
}
