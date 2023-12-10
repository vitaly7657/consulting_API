using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace m21_e2_API.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        //создание таблицы абонентов в базе данных
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<SiteText> SiteTexts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TagLine> TagLines { get; set; }
        //контекст подключения к БД
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated(); //создание базы данных если при запуске её нет
        }
    }
}
