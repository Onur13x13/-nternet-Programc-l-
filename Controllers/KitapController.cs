using Microsoft.AspNetCore.Mvc;
using WebApplication2.Utility;
using WebApplication2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Index()
        {
            List<Kitap> objKitapList = _kitapRepository.GetAll(includeProps: "KitapTuru").ToList();
            return View(objKitapList);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()
            });

            ViewBag.KitapTuruList = KitapTuruList;

            if (id == null || id == 0)
            {
                // ekleme
                return View();
            }
            else
            {
                // güncelleme
                Kitap kitapVt = _kitapRepository.Get(u => u.Id == id);
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);
            }
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                if (file != null && file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }
            }

            if (kitap.Id == 0)
            {
                _kitapRepository.Ekle(kitap);
                TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu!";
            }
            else
            {
                _kitapRepository.Guncelle(kitap);
                TempData["basarili"] = "Kitap Başarıyla Güncellendi!";
            }

            _kitapRepository.Kaydet();
            return RedirectToAction("Index", "Kitap");
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kitap kitapVt = _kitapRepository.Get(u => u.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }

            return View(kitapVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kitap kitap = _kitapRepository.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }

            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Kitap Başarıyla Silindi!";
            return RedirectToAction("Index", "Kitap");
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult EkleGuncelleAjax([FromBody] Kitap kitap, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                if (file != null && file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }
            }

            if (kitap.Id == 0)
            {
                _kitapRepository.Ekle(kitap);
                TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu!";
            }
            else
            {
                _kitapRepository.Guncelle(kitap);
                TempData["basarili"] = "Kitap Başarıyla Güncellendi!";
            }

            _kitapRepository.Kaydet();
            return Json(new { success = true, message = "İşlem başarıyla gerçekleştirildi." });
        }
    }
}
