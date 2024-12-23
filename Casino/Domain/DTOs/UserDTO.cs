namespace Domain.DTO 
{
    public class UserDTO
    {
        public string? IdOfUser { get; set; }
        public string? Email { get; set; } 
        public string? UserName { get; set; } 
        public string? Name { get; set;}
        public string? Surname { get; set;}
        public string? Patronymic { get; set;}
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public bool? Admin { get; set; }
        public string? Token { get; set; }
        public int? Balance { get; set; }




    } 
}