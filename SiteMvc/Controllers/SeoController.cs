using dll.Models;
using EduApi.Siniflar;
using EduApi.SiteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SiteMvc.Controllers
{
    public class SeoController : Controller
    {
        // GET: Seo
        public ActionResult Index()
        {
            string filePath = Server.MapPath("~") + "SiteMap.xml";
            XmlTextWriter xr = new XmlTextWriter(filePath, Encoding.UTF8);
            try
            {
                xr.WriteStartDocument();
                xr.WriteStartElement("urlset");
                xr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                xr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                xr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");
                /* sitemap dosyamızın olmazsa olmazını ekledik. Şeması bu dedik buraya kadar.  */
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "https://eduadvisor.co.uk/");
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("changefreq", "daily");
                xr.WriteElementString("priority", "1");
                xr.WriteEndElement();
                List<sayfalar> sayfalar = site_islem.SayfalariGetir();
                List<egitim_turleri> turler = site_islem.EgitimTurleriGetir();
                for (int i = 0; i < sayfalar.Count; i++)
                {
                    xr.WriteStartElement("url");
                    xr.WriteElementString("loc", "https://eduadvisor.co.uk/" + sayfalar[i].seo_url);
                    xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                    xr.WriteElementString("priority", "0.5");
                    xr.WriteElementString("changefreq", "monthly");
                    xr.WriteEndElement();
                }
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "https://eduadvisor.co.uk/Sorulan-Sorular");
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "0.5");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "https://eduadvisor.co.uk/Site-Haritasi");
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "0.5");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
                OkulSonuclariFiltreleModel kriterler = new OkulSonuclariFiltreleModel();
                kriterler.ulke_id = "-1";
                kriterler.sehir_id = "-1";
                kriterler.puan_turleri = "";
                kriterler.aranacak_kelime = "";
                kriterler.sirala = 0;
                for (int i = 0; i < turler.Count; i++)
                {
                    xr.WriteStartElement("url");
                    xr.WriteElementString("loc", "https://eduadvisor.co.uk/Okul/" + turler[i].seo_url);
                    xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                    xr.WriteElementString("priority", "0.5");
                    xr.WriteElementString("changefreq", "daily");
                    xr.WriteEndElement();
                    kriterler.egitim_id = turler[i].id.ToString();
                    var gruplar = site_islem.OkulListeleSonuclari(kriterler, "tr-TR");
                    for (int j = 0; j < gruplar.Count; j++)
                    {
                        xr.WriteStartElement("url");
                        xr.WriteElementString("loc", "https://eduadvisor.co.uk/Genel-Detay/" + gruplar[j].seo_url);
                        xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                        xr.WriteElementString("priority", "1");
                        xr.WriteElementString("changefreq", "daily");
                        xr.WriteEndElement();
                        SiteGrupDetayModel detay = site_islem.GrupDetayGetir(gruplar[j].seo_url, HttpContext.Session["Dil"].ToString());
                        if (detay.okullar != null)
                            for (int z = 0; z < detay.okullar.Count; z++)
                            {
                                xr.WriteStartElement("url");
                                xr.WriteElementString("loc", "https://eduadvisor.co.uk/Okul-Detay/" + gruplar[j].seo_url + "/" + detay.okullar[z].seo_url + "-" + detay.okullar[z].id);
                                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                                xr.WriteElementString("priority", "1");
                                xr.WriteElementString("changefreq", "always");
                                xr.WriteEndElement();
                            }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                xr.WriteEndDocument();
                xr.Flush();
                xr.Close();
            }
            return RedirectToRoute("Home");
        }
    }
}