using System.Linq.Expressions;
using WebApplication2.Models;
using WebApplication2.Utility;

namespace WebApplication2.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
    {
        private UygulamaDbContext _uygulamaDbContext;
        public KitapTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public KitapTuru Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _uygulamaDbContext.Set<KitapTuru>().Find(id);
        }

        public void Ekle(KitapTuru kitapTuru)
        {
            _uygulamaDbContext.Set<KitapTuru>().Add(kitapTuru);
            _uygulamaDbContext.SaveChanges();
        }

        public void Guncelle(KitapTuru kitapTuru)
        {
            _uygulamaDbContext.Update(kitapTuru);
            _uygulamaDbContext.SaveChanges();
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
        public void Sil(KitapTuru kitapTuru)
        {
            _uygulamaDbContext.Set<KitapTuru>().Remove(kitapTuru);
            _uygulamaDbContext.SaveChanges();
        }
    }
}
