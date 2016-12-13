using System.Data.Entity;
using ASPNETSimple.DAL.Entities;

namespace ASPNETSimple.DAL.Context
{

    public partial class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public EFContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<EFContext>(new UsersDbInitializer());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    public class UsersDbInitializer : DropCreateDatabaseAlways<EFContext>
    {
        protected override void Seed(EFContext db)
        {
            db.Users.Add(new User { Id = "1", Email = "test1@g.com", FirstName = "User1", Password = "qwerty123" });
            db.Users.Add(new User { Id = "2", Email = "test2@g.com", FirstName = "User2", Password = "qwerty123" });
            db.Users.Add(new User { Id = "3", Email = "test3@g.com", FirstName = "User3", Password = "qwerty123" });
            db.Users.Add(new User { Id = "4", Email = "test4@g.com", FirstName = "User4", Password = "qwerty123" });
            db.SaveChanges();
        }
    }
}
