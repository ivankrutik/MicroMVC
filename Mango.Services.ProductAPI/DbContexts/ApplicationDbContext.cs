using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = 1,
                Name = "Самоса",
                Price = 15,
                CategoryName = "Закуска",
                ImageUrl = string.Empty,
                Image = null,
                Description = "Жареное или печёное тесто с начинкой. Размер и форма различны, но наиболее распространённая в виде треугольника. Часто подаётся с соусами."
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = 2,
                Name = "Панир тикка",
                Price = decimal.Parse("13,99"),
                CategoryName = "Закуска",
                ImageUrl = string.Empty,
                Image = null,
                Description = "Индийское блюдо, приготовленное из кусочков панира, маринованных в специях и приготовленных на гриле в тандыре. Это вегетарианская альтернатива куриной тикке и другим мясным блюдам. Это популярное блюдо широко доступно в Индии и странах с индийской диаспорой"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = 3,
                Name = "Сладкий пирог",
                Price = decimal.Parse("10,99"),
                CategoryName = "Десерт",
                ImageUrl = string.Empty,
                Image = null,
                Description = "Хлебобулочное изделие из теста с начинкой, которое выпекается или жарится"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = 4,
                Name = "Пав бхаджи",
                Price = decimal.Parse("15"),
                CategoryName = "Первое блюдо",
                ImageUrl = string.Empty,
                Image = null,
                Description = "Блюдо быстрого питания из Индии, состоящее из густого овощного карри, которое подается с мягкой булочкой. Он возник в городе Мумбаи."
            });
        }
    }
}
