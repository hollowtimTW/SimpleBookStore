using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Models;
using SimpleBookStore.Utility.Helper;

namespace SimpleBookStore.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<EmailVerificationCode> EmailVerificationCodes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var now = TimeHelper.Now();

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "文學小說",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now
                },
                new Category
                {
                    Id = 2,
                    Name = "歷史文化",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now
                },
                new Category
                {
                    Id = 3,
                    Name = "旅遊休閒",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now
                },
                new Category
                {
                    Id = 4,
                    Name = "電腦資訊",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now
                },
                new Category
                {
                    Id = 5,
                    Name = "心理勵志",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now
                }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    Name = "林語川",
                    Bio = "熱愛文字與城市觀察，擅長描寫人與人之間微妙的情感。",
                    CreatedAt = now
                },
                new Author
                {
                    Id = 2,
                    Name = "張書涵",
                    Bio = "歷史科普作家，致力於用輕鬆的語言讓讀者了解複雜歷史事件。",
                    CreatedAt = now
                },
                new Author
                {
                    Id = 3,
                    Name = "陳彥廷",
                    Bio = "自由旅人與攝影記錄者，筆下風景充滿詩意與溫度。",
                    CreatedAt = now
                },
                new Author
                {
                    Id = 4,
                    Name = "高雅雯",
                    Bio = "軟體工程師轉職作家，擅長將抽象技術轉化為易懂故事。",
                    CreatedAt = now
                },
                new Author
                {
                    Id = 5,
                    Name = "李思瑜",
                    Bio = "心理學背景出身，專注於內在成長與心靈療癒領域。",
                    CreatedAt = now
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "靈光之城：The Rise of Mindlight",
                    CategoryId = 1,
                    AuthorId = 1,
                    Publisher = "幻語出版",
                    PublishedDate = new DateTime(2023, 2, 12),
                    Price = 420,
                    Description = "當思想成為力量，城市也會呼吸。",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Product
                {
                    Id = 2,
                    Title = "時砂紀元：Chrono Sands",
                    CategoryId = 2,
                    AuthorId = 2,
                    Publisher = "時代盒子文化",
                    PublishedDate = new DateTime(2022, 8, 5),
                    Price = 499,
                    Description = "一部顛覆歷史視角的時空探險小說。",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now.AddMinutes(2),
                    UpdatedAt = now.AddMinutes(2)
                },
                new Product
                {
                    Id = 3,
                    Title = "Walkscape：城市微旅行指南",
                    CategoryId = 3,
                    AuthorId = 3,
                    Publisher = "步路生活",
                    PublishedDate = new DateTime(2021, 11, 20),
                    Price = 310,
                    Description = "探索日常的陌生角落，走出你的Walkscape。",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now.AddMinutes(3),
                    UpdatedAt = now.AddMinutes(3)
                },
                new Product
                {
                    Id = 4,
                    Title = "Code Alchemy：寫給未來的程式語言",
                    CategoryId = 4,
                    AuthorId = 1,
                    Publisher = "幻碼實驗室",
                    PublishedDate = new DateTime(2024, 1, 3),
                    Price = 650,
                    Description = "結合語言學與人工智慧的程式思維演進之路。",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now.AddMinutes(4),
                    UpdatedAt = now.AddMinutes(4)
                },
                new Product
                {
                    Id = 5,
                    Title = "Reset Me：重新啟動的勇氣",
                    CategoryId = 5,
                    AuthorId = 2,
                    Publisher = "心靈迴響",
                    PublishedDate = new DateTime(2020, 5, 25),
                    Price = 370,
                    Description = "給被生活困住的你，一場重啟心靈的旅程。",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = now.AddMinutes(5),
                    UpdatedAt = now.AddMinutes(5)
                }
            );

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    Code = "WELCOME10",
                    Description = "新會員首購折扣",
                    DiscountAmount = 100,
                    MinimumSpend = 500,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(90),
                    IsActive = true
                },
                new Coupon
                {
                    Id = 2,
                    Code = "SUMMER20",
                    Description = "夏季特賣全館折扣",
                    DiscountAmount = 200,
                    MinimumSpend = 1000,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(90),
                    IsActive = true
                },
                new Coupon
                {
                    Id = 3,
                    Code = "FREESHIP",
                    Description = "免運優惠券",
                    DiscountAmount = 60,
                    MinimumSpend = 0,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(90),
                    IsActive = true
                },
                new Coupon
                {
                    Id = 4,
                    Code = "VIP50",
                    Description = "VIP會員專屬優惠",
                    DiscountAmount = 500,
                    MinimumSpend = 2500,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(90),
                    IsActive = true
                },
                new Coupon
                {
                    Id = 5,
                    Code = "BIRTHDAY30",
                    Description = "生日禮遇折扣券",
                    DiscountAmount = 300,
                    MinimumSpend = 1500,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(90),
                    IsActive = true
                }
            );

        }
    }
}
