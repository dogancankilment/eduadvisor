using EduApi.Siniflar;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SiteMvc.App_Classes
{
    public class AllActionRun : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string dil = "tr-TR";
            if (HttpContext.Current.Session["Dil"] == null)
                HttpContext.Current.Session["Dil"] = dil;
            else
                dil = HttpContext.Current.Session["Dil"].ToString();

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(dil);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(dil);
            HttpCookie cookie = new HttpCookie("language");
            cookie.Value = dil;
            HttpContext.Current.Response.Cookies.Add(cookie);

            site_islem.ZiyaretciSayisiArttir(HttpContext.Current.Request.UserHostAddress);

            filterContext.Controller.ViewBag.EgitimTurleri = site_islem.EgitimTurleriGetir();
            filterContext.Controller.ViewBag.Ulkeler = site_islem.UlkeleriGetir(HttpContext.Current.Session["Dil"].ToString());
            filterContext.Controller.ViewBag.SosyalMedyalar = site_islem.SosyalMedyalar();
            if (HttpContext.Current.Session["popup"] == null)
            {
                filterContext.Controller.ViewBag.Popup = site_islem.PopupGetir();
                if (filterContext.Controller.ViewBag.Popup != null)
                    HttpContext.Current.Session["popup"] = "1";
            }
            else
                HttpContext.Current.Session["popup"] = "0";
                       
            if (filterContext.Controller.ToString() == "SiteMvc.Controllers.OkulSonuclariController")
                if (filterContext.ActionDescriptor.ActionName == "OkulDetay")
                    filterContext.Controller.ViewBag.SikayetNedenleri = site_islem.YorumSikayetNedenleri(HttpContext.Current.Session["Dil"].ToString());
            if (HttpContext.Current.Session["giris_yapan"] != null)
            {
                filterContext.Controller.ViewBag.giris_yapan = site_islem.UyeGetir(HttpContext.Current.Session["giris_yapan"].ToString());
                if (filterContext.Controller.ViewBag.giris_yapan == null)
                {
                    HttpContext.Current.Session["giris_yapan"] = null;
                    FormsAuthentication.SignOut();
                }
                if (filterContext.Controller.ToString() == "SiteMvc.Controllers.UyeController")
                    filterContext.Controller.ViewBag.Seviyeler = site_islem.SeviyeleriGetir();
            }
            else
                FormsAuthentication.SignOut();

            switch (HttpContext.Current.Session["Dil"].ToString())
            {
                case "tr-TR": filterContext.Controller.ViewBag.dil_kisa_kod = "tr"; break;
                case "en-US": filterContext.Controller.ViewBag.dil_kisa_kod = "gb"; break;
            }

        }
    }
}