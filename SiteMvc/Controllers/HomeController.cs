using EduApi.Siniflar;
using EduApi.SiteModels;
using Newtonsoft.Json;
using SiteMvc.App_Classes;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace SiteMvc.Controllers
{
    [AllActionRun]
    public class HomeController : Controller
    {
        FacebookIslemleri Face = new FacebookIslemleri();
        string googleplus_redirect_url = "https://eduadvisor.co.uk/Google-Profil",
            googleplus_client_id = "748278992922-j3eoohvfu93m6vb9kf2p9u6o28cvqqfb.apps.googleusercontent.com",
            googleplus_client_sceret = "T-gdkcDabg0ehxpZfQ8aCUEQ", linkedin_client_id = "780adj5g3n8a03", linkedin_client_sceret = "gJckJJuYAtdTBE2d",
            linkedin_redirect_url = "https://eduadvisor.co.uk/Linkedin-Profil";
        public ActionResult Index(string ReturnUrl)
        {
            if (ReturnUrl != null)
                ViewBag.ReturnUrl = ReturnUrl;
            List<dll.Models.egitim_turleri> EgitimTurleri = (List<dll.Models.egitim_turleri>)ViewBag.EgitimTurleri;
            AnasayfaIcerikModel icerikler = new AnasayfaIcerikModel();
            icerikler.enler = site_islem.AnasayfaOkullar(HttpContext.Session["Dil"].ToString());
            #region tablar
            List<AnasayfaTabModel> temp_tabs = new List<AnasayfaTabModel>();
            temp_tabs.Add(new AnasayfaTabModel
            {
                id = "#dilokulu",
                image_url = "/Content/img/dilokulu.png",
                baslik = Resources.Varsayilan.baslik_dil_okullari,
                deger = "ilk"
            });
            temp_tabs.Add(new AnasayfaTabModel
            {
                id = "#universite",
                image_url = "/Content/img/unimenu.png",
                baslik = Resources.Varsayilan.baslik_universite,
                deger = "diger"
            });
            temp_tabs.Add(new AnasayfaTabModel
            {
                id = "#kolej",
                image_url = "/Content/img/kolejmenu.png",
                baslik = Resources.Varsayilan.baslik_kolej,
                deger = "diger"
            });

            temp_tabs.Add(new AnasayfaTabModel
            {
                id = "#lise",
                image_url = "/Content/img/lisemenu.png",
                baslik = Resources.Varsayilan.baslik_lise,
                deger = "diger",
            });

            temp_tabs.Add(new AnasayfaTabModel
            {
                id = "#diger",
                image_url = "/Content/img/digermenu.png",
                baslik = Resources.Varsayilan.baslik_diger,
                deger = "diger"
            });
            #endregion
            icerikler.tabs = temp_tabs;
            icerikler.yorumlar = site_islem.AnasayfaYorumVeOkullar(HttpContext.Session["Dil"].ToString());
            icerikler.arama_back = site_islem.AramaBackGetir();
            icerikler.ilkyorum = site_islem.IlkYorumGetir(HttpContext.Session["Dil"].ToString());
            return View(icerikler);
        }
        public ActionResult Footer(string gozukecek)
        {
            string view = "";
            switch (gozukecek)
            {
                case "Hakkimizda":
                    ViewBag.SayfaIcerik = site_islem.SayfaGetir(gozukecek);
                    ViewBag.Title = "Hakkımızda";
                    view = "Hakkimizda.cshtml";
                    break;
                case "Misyonumuz":
                    ViewBag.SayfaIcerik = site_islem.SayfaGetir(gozukecek);
                    ViewBag.Title = "Misyonumuz";
                    view = "MisyonVizyon.cshtml";
                    break;
                case "Kullanim-Kosullari":
                    ViewBag.SayfaIcerik = site_islem.SayfaGetir(gozukecek);
                    ViewBag.Title = "Kullanim-Kosullari";
                    view = "KullanimKosullari.cshtml";
                    break;
                case "Gizlilik-Politikasi":
                    ViewBag.SayfaIcerik = site_islem.SayfaGetir(gozukecek);
                    ViewBag.Title = "Gizlilik-Politikasi";
                    view = "GizlilikPolitikasi.cshtml";
                    break;
                case "Sorulan-Sorular":
                    ViewBag.Sorular = site_islem.SorulanSorular();
                    ViewBag.Title = "Sorulan-Sorular";
                    view = "SorulanSorular.cshtml";
                    break;
                case "Site-Haritasi":
                    ViewBag.SiteHaritasi = site_islem.SiteHaritasi(HttpContext.Session["Dil"].ToString());
                    ViewBag.Title = "Site-Haritasi";
                    view = "SiteHaritasi.cshtml";
                    break;
                case "Bize-Ulasin": ViewBag.Title = "Bize-Ulasin"; view = "BizeUlasin.cshtml"; break;
                default:
                    return RedirectToRoute("Home");
            }
            return View("~/Views/Diger/" + view);
        }
        [HttpPost]
        public PartialViewResult SehirleriGetir(int u_id)
        {
            List<ValueTextModel> sehirler = site_islem.SehirleriGetir(u_id);
            return PartialView(sehirler);
        }
        public PartialViewResult UlkeGetir()
        {
            List<ValueTextModel> ulkeler = site_islem.UlkeleriGetir(HttpContext.Session["Dil"].ToString());
            return PartialView("SehirleriGetir", ulkeler);
        }
        [HttpPost]
        public string BizeUlasin(BizeUlasinModel data)
        {
            try
            {
                return site_islem.BizeUlasinGonder(data, HttpContext.Request.UserHostAddress) ? "1" : "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public string AboneOl(string mail)
        {
            return site_islem.AboneOl(mail);
        }
        [HttpPost]
        public string SoruSor(string mail, string soru)
        {
            return site_islem.SoruSor(mail, soru);
        }
        [HttpPost]
        
        public void DilDegistir(string kod)
        {
            HttpContext.Session["Dil"] = kod;
        }

        
        [HttpPost]
        public string SifremiUnuttum(string mail)
        {
            return site_islem.SifreHatirlat(mail);
        }
        [HttpPost]
        public JavaScriptResult LinkedinGirisYap(string url)
        {
            Uri Url = new Uri(url);
            Session["oturum_sonrasi_sayfa"] = Url.AbsolutePath;
            var Link = "https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=" + linkedin_client_id + "&redirect_uri=" + linkedin_redirect_url + "&state=bl17scdgr"; ;
            return JavaScript("window.location = '" + Link + "'");
        }
        [HttpPost]
        public JavaScriptResult FacebookGirisYap(string url)
        {
            Uri Url = new Uri(url);
            Session["oturum_sonrasi_sayfa"] = Url.AbsolutePath;
            var Link = Face.CrateLoginUrl();
            return JavaScript("window.location = '" + Link + "'");
        }
        [HttpPost]
        public JavaScriptResult GoogleGirisYap(string url)
        {
            Uri Url = new Uri(url);
            Session["oturum_sonrasi_sayfa"] = Url.AbsolutePath;
            var Link = "https://accounts.google.com/o/oauth2/auth?response_type=code&redirect_uri=" +
                googleplus_redirect_url +
                "&scope=https://www.googleapis.com/auth/userinfo.email%20https://www.googleapis.com/auth/userinfo.profile&client_id=" +
                googleplus_client_id;
            return JavaScript("window.location = '" + Link + "'");
        }
        [HttpGet]
        public void LinkedinProfil(string code)
        {
            if (code != null)
            {
                /*Get Access Token and Refresh Token*/
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://www.linkedin.com/oauth/v2/accessToken");
                webRequest.Method = "POST";
                string parameters = "code=" + code + "&client_id=" + linkedin_client_id + "&client_secret=" +
                    linkedin_client_sceret + "&redirect_uri=" + linkedin_redirect_url + "&grant_type=authorization_code";
                byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                Stream postStream = webRequest.GetRequestStream();

                // Add the post data to the web request
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();
                WebResponse response = webRequest.GetResponse();
                postStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(postStream);
                string responseFromServer = reader.ReadToEnd();
                LinkedinAccessToken serStatus = JsonConvert.DeserializeObject<LinkedinAccessToken>(responseFromServer);
                /*End*/
                getlinkedinuserdataSer(serStatus.access_token);
            }
        }

        private void getlinkedinuserdataSer(string access_token)
        {
            using (var client = new HttpClient())
            {
                var urlProfile = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,picture-url,email-address)?format=json&oauth2_access_token=" + access_token;
                var response = client.GetAsync(urlProfile).Result;

                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    LinkedinUserOutputData serStatus = JsonConvert.DeserializeObject<LinkedinUserOutputData>(responseString);
                    if (site_islem.BireyselKayitOlKontrol(serStatus.emailAddress))
                    {
                        FormsAuthentication.SetAuthCookie(serStatus.emailAddress, true);
                        Session["giris_yapan"] = serStatus.emailAddress;
                        if (Session["oturum_sonrasi_sayfa"] != null)
                            Response.Redirect(Session["oturum_sonrasi_sayfa"].ToString());
                        else
                            Response.RedirectToRoute("Home");
                        return;
                    }
                    string resimadi = "Eduadvisor-uye-" + DateTime.Now.ToString("yyyyMMddHmmss");
                    try
                    {
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData(serStatus.pictureUrl);
                        MemoryStream ms = new MemoryStream(bytes);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                        string extension = "";
                        if (ImageFormat.Jpeg.Equals(img.RawFormat))
                            extension = ".jpg";
                        else if (ImageFormat.Png.Equals(img.RawFormat))
                            extension = ".png";
                        resimadi += extension;
                        if (!extension.Equals(""))
                            img.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/kul_profil/" + resimadi);
                        else
                            resimadi = "profil.png";
                    }
                    catch (Exception)
                    {
                        resimadi = "profil.png";
                    }
                    string sonuc = site_islem.BireyselKayitOl(serStatus.firstName, serStatus.lastName, resimadi, serStatus.emailAddress, islem.randomSifreUret(), true, 3);
                    if (sonuc.Equals("1") || sonuc.Equals("-9"))
                    {
                        Session["giris_yapan"] = serStatus.emailAddress;
                    }
                }
            }
            if (Session["oturum_sonrasi_sayfa"] != null)
                Response.Redirect(Session["oturum_sonrasi_sayfa"].ToString());
            else
                Response.RedirectToRoute("Home");
        }
        [HttpGet]
        public void FacebookProfil(string code)
        {
            if (code != null)
            {
                string state = "";
                string type = "";
                dynamic token = Face.GetAccessToken(code, state, type);
                FacebookProfilModel Profil = Face.GetUserInfo(token);
                if (site_islem.BireyselKayitOlKontrol(Profil.Email))
                {
                    FormsAuthentication.SetAuthCookie(Profil.Email, true);
                    Session["giris_yapan"] = Profil.Email;
                    if (Session["oturum_sonrasi_sayfa"] != null)
                        Response.Redirect(Session["oturum_sonrasi_sayfa"].ToString());
                    else
                        Response.RedirectToRoute("Home");
                    return;
                }
                string resimadi = "Eduadvisor-uye-" + DateTime.Now.ToString("ddMMyyyyHHmmssffff");
                try
                {
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(string.Format("http://graph.facebook.com/{0}/picture", Profil.Id));
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    string extension = "";
                    if (ImageFormat.Jpeg.Equals(img.RawFormat))
                        extension = ".jpg";
                    else if (ImageFormat.Png.Equals(img.RawFormat))
                        extension = ".png";
                    resimadi += extension;
                    if (!extension.Equals(""))
                        img.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/kul_profil/" + resimadi);
                    else
                        resimadi = "profil.png";
                }
                catch (Exception)
                {
                    resimadi = "profil.png";
                }
                string sonuc = site_islem.BireyselKayitOl(Profil.isim, Profil.soyisim, resimadi, Profil.Email, islem.randomSifreUret(), true, 0);
                if (sonuc.Equals("1") || sonuc.Equals("-9"))
                {
                    FormsAuthentication.SetAuthCookie(Profil.Email, true);
                    Session["giris_yapan"] = Profil.Email;
                }
            }
            if (Session["oturum_sonrasi_sayfa"] != null)
                Response.Redirect(Session["oturum_sonrasi_sayfa"].ToString());
            else
                Response.RedirectToRoute("Home");
        }
        [HttpGet]
        public void GoogleProfil(string code)
        {
            if (code != null)
            {
                /*Get Access Token and Refresh Token*/
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                webRequest.Method = "POST";
                string parameters = "code=" + code + "&client_id=" + googleplus_client_id + "&client_secret=" + googleplus_client_sceret + "&redirect_uri=" + googleplus_redirect_url + "&grant_type=authorization_code";
                byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                Stream postStream = webRequest.GetRequestStream();

                // Add the post data to the web request
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();
                WebResponse response = webRequest.GetResponse();
                postStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(postStream);
                string responseFromServer = reader.ReadToEnd();
                GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);
                /*End*/
                getgoogleplususerdataSer(serStatus.access_token);
            }
        }
        private void getgoogleplususerdataSer(string access_token)
        {
            using (var client = new HttpClient())
            {
                var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + access_token;
                var response = client.GetAsync(urlProfile).Result;

                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    GoogleUserOutputData serStatus = JsonConvert.DeserializeObject<GoogleUserOutputData>(responseString);
                    if (site_islem.BireyselKayitOlKontrol(serStatus.email))
                    {
                        FormsAuthentication.SetAuthCookie(serStatus.email, true);
                        Session["giris_yapan"] = serStatus.email;
                        if (Session["oturum_sonrasi_sayfa"] != null)
                            Response.Redirect(Session["oturum_sonrasi_sayfa"].ToString());
                        else
                            Response.RedirectToRoute("Home");
                        return;
                    }
                    string resimadi = "Eduadvisor-uye-" + DateTime.Now.ToString("ddMMyyyyHHmmssffff");
                    try
                    {
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData(serStatus.picture);
                        MemoryStream ms = new MemoryStream(bytes);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                        string extension = "";
                        if (ImageFormat.Jpeg.Equals(img.RawFormat))
                            extension = ".jpg";
                        else if (ImageFormat.Png.Equals(img.RawFormat))
                            extension = ".png";
                        resimadi += extension;
                        if (!extension.Equals(""))
                            img.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/kul_profil/" + resimadi);
                        else
                            resimadi = "profil.png";
                    }
                    catch (Exception)
                    {
                        resimadi = "profil.png";
                    }
                    string sonuc = site_islem.BireyselKayitOl(serStatus.given_name, serStatus.family_name, resimadi, serStatus.email, islem.randomSifreUret(), true, 1);
                    if (sonuc.Equals("1") || sonuc.Equals("-9"))
                    {
                        FormsAuthentication.SetAuthCookie(serStatus.email, true);
                        Session["giris_yapan"] = serStatus.email;
                    }
                }
            }
            if (Session["oturum_sonrasi_sayfa"] != null)
                Response.Redirect(Session["oturum_sonrasi_sayfa"].ToString());
            else
                Response.RedirectToRoute("Home");
        }
        [HttpPost]
        public string YakinOkullar(string lat, string lng)
        {
            AnasayfaYakinOkulModel donecek = new AnasayfaYakinOkulModel();
            try
            {
                donecek = site_islem.YakinOkullarGetir(lat, lng, Resources.Varsayilan.b_detayliincele, Resources.Varsayilan.b_indirimlipng);
            }
            catch (Exception)
            {
            }
            return JsonConvert.SerializeObject(donecek);
        }
        [HttpPost]
        public string UlkeSehirEyaletGetir()
        {
            List<ValueTextModel> veriler = site_islem.UlkeEyaletSehirGetir(HttpContext.Session["Dil"].ToString());
            return JsonConvert.SerializeObject(veriler);
        }
        [HttpPost]
        public PartialViewResult EyaletGetir(int ulke_id)
        {
            List<ValueTextModel> eyaletler = site_islem.EyaletGetir(ulke_id);
            return PartialView("JustValueTextModel", eyaletler);
        }
        [HttpPost]
        public PartialViewResult EyaletSehirleriGetir(int e_id)
        {
            List<ValueTextModel> sehirler = site_islem.EyaletSehirleriGetir(e_id);
            return PartialView("SehirleriGetir", sehirler);
        }
        [HttpPost]
        public string OkulOnerisi()
        {
            List<ValueTextModel> veriler = site_islem.OkulOnerisiGetir();
            return JsonConvert.SerializeObject(veriler);
        }
        [HttpPost]
        public string SehirleriGetirTextValue(int u_id)
        {
            List<ValueTextModel> sehirler = site_islem.SehirleriGetir(u_id);
            return JsonConvert.SerializeObject(sehirler);
        }
    }
}