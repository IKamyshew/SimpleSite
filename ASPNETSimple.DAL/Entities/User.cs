namespace ASPNETSimple.DAL.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        //public ICollection<Role> Roles { get; set; }
    }
}
