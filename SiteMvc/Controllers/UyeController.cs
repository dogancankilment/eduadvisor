using dll.Models;
using EduApi.Siniflar;
using EduApi.SiteModels;
using SiteMvc.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SiteMvc.Controllers
{
    [AllActionRun]
    public class UyeController : Controller
    {
        public ActionResult Mesajlarim(string v)
        {
            if (v != null)
                ViewBag.acilacak = "tabs_" + v;
            if (HttpContext.Session["giris_yapan"] == null)
                return RedirectToAction("Index", "Home", new { ReturnUrl = Request.Url.AbsolutePath });
            UyeMesajlarModel model = site_islem.MesajlarimSayfaVerileri(HttpContext.Session["giris_yapan"].ToString());
            return View(model);
        }
        public PartialViewResult MesajlariGetir()
        {
            List<uye_mesajlari> mesajlar = new List<uye_mesajlari>();
            if (HttpContext.Session["giris_yapan"] != null)
                mesajlar = site_islem.UyeMesajlariGetir(HttpContext.Session["giris_yapan"].ToString());
            return PartialView(mesajlar);
        }
        [HttpPost]
        public void GirisYap(string mail, string sifre)
        {
            if (site_islem.GirisYap(mail, sifre))
            {
                FormsAuthentication.SetAuthCookie(mail, true);
                HttpContext.Session["giris_yapan"] = mail;
            }
            else
                Convert.ToInt32("asd");
        }
        public void CikisYap()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session["giris_yapan"] = null;
        }
        [HttpPost]
        public string KayitOl(string mail, string sifre, string haber)
        {
            string sonuc = site_islem.BireyselKayitOl(mail.Split('@').First(), "", "profil.png", mail, sifre, haber == "on", 2);
            if (sonuc.Equals("1"))
            {
                FormsAuthentication.SetAuthCookie(mail, true);
                HttpContext.Session["giris_yapan"] = mail;
            }
            return sonuc;
        }
        [Authorize]
        public ActionResult UyeProfil()
        {
            if (HttpContext.Session["giris_yapan"] == null)
                return RedirectToAction("Index", "Home", new { ReturnUrl = Request.Url.AbsolutePath });
            uyeler uye = site_islem.UyeTamGetir(HttpContext.Session["giris_yapan"].ToString());
            return View(uye);
        }
        [Authorize]
        [HttpPost]
        public string UyeProfilKullanici(UyeProfilKullaniciModel data)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeProfilKullaniciGuncelle(data, HttpContext.Session["giris_yapan"].ToString());
                else
                    return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [Authorize]
        [HttpPost]
        public string UyeProfilResmi(string resim_data)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeProfilResimGuncelle(resim_data, HttpContext.Session["giris_yapan"].ToString());
                else
                    return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public PartialViewResult Kampusler(int okul_id)
        {
            List<ValueTextModel> kampusler = site_islem.GrupOkullariGetir(okul_id);
            return PartialView("JustValueTextModel", kampusler);
        }
        [HttpPost]
        public PartialViewResult ProgramTurleri(string egitim_id)
        {
            List<ValueTextModel> turler = site_islem.EgitimTuruneGoreProgramTurleriGetir(egitim_id);
            return PartialView("JustValueTextModel", turler);
        }
        [HttpPost]
        public PartialViewResult Programlar(string okul_seo)
        {
            List<ValueTextModel> programlar = site_islem.OkulProgramlariGetir(okul_seo);
            return PartialView("JustValueTextModel", programlar);
        }
        [HttpPost]
        public string ProfilEgitimEkle(UyeProfilEgitimModel data)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeProfilEgitimEkle(data, HttpContext.Session["giris_yapan"].ToString());
                else
                    return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpGet]
        public PartialViewResult ProfilEgitimlerGetir()
        {
            List<uye_okullari> model = new List<uye_okullari>();
            if (HttpContext.Session["giris_yapan"] != null)
            {
                model = site_islem.UyeOkullariGetir(HttpContext.Session["giris_yapan"].ToString());
                return PartialView(model);
            }
            return PartialView(model);
        }
        [HttpPost]
        public string ProfilEgitimSil(int id)
        {
            try
            {
                return site_islem.UyeProfilEgitimSil(id);
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string SifreGuncelle(string mevcut, string yeni)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeProfilSifreGuncelle(mevcut, yeni, HttpContext.Session["giris_yapan"].ToString());
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string ProfilHesapDondur(string mevcut, string sebep)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                {
                    string sonuc = site_islem.UyeProfilDondur(mevcut, sebep, HttpContext.Session["giris_yapan"].ToString());
                    if (sonuc == "1")
                    {
                        FormsAuthentication.SignOut();
                        HttpContext.Session["giris_yapan"] = null;
                    }
                    return sonuc;
                }
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string ProfilYorumSil(int id)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeProfilYorumSil(id, HttpContext.Session["giris_yapan"].ToString());
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpGet]
        public PartialViewResult ProfilYorumlariGetir()
        {
            List<yorum> yorumlar = new List<yorum>();
            if (HttpContext.Session["giris_yapan"] != null)
                yorumlar = site_islem.UyeProfilYorumlariGetir(HttpContext.Session["giris_yapan"].ToString());
            return PartialView(yorumlar);
        }
        [HttpPost]
        public PartialViewResult ProfilYorumDetayGetir(int id)
        {
            yorum y = new yorum();
            if (HttpContext.Session["giris_yapan"] != null)
                y = site_islem.UyeProfilYorumGetir(id, HttpContext.Session["giris_yapan"].ToString());
            if (y == null)
                y = new yorum();
            return PartialView(y);
        }
        [HttpPost]
        public string ProfilYorumResimSil(int id)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.ProfilYorumResimSil(id, HttpContext.Session["giris_yapan"].ToString());
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string ProfilYorumGuncelle(ProfilYorumGuncelleModel yorum)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.ProfilYorumDetayGuncelle(yorum, HttpContext.Session["giris_yapan"].ToString());
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpGet]
        public ActionResult UyeDogrula(string dog_kod)
        {
            if (dog_kod == null)
                return RedirectToAction("Index", "Home");
            else
            {
                string sonuc = site_islem.UyeDogrula(dog_kod);
                //if (sonuc != "0")
                //{
                //    HttpContext.Session["giris_yapan"] = sonuc;
                //    FormsAuthentication.SetAuthCookie(sonuc, false);
                //}
                ViewBag.dogrulama_sonucu = sonuc;
                return View();
            }
        }
        [HttpPost]
        public string ProfilUyeDogrula(string dogrulama_kodu)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.ProfilUyeDogrula(dogrulama_kodu, HttpContext.Session["giris_yapan"].ToString());
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string ProfilTekrarKodGonder()
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.ProfilTekrarKodGonder(HttpContext.Session["giris_yapan"].ToString());
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string MesajGonder(string mesaj)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeMesajGonder(HttpContext.Session["giris_yapan"].ToString(), mesaj);
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpGet]
        public void MesajlariOkundu()
        {
            if (HttpContext.Session["giris_yapan"] != null)
                site_islem.UyeMesajlarOkundu(HttpContext.Session["giris_yapan"].ToString());
        }
        [HttpPost]
        public string BildirimSil(int id)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeBildirimCevapla(HttpContext.Session["giris_yapan"].ToString(), id, 0);
                return "-1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string BildirimOnayla(int id)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeBildirimCevapla(HttpContext.Session["giris_yapan"].ToString(), id, 1);
                return "-1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string YorumBegen(int id, int durum)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeYorumBegen(HttpContext.Session["giris_yapan"].ToString(), id, durum);
                return "-1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string YorumSikayetEt(int id, string neden,string sifre)
        {
            try
            {
                if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.UyeYorumSikayetEt(HttpContext.Session["giris_yapan"].ToString(), id, neden,sifre);
                return "-9";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public PartialViewResult UyeProfilOkullarGetir(string egitim_id)
        {
            List<ValueTextModel> gruplar = site_islem.EgitimTuruneGoreOkulGruplariGetir(egitim_id);
            return PartialView(gruplar);
        }
    }
}