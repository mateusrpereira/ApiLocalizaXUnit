namespace Api.Domain.Dtos.User
{
    public class UserDtoCreateResult
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
