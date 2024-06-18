namespace NET105_ASM_MVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
