

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class KitapTuru
    {
        [Key]   // Id Boş olamaz
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap Tür Adı Boş Bırakılamaz!")] // not null (Ad boş olamaz)
        [MaxLength(25)] // maksimum 25 karaktere kadar izin veriyor
        [DisplayName("Kitap Türü Adı")] // ekranda ad yerine Kitap Türü Adı gözükecek
        public string Ad { get; set; }

    }
}
