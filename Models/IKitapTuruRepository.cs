using WebApplication2.Models;

namespace WebApplication2.Models
{
    public interface IKitapTuruRepository : IRepository<KitapTuru>
    {
        KitapTuru Get(int? id);
        void Guncelle(KitapTuru kitapTuru);
        void Kaydet();
        void Sil(KitapTuru kitapTuru);
    }
}
