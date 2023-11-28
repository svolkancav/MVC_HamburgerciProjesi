using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.MenuServices;
using HamburgerciProject.Application.Services.SepetServices;
using HamburgerciProject.Application.Services.SiparisServices;
using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MVC_HamburgerciProjesi.Models.Enum;
using System.Data;
using System.Security.Claims;
using X.PagedList;

namespace HamburgerciProject.Presentation.Controllers
{
    #region eski

    //public class SiparisController : Controller
    //{

    //    private readonly ISiparisService _siparisService;

    //    public SiparisController(ISiparisService siparisService)
    //    {
    //        _siparisService = siparisService;
    //    }
    //    public async Task<IActionResult> Index(string deger, int p = 3) // int p değerini pagination için verdik.using X.PagedList; kullanıldı. string deger ise search için 
    //    {
    //        if (!string.IsNullOrEmpty(deger))
    //        {
    //            List<CreateSiparisDTO> siparis = await _siparisService.GetSiparisler();
    //            List<CreateSiparisDTO> seciliSiparisler = siparis.Where(x => x.İçerik.ToLower().Contains(deger.ToLower())).ToList();

    //            return View(seciliSiparisler.ToPagedList(p, 3));
    //        }
    //        else
    //        {

    //            List<CreateSiparisDTO> siparisler = await _siparisService.GetSiparisler();
    //            return View(siparisler.ToPagedList(p, 3));

    //        }

    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Create(List<MenuVM> menuler, List<EkstraMalzemeVM> ekMalzemeler)
    //    {
    //        CreateSiparisDTO siparis = await _siparisService.SiparisOlustur(menuler, ekMalzemeler);

    //        await _siparisService.Create(siparis);

    //        return RedirectToAction("Index");


    //    }

    #endregion

    public class SiparisController : Controller
    {
        private readonly IMenuService menuService;
        private readonly ISepetService sepetService;
        private readonly ISiparisService siparisService;

        public SiparisController(IMenuService menuService, ISepetService sepetService, ISiparisService siparisService)
        {
            this.menuService = menuService;
            this.sepetService = sepetService;
            this.siparisService = siparisService;

        }


        public async Task<IActionResult> SiparisOlustur()
        {
            CreateSiparisDTO createSiparis = new CreateSiparisDTO();
            return View(createSiparis);
        }

        public async Task<IActionResult> Index(string deger, int p = 3) // int p değerini pagination için verdik.using X.PagedList; kullanıldı. string deger ise search için 
        {
            if (!string.IsNullOrEmpty(deger))
            {
                List<CreateSiparisDTO> siparis = await siparisService.GetSiparisler();
                List<CreateSiparisDTO> seciliSiparisler = siparis.Where(x => x.İçerik.ToLower().Contains(deger.ToLower())).ToList();
                if (seciliSiparisler is not null)
                {
                    return View(seciliSiparisler.ToPagedList(p, 3));

                }
                else
                {
                    return View(siparis.ToPagedList(p, 3));
                }


            }
            else
            {

                List<CreateSiparisDTO> siparisler = await siparisService.GetSiparisler();
                return View(siparisler.ToPagedList(p, 3));

            }

        }

        [HttpPost]
        public async Task<IActionResult> SiparisGonder(SiparisDTO siparisDTO)
        {
            MenuDTO menu = await menuService.GetById(siparisDTO.MenuID);

            siparisDTO.MenuID = menu.Id;
            siparisDTO.MenuAdi = menu.MenuAdi;
            siparisDTO.ToplamTutar = menu.MenuFiyati;
            if (siparisDTO.Boyut == Boyut.Büyük) siparisDTO.ToplamTutar *= 1.4M;
            else if (siparisDTO.Boyut == Boyut.Orta) siparisDTO.ToplamTutar *= 1.2M;
            siparisDTO.ToplamTutar *= siparisDTO.Adedi;

            SepetDTO sepet = new SepetDTO()
            {
                MenuID = siparisDTO.MenuID,
                Adet = siparisDTO.Adedi,
                Boyut = siparisDTO.Boyut,
                Fiyat = siparisDTO.ToplamTutar,
                UserId = siparisDTO.UserId,
                Status = Domain.Enum.Status.Active
            };

            sepetService.Add(sepet);
            var sepettekiler = new List<SepetDTO>();
            sepettekiler = await sepetService.GetAll();
            var userSepet = sepettekiler.Where(x => x.UserId == siparisDTO.UserId).ToList();
            siparisDTO.Sepettekiler = userSepet;

            if (siparisDTO.Sepettekiler.Count > 1 && siparisDTO.ekleme == 1)
            {
                return PartialView("_SepetTemizlensinMi", siparisDTO);
            }

            return PartialView("_SiparisListesi", siparisDTO);
        }

