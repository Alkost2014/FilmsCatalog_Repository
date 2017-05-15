namespace FilmsCatalog.Models
{
    using DomainModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class DatabaseContext : DbContext
    {
        //static DatabaseContext()
        //{
        //    Database.SetInitializer(new ContextInitializer());
        //}

        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Film> Films { get; set; }
    }

    //class ContextInitializer : DropCreateDatabaseAlways<DatabaseContext>
    //{
    //    protected override void Seed(DatabaseContext db)
    //    {
    //        User user1 = new User { Name = "User1", Email = "user1@test.ru", Password = "qaz321" };
    //        User user2 = new User { Name = "User2", Email = "user2@test.ru", Password = "qaz321" };

    //        db.Users.Add(user1);
    //        db.Users.Add(user2);
    //        db.SaveChanges();

    //        Film film1 = new Film { Caption = "����� 1", Description = "��� ����", Year = new DateTime(2017, 5, 1), Producer = "�����", Poster = "", User = user1 };
    //        Film film2 = new Film { Caption = "����� 2", Description = "��� ����", Year = new DateTime(2017, 5, 2), Producer = "�����", Poster = "", User = user2 };
    //        Film film3 = new Film { Caption = "����� 3", Description = "��� ����", Year = new DateTime(2017, 5, 3), Producer = "�����", Poster = "", User = user1 };
    //        Film film4 = new Film { Caption = "����� 4", Description = "��� ����", Year = new DateTime(2017, 5, 4), Producer = "�����", Poster = "", User = user2 };

    //        db.Films.AddRange(new List<Film>() { film1, film2, film3, film4 });
    //        db.SaveChanges();
    //    }
    //}
}