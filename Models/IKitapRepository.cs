using WebApplication2.Models;

namespace WebApplication2.Models
{
    public interface IKitapRepository : IRepository<Kitap>
    {
        void Guncelle(Kitap kitap);
        void Kaydet();
    }
}
