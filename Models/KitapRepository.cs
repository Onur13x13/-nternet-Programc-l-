using System.Linq.Expressions;
using WebApplication2.Models;
using WebApplication2.Utility;

namespace WebApplication2.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        private UygulamaDbContext _uygulamaDbContext;
        public KitapRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Kitap kitap)
        {
            _uygulamaDbContext.Update(kitap);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