        public async Task<IActionResult> SepetiOnayla(int id)
        {
            if (id != 0)
            {
                var sepettekiler = new List<SepetDTO>();
                sepettekiler = await sepetService.GetAll();
                var userSepet = sepettekiler.Where(x=>x.UserId == id);
                SepetDTO sepetDTO = new SepetDTO();
                
                return View(sepetDTO);

                //TODO sepetDTO.Sepettekiler doldurulup gönderilecek.
            }
            return View();
            
        }

        public async Task<IActionResult> SiparisOnayla(int id)
        {
            if (id != 0)
            {
                SepetDTO sepetDTO = new SepetDTO();
                sepetDTO = await sepetService.GetById(id);
                await sepetService.Add(sepetDTO);
                sepetService.DeleteById(sepetDTO.Id);
                return RedirectToAction("SiparisOlustur");
            }
            else
            {
                return View();
            }

            //var sepettekiler = new List<SepetDTO>();
            //sepettekiler = await sepetService.GetAll();
            //var userSepet = sepettekiler.Where(x => x.UserId == id).ToList();

            //foreach (SepetDTO item in userSepet)
            //{
            //    if (item.MenuID != null)
            //    {
            //        SiparislerMenuler siparislerMenu = new SiparislerMenuler()
            //        {
            //            SiparisID = siparis.ID,
            //            MenuID = (int)item.MenuID,
            //            Boyut = item.Boyut,
            //            Adet = item.Adet,
            //            TotalFiyat = item.Fiyat
            //        };
            //        siparisMenulerService.Add(siparislerMenu);
            //        sepetService.Delete(item);
            //    }
            //}


        }
        [HttpPost]
        public async Task<IActionResult> SepettenSil(SepetDTO sepettenSilDTO)
        {
            SepetDTO sepettenSilinecek = await sepetService.GetById(sepettenSilDTO.SepetId);
            if (sepettenSilinecek.Adet > 1)
            {
                sepettenSilinecek.Fiyat = (sepettenSilinecek.Fiyat / sepettenSilinecek.Adet) * (sepettenSilinecek.Adet - 1);
                sepettenSilinecek.Adet--;
                sepetService.Update(sepettenSilinecek);
            }
            else
            {
                sepetService.Delete(sepettenSilinecek);
            }
            SiparisDTO siparisGonderDTO = new();
            siparisGonderDTO.Sepettekiler = await sepetService.GetAll();
            var aktifSepet = siparisGonderDTO.Sepettekiler.Where(x => x.Status == Domain.Enum.Status.Active).ToList();

            return PartialView("_SiparisListesi", aktifSepet);
        }
        [HttpPost]
        public async Task<IActionResult> AdetArttır(SepetDTO sepettenSilDTO)
        {
            SepetDTO sepet = await sepetService.GetById(sepettenSilDTO.SepetId);
            sepet.Fiyat = (sepet.Fiyat / sepet.Adet) * (sepet.Adet + 1);
            sepet.Adet++;
            sepetService.Update(sepet);
            SiparisDTO siparisGonderDTO = new();
            siparisGonderDTO.Sepettekiler = await sepetService.GetAll();
            var aktifSepet = siparisGonderDTO.Sepettekiler.Where(x => x.Status == Domain.Enum.Status.Active).ToList();
            return PartialView("_SiparisListesi", aktifSepet);
        }
        public async Task<IActionResult> Siparis()
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            int userID = Convert.ToInt32(userIDClaim.Value);

            List<CreateSiparisDTO> siparisler = await siparisService.GetSiparisler();

            return View(siparisler.Where(x => x.UserId == userID).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> SepetTemizle(SepetDTO sepetDTO)
        {

            var sepettekiler = new List<SepetDTO>();
            sepettekiler = await sepetService.GetAll();
            var userSepet = sepettekiler.Where(x => x.UserId == sepetDTO.UserId).ToList();
            foreach (var item in userSepet)
            {
                sepetService.Delete(item);
            }

            return PartialView("_SiparisListesi");
        }

        [HttpPost]
        public async Task<IActionResult> SepetYukle(Sepet sepetTemizleDTO)
        {
            List<CreateSiparisDTO> siparisler = await siparisService.GetSiparisler();
            var userSiparisler = new List<CreateSiparisDTO>();
            userSiparisler = siparisler.Where(x => x.UserId == sepetTemizleDTO.UserId).ToList();
            return PartialView("_SiparisListesi", userSiparisler);
        }

    }




}

