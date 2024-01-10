﻿using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IKitapRepository _kitapRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository = kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();
            return View(objKiralamaList);
        }
        //GET
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });

            ViewBag.KitapList = KitapList;

            if (id == null || id == 0)
            {
                // ekleme
                return View();
            }
            else
            {
                // güncelleme
                Kiralama kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
                if (kiralamaVt == null)
                {
                    return NotFound();
                }
                return View(kiralamaVt);
            }
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
            if (kiralama.Id == 0)
            {
                _kiralamaRepository.Ekle(kiralama);
                TempData["basarili"] = "Yeni Kiralama Kaydı Başarıyla Oluşturuldu!";
            }
            else
            {
                _kiralamaRepository.Guncelle(kiralama);
                TempData["basarili"] = "Kiralama Kyıt Güncelleme Başarılı!";
            }

            _kiralamaRepository.Kaydet();
            return RedirectToAction("Index", "Kiralama");
        }

        // GET ACTION
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kiralama kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralamaVt == null)
            {
                return NotFound();
            }

            return View(kiralamaVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);

            if (kiralama == null)
            {
                return NotFound();
            }

            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme İşlemi Başarılı";
            return RedirectToAction("Index", "Kiralama");
        }
    }
}
