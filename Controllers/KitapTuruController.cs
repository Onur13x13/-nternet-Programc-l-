using Microsoft.AspNetCore.Mvc;
using WebApplication2.Utility;
using WebApplication2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepository;

        public KitapTuruController(IKitapTuruRepository kitapTuruRepository)
        {
            _kitapTuruRepository = kitapTuruRepository;
        }

        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList = _kitapTuruRepository.GetAll().ToList();
            return View(objKitapTuruList);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Ekle(kitapTuru);
                TempData["basarili"] = "Yeni Kitap Türü Başarıyla Oluşturuldu!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru kitapTuruVt = _kitapTuruRepository.Get(id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }

        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Guncelle(kitapTuru);
                TempData["basarili"] = "Yeni Kitap Türü Başarıyla Güncellendi!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru kitapTuruVt = _kitapTuruRepository.Get(id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            KitapTuru kitapTuru = _kitapTuruRepository.Get(id);
            if (kitapTuru == null)
            {
                return NotFound();
            }
            _kitapTuruRepository.Sil(kitapTuru);
            TempData["basarili"] = "Kitap Türü Başarıyla Silindi!";
            return RedirectToAction("Index");
        }
    }
}
