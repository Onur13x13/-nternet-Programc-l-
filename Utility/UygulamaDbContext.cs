using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

// Veri tabanında ef tablo oluşturması için ilgili model sınıfları buraya eklenmeli
namespace WebApplication2.Utility
{
    public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }


        public DbSet<KitapTuru> KitapTurleri { get; set; }

        public DbSet<Kitap> Kitaplar { get; set; }

        public DbSet<Kiralama> Kiralamalar { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
