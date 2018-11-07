using dll.App_Classes;
using dll.Models;
using EduApi.SiteModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace EduApi.Siniflar
{
    public class site_islem
    {
        public static DllContext ctx = new DllContext();
        public static AnasayfaEnlerModel AnasayfaOkullar(string dil)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            string sql =
                "select * from (SELECT *,ROW_NUMBER() OVER (PARTITION BY tur ORDER BY deger DESC) AS 'rn' " +
                "FROM (select tur,case when tur<1 then sum(deger) else avg(deger) end as 'deger',okul_adi,grup_seo,yer from ((select  -1 as 'tur',0.0+(select count(*) from yorum y where y.okul_id=o.id and y.onay=1 and y.uye_sildi=0) as 'deger', " +
                "case when(o.merkez_id<=0) then (select top 1 og.adi from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.id) else (select top 1 og.adi from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.merkez_id) end as 'okul_adi'," +
                "case when(o.merkez_id<=0) then (select top 1 og.seo_url from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.id) else (select top 1 og.seo_url from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.merkez_id) end as 'grup_seo'," +
                "case when(o.merkez_id<=0) then (select concat(i.adi,concat('/',u." + (dil == "tr-TR" ? "adi" : "adi_ing") + ")) from il i inner join ulke u on i.ulke_id=u.id where i.id=o.sehir_id) else (select concat(i.adi,concat('/',u." + (dil == "tr-TR" ? "adi" : "adi_ing") + ")) from il i inner join ulke u on i.ulke_id=u.id where i.id=(select oo.sehir_id from okullar oo where oo.id=o.merkez_id)) end as 'yer' " +
                "from okullar o where o.yayinda>0 and (select count(*) from yorum y where y.okul_id=o.id and y.onay=1 and y.uye_sildi=0)>0 and o.arsivde=0 and " +
                "((o.merkez_id<=0 and (select count(*) from okul_grup_iliski oi where oi.okul_id=o.id)>0) or (o.merkez_id>0 and (select count(*) from okul_grup_iliski oi where oi.okul_id=o.merkez_id)>0)) and " +
                "(o.egitim_id>1 or (o.egitim_id=1 and (select count(*) from okul_fakulteleri ff where ff.okul_id=o.id)>0))) " +
                "union all " +
                "(select -2 as 'tur',(case when(select count(*) from okul_sayac os where os.okul_id=o.id)>0 then (select sayac from okul_sayac os where os.okul_id=o.id) else 0 end)+(select count(*) from okul_gunluk og where og.okul_id=o.id) as 'deger'," +
                "case when(o.merkez_id<=0) then (select top 1 og.adi from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.id) else (select top 1 og.adi from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.merkez_id) end as 'okul_adi'," +
                "case when(o.merkez_id<=0) then (select top 1 og.seo_url from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.id) else (select top 1 og.seo_url from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.merkez_id) end as 'grup_seo'," +
                "case when(o.merkez_id<=0) then (select concat(i.adi,concat('/',u." + (dil == "tr-TR" ? "adi" : "adi_ing") + ")) from il i inner join ulke u on i.ulke_id=u.id where i.id=o.sehir_id) else (select concat(i.adi,concat('/',u." + (dil == "tr-TR" ? "adi" : "adi_ing") + ")) from il i inner join ulke u on i.ulke_id=u.id where i.id=(select oo.sehir_id from okullar oo where oo.id=o.merkez_id)) end as 'yer' " +
                "from okullar o where o.yayinda>0 and o.arsivde=0 and " +
                "((o.merkez_id<=0 and (select count(*) from okul_grup_iliski oi where oi.okul_id=o.id)>0) or (o.merkez_id>0 and (select count(*) from okul_grup_iliski oi where oi.okul_id=o.merkez_id)>0)) and " +
                "(o.egitim_id>1 or (o.egitim_id=1 and (select count(*) from okul_fakulteleri ff where ff.okul_id=o.id)>0))) " +
                "union all " +
                "(select -3 as 'tur',(select count(*) from yorum y where y.onay=1 and y.uye_sildi=0) as 'deger',concat(i.adi,concat('/',u." + (dil == "tr-TR" ? "adi" : "adi_ing") + ")) as 'okul_adi','' as 'grup_seo','' as 'yer' " +
                "from okullar o inner join il i on i.id=o.sehir_id inner join ulke u on i.ulke_id=u.id where o.yayinda>0 and (select count(*) from yorum y where y.onay=1 and y.uye_sildi=0)>0 and o.arsivde=0)" +
                "union all " +
                "(select o.egitim_id as 'tur', case " +
                "when(select count(*) from yorum y where y.okul_id = o.id and y.onay = 1 and y.uye_sildi=0) > 1 then(select ROUND(SUM(puani) / count(*), 1) from yorum y where y.onay = 1 and y.okul_id = o.id and y.uye_sildi=0) " +
                "when(select count(*) from yorum y where y.okul_id = o.id and y.onay = 1 and y.uye_sildi=0) > 0 then(select top 1 y.puani from yorum y where y.okul_id = o.id and y.onay = 1 and y.uye_sildi=0) else 0 end as 'deger'," +
                "case when(o.merkez_id<=0) then (select top 1 og.adi from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.id) else (select top 1 og.adi from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.merkez_id) end as 'okul_adi'," +
                "case when(o.merkez_id<=0) then (select top 1 og.seo_url from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.id) else (select top 1 og.seo_url from okul_grup_iliski oi inner join okul_gruplari og on oi.grup_id=og.id where oi.okul_id=o.merkez_id) end as 'grup_seo'," +
                "case when(o.merkez_id<=0) then (select concat(i.adi,concat('/',u." + (dil == "tr-TR" ? "adi" : "adi_ing") + ")) from il i inner join ulke u on i.ulke_id=u.id where i.id=o.sehir_id) else (select concat(i.adi,concat('/',u." + (dil == "tr-TR" ? "adi" : "adi_ing") + ")) from il i inner join ulke u on i.ulke_id=u.id where i.id=(select oo.sehir_id from okullar oo where oo.id=o.merkez_id)) end as 'yer' " +
                "from okullar o where o.yayinda>0 and o.arsivde=0 and " +
                "((o.merkez_id<=0 and (select count(*) from okul_grup_iliski oi where oi.okul_id=o.id)>0) or (o.merkez_id>0 and (select count(*) from okul_grup_iliski oi where oi.okul_id=o.merkez_id)>0)) and " +
                "((o.egitim_id>1 and o.egitim_id<5) or (o.egitim_id=1 and (select count(*) from okul_fakulteleri ff where ff.okul_id=o.id)>0)))) as tmp group by tur,okul_adi,grup_seo,yer) as tmp2) as tmp3 WHERE rn <5 ";
            var lst = baglanti.Database.SqlQuery<AnasayfaEnlerOkulModel>(sql).ToList();
            List<AnasayfaEnlerOkulModel> lstencokyorumalan = lst.Where(x => x.tur == -1).ToList();
            List<AnasayfaEnlerOkulModel> lstencokgoruntulenen = lst.Where(x => x.tur == -2).ToList();
            List<AnasayfaEnlerOkulModel> lstpopulersehir = lst.Where(x => x.tur == -3).ToList();
            List<AnasayfaEnlerOkulModel> lstenbegenilenuni = lst.Where(x => x.tur == 1).ToList();
            List<AnasayfaEnlerOkulModel> lstenbegenilendil = lst.Where(x => x.tur == 2).ToList();
            List<AnasayfaEnlerOkulModel> lstenbegenilenkolej = lst.Where(x => x.tur == 3).ToList();
            List<AnasayfaEnlerOkulModel> lstenbegenilenlise = lst.Where(x => x.tur == 4).ToList();
            AnasayfaEnlerModel donecek = new AnasayfaEnlerModel()
            {
                EnBegenilenDilOkullari = lstenbegenilendil,
                EnBegenilenKolejler = lstenbegenilenkolej,
                EnBegenilenLiseler = lstenbegenilenlise,
                EnBegenilenUniversiteler = lstenbegenilenuni,
                EnPopulerSehirler = lstpopulersehir,
                goruntulenenler = lstencokgoruntulenen,
                yorum_alanlar = lstencokyorumalan
            };
            return donecek;
        }
        public static List<sayfalar> SayfalariGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.sayfalars.AsNoTracking().Where(x => x.id > 1).ToList();
        }
        public static void AnketGonder(List<ValueTextModel> data, int y_id)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                yorum yapilan = baglanti.yorums.AsNoTracking().Where(x => x.id == y_id).FirstOrDefault();
                anket_cevaplayan yeni = new anket_cevaplayan()
                {
                    anket_id = baglanti.Anketlers.AsNoTracking().Where(x => x.egitim_id == yapilan.okullar.egitim_id && x.aktif > 0).Select(x => x.id).FirstOrDefault(),
                    okul_id = yapilan.okul_id,
                    tarih = DateTime.Now,
                    uye_id = yapilan.uye_id
                };
                List<anket_cevaplari> yeni_cevaplar = new List<anket_cevaplari>();
                for (int i = 0; i < data.Count; i++)
                {
                    yeni_cevaplar.Add(new anket_cevaplari() { soru_id = Convert.ToInt32(data[i].value), degeri = Convert.ToInt32(data[i].text) });
                }
                yeni.anket_cevaplari = yeni_cevaplar;
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                baglanti.anket_cevaplayan.Add(yeni);
                baglanti.SaveChanges();
            }
            catch (Exception)
            {
            }
        }
        public static Anketler AnketGetir(int y_id)
        {
            Anketler anket = new Anketler();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                yorum yapilan = baglanti.yorums.AsNoTracking().Where(x => x.id == y_id).FirstOrDefault();
                Anketler gecerli = baglanti.Anketlers.AsNoTracking().Where(x => x.egitim_id == yapilan.okullar.egitim_id && x.aktif > 0).FirstOrDefault();
                if (gecerli.anket_cevaplayan.Where(x => x.okul_id == yapilan.okul_id && x.uye_id == yapilan.uye_id).ToList().Count == 0)
                    anket = gecerli;
            }
            catch (Exception)
            {
            }
            return anket;
        }
        public static popup PopupGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.popups.AsNoTracking().Where(x => x.aktif == true).FirstOrDefault();
        }
        public static AnasayfaYorumModel IlkYorumGetir(string dil)
        {
            AnasayfaYorumModel donecek = new AnasayfaYorumModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                genel_ayarlar ayar = baglanti.genel_ayarlar.AsNoTracking().FirstOrDefault();
                if (ayar.ilk_yorum_tur == 0)
                {
                    donecek = baglanti.yorum_begeniler.AsNoTracking().Where(x => x.yorum.onay == 1 && x.yorum.okullar.yayinda == 1 && x.yorum.okullar.arsivde == false && x.yorum.uye_sildi == false).Select(b => b.yorum).GroupBy(x => x).Select(b => new
                    {
                        begeni_sayisi = b.Count(),
                        yorum = b.Key
                    }).OrderByDescending(x => x.begeni_sayisi).Select(b => new AnasayfaYorumModel
                    {
                        adi = b.yorum.uyeler.adi + " " + b.yorum.uyeler.soyadi,
                        grup_seo = b.yorum.okullar.merkez_id < 1 ? b.yorum.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.seo_url : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.yorum.okullar.merkez_id).FirstOrDefault().okul_gruplari.seo_url,
                        image_url = b.yorum.uyeler.fotograf,
                        okul_adi = (b.yorum.okullar.merkez_id < 1 ? b.yorum.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.yorum.okullar.merkez_id).FirstOrDefault().okul_gruplari.adi) + " - " +
                        (b.yorum.okullar.egitim_id == 1 ? (dil == "tr-TR" ? b.yorum.okullar.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi : b.yorum.okullar.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi_ingilizce) + " (" + b.yorum.okullar.adi + ")"
                        : b.yorum.okullar.adi),
                        okul_seo = b.yorum.okullar.seo_url,
                        seviyesi = baglanti.uye_seviyeleri.Where(x => x.puani <= b.yorum.uyeler.puani).OrderByDescending(x => x.puani).FirstOrDefault().adi,
                        yorum_baslik = b.yorum.baslik,
                        yorum_icerik = b.yorum.icerik,
                        yorum_puani = b.yorum.puani,
                        yorum_tarih = b.yorum.tarih,
                        okul_id = b.yorum.okullar.id.ToString()
                    }).FirstOrDefault();
                }
                else if (ayar.ilk_yorum_tur == 1)
                {
                    donecek = baglanti.yorums.Where(x => x.onay == 1 && x.okullar.yayinda == 1 && x.okullar.arsivde == false && x.uye_sildi == false).OrderByDescending(x => x.tarih).Select(b => new AnasayfaYorumModel
                    {
                        adi = b.uyeler.adi + " " + b.uyeler.soyadi,
                        grup_seo = b.okullar.merkez_id < 1 ? b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.seo_url : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.okullar.merkez_id).FirstOrDefault().okul_gruplari.seo_url,
                        image_url = b.uyeler.fotograf,
                        okul_adi = (b.okullar.merkez_id < 1 ? b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.okullar.merkez_id).FirstOrDefault().okul_gruplari.adi) + " - " +
                        (b.okullar.egitim_id == 1 ? (dil == "tr-TR" ? b.okullar.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi : b.okullar.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi_ingilizce) + " (" + b.okullar.adi + ")"
                        : b.okullar.adi),
                        okul_seo = b.okullar.seo_url,
                        seviyesi = baglanti.uye_seviyeleri.Where(x => x.puani <= b.uyeler.puani).OrderByDescending(x => x.puani).FirstOrDefault().adi,
                        yorum_baslik = b.baslik,
                        yorum_icerik = b.icerik,
                        yorum_puani = b.puani,
                        yorum_tarih = b.tarih,
                        okul_id = b.okullar.id.ToString()
                    }).FirstOrDefault();
                }
                else
                {
                    donecek = baglanti.yorums.Where(x => x.onay == 1 && x.okullar.yayinda == 1 && x.id == ayar.ilk_yorum_id && x.okullar.arsivde == false && x.uye_sildi == false).Select(b => new AnasayfaYorumModel
                    {
                        adi = b.uyeler.adi + " " + b.uyeler.soyadi,
                        grup_seo = b.okullar.merkez_id < 1 ? b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.seo_url : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.okullar.merkez_id).FirstOrDefault().okul_gruplari.seo_url,
                        image_url = b.uyeler.fotograf,
                        okul_adi = (b.okullar.merkez_id < 1 ? b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.okullar.merkez_id).FirstOrDefault().okul_gruplari.adi) + " - " +
                        (b.okullar.egitim_id == 1 ? (dil == "tr-TR" ? b.okullar.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi : b.okullar.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi_ingilizce) + " (" + b.okullar.adi + ")"
                        : b.okullar.adi),
                        okul_seo = b.okullar.seo_url,
                        seviyesi = baglanti.uye_seviyeleri.Where(x => x.puani <= b.uyeler.puani).OrderByDescending(x => x.puani).FirstOrDefault().adi,
                        yorum_baslik = b.baslik,
                        yorum_icerik = b.icerik,
                        yorum_puani = b.puani,
                        yorum_tarih = b.tarih,
                        okul_id = b.okullar.id.ToString()
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return donecek;
        }
        public static List<string> YorumSikayetNedenleri(string dil)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.yorum_sikayet_nedenleri.AsNoTracking().OrderBy(x => x.sira_no).Select(b => (dil == "tr-TR" ? b.adi : b.adi_ingilizce)).ToList();
        }
        public static List<ValueTextModel> UlkeEyaletSehirGetir(string dil)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> ulkeler = baglanti.ulkes.AsNoTracking().Select(b => new ValueTextModel()
            {
                value = "0-" + b.id,
                text = (dil == "tr-TR" ? b.adi : b.adi_ing)
            }).ToList();
            ulkeler.AddRange(baglanti.ils.AsNoTracking().Select(b => new ValueTextModel()
            {
                value = b.ulke_id + "-" + b.id,
                text = b.eyalet_id == -1 ? b.adi + "," + (dil == "tr-TR" ? b.ulke.adi : b.ulke.adi_ing) : b.adi + "," + baglanti.eyalets.Where(x => x.id == b.eyalet_id).FirstOrDefault().adi + "," + (dil == "tr-TR" ? b.ulke.adi : b.ulke.adi_ing)
            }).ToList());
            return ulkeler;
        }
        public static List<uye_mesajlari> UyeMesajlariGetir(string mail)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            int uye = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).Select(x => x.id).FirstOrDefault();
            return baglanti.uye_mesajlari.AsNoTracking().Where(x => x.alici == uye || x.uye_id == uye).ToList();
        }
        public static UyeMesajlarModel MesajlarimSayfaVerileri(string mail)
        {
            UyeMesajlarModel model = new UyeMesajlarModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uyeler uye = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault();
                model.mesajlar = baglanti.uye_mesajlari.AsNoTracking().Where(x => x.alici == uye.id || x.uye_id == uye.id).ToList();
                model.kampanyalar = baglanti.toplu_mesaj.AsNoTracking().Where(x => x.bas_tarih <= DateTime.Now && x.silindi < 1 && x.indirim_uye.Where(i => i.uye_id == uye.id && i.durumu == 0).ToList().Count == 0 &&
                        (x.toplu_mesaj_kitle.Where(y => y.egitim_id == -1 || (y.egitim_id == 0 && uye.uye_ilgi.Count > 0) || (x.seviye_mi == 0 && baglanti.uye_ilgi.Where(d => d.uye_id == uye.id && d.egitim_id == y.egitim_id).ToList().Count > 0) ||
                         (x.seviye_mi == 1 && (baglanti.uye_seviyeleri.Where(g => g.id == y.egitim_id).FirstOrDefault().puani > uye.puani && uye.puani >=
                         (baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).ToList().Count > 0 ?
                          baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).OrderByDescending(g => g.puani).FirstOrDefault().puani : 0))
                        )).ToList().Count > 0) && x.okul_id > 0 && baglanti.okullars.Where(y => y.id == x.okul_id && y.yayinda > 0 && y.arsivde == false).ToList().Count > 0).OrderByDescending(x => x.bas_tarih).Select(c => new UyeKampanyalarModel
                        {
                            okullar = c.okul_id > 0 ? baglanti.okullars.Where(x => x.id == c.okul_id).FirstOrDefault() : null,
                            toplu_mesaj = c
                        }).ToList();
                model.bildirimler = baglanti.toplu_mesaj.AsNoTracking().Where(x => x.bas_tarih <= DateTime.Now && x.silindi < 1 && x.indirim_uye.Where(i => i.uye_id == uye.id && i.durumu == 0).ToList().Count == 0 &&
                      (x.toplu_mesaj_kitle.Where(y => y.egitim_id == -1 || (y.egitim_id == 0 && uye.uye_ilgi.Count > 0) || (x.seviye_mi == 0 && baglanti.uye_ilgi.Where(d => d.uye_id == uye.id && d.egitim_id == y.egitim_id).ToList().Count > 0) ||
                       (x.seviye_mi == 1 && (baglanti.uye_seviyeleri.Where(g => g.id == y.egitim_id).FirstOrDefault().puani > uye.puani && uye.puani >=
                       (baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).ToList().Count > 0 ?
                        baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).OrderByDescending(g => g.puani).FirstOrDefault().puani : 0))
                      )).ToList().Count > 0) && x.okul_id < 0).OrderByDescending(x => x.bas_tarih).ToList();
            }
            catch (Exception e)
            {
            }
            return model;
        }
        public static List<ValueTextModel> OkulOnerisiGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.okul_gruplari.AsNoTracking().Where(x => x.okul_grup_iliski.Count > 0).Select(b => new ValueTextModel { value = "", text = b.adi }).ToList();
        }
        public static string UyeYorumBegen(string mail, int id, int durum)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                if (durum == 1)
                {
                    yorum_begeniler begeni = baglanti.yorum_begeniler.AsNoTracking().Where(x => x.uyeler.mail == mail && x.yorum_id == id).FirstOrDefault();
                    baglanti.Entry(begeni).State = System.Data.Entity.EntityState.Deleted;
                    baglanti.yorum_begeniler.Remove(begeni);
                    baglanti.SaveChanges();
                }
                else
                {
                    yorum_begeniler begeni = new yorum_begeniler()
                    {
                        yorum_id = id,
                        uye_id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).Select(x => x.id).FirstOrDefault()
                    };
                    baglanti.Entry(begeni).State = System.Data.Entity.EntityState.Added;
                    baglanti.SaveChanges();
                    baglanti.Entry(begeni).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                }
                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
        }
        public static string UyeYorumSikayetEt(string mail, int id, string neden, string sifre)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                sifre = islem.sifrele(sifre);
                uyeler uye = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail && x.sifre == sifre).FirstOrDefault();
                if (uye != null)
                {
                    yorum_sikayetleri yeni = new yorum_sikayetleri()
                    {
                        yorum_id = id,
                        uye_id = uye.id,
                        tarih = DateTime.Now,
                        nedeni = neden
                    };
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                    baglanti.SaveChanges();
                    return "1";
                }
                return "-8";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static List<ValueTextModel> EyaletSehirleriGetir(int eyalet_id)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.ils.AsNoTracking().Where(x => x.eyalet_id == eyalet_id && x.eyalet_id > 0).Select(b => new ValueTextModel { value = b.id.ToString(), text = b.adi }).ToList();
        }
        public static List<ValueTextModel> EyaletGetir(int ulke_id)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.eyalets.AsNoTracking().Where(x => x.ulke_id == ulke_id).Select(b => new ValueTextModel { value = b.id.ToString(), text = b.adi }).ToList();
        }
        public static string UyeBildirimCevapla(string mail, int id, byte durum)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                indirim_uye ind = new indirim_uye()
                {
                    uye_id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault().id,
                    durumu = durum,
                    mesaj_id = id,
                    tarih = DateTime.Now
                };
                baglanti.Entry(ind).State = System.Data.Entity.EntityState.Added;
                baglanti.indirim_uye.Add(ind);
                baglanti.SaveChanges();
                baglanti.Entry(ind).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static void UyeMesajlarOkundu(string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                int id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).Select(x => x.id).FirstOrDefault();
                List<uye_mesajlari> mesajlar = baglanti.uye_mesajlari.AsNoTracking().Where(x => x.alici == id && x.okundu == 0 && x.turu == 0).ToList();
                for (int i = 0; i < mesajlar.Count; i++)
                {
                    mesajlar[i].okundu = 1;
                    baglanti.Entry(mesajlar[i]).State = System.Data.Entity.EntityState.Modified;
                }
                if (mesajlar.Count > 0)
                {
                    baglanti.SaveChanges();
                    for (int i = 0; i < mesajlar.Count; i++)
                    {
                        baglanti.Entry(mesajlar[i]).State = System.Data.Entity.EntityState.Detached;
                    }
                    baglanti.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
        public static string UyeMesajGonder(string mail, string mesaj)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uye_mesajlari yeni = new uye_mesajlari()
                {
                    alici = 0,
                    icerik = mesaj,
                    okundu = 0,
                    tarih = DateTime.Now,
                    turu = 0,
                    uye_id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).Select(x => x.id).FirstOrDefault()
                };
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                baglanti.uye_mesajlari.Add(yeni);
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string ProfilYorumResimSil(int id, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                yorum_resimleri y = baglanti.yorum_resimleri.AsNoTracking().Where(x => x.id == id && x.yorum.uyeler.mail == mail).FirstOrDefault();
                baglanti.Entry(y).State = System.Data.Entity.EntityState.Deleted;
                baglanti.yorum_resimleri.Remove(y);
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string UyeDogrula(string dog_kod)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uye_dogrulama gelen = baglanti.uye_dogrulama.AsNoTracking().Where(x => x.dogrulama_kodu == dog_kod && x.onay == 0).FirstOrDefault();
                if (gelen != null)
                {
                    gelen.dogrulama_kodu = "0";
                    gelen.onay = 1;
                    gelen.dogrulama_tarihi = DateTime.Now;
                    baglanti.Entry(gelen).State = System.Data.Entity.EntityState.Modified;
                    baglanti.SaveChanges();
                    baglanti.Entry(gelen).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                    return "1";
                }
                return "0";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string ProfilTekrarKodGonder(string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uye_dogrulama uye = baglanti.uye_dogrulama.AsNoTracking().Where(x => x.uyeler.mail == mail).FirstOrDefault();
                string kod = islem.DogrulamaKodUret();
                while (baglanti.uye_dogrulama.AsNoTracking().Where(x => x.dogrulama_kodu == kod).ToList().Count > 0)
                {
                    kod = islem.DogrulamaKodUret();
                }
                if (uye != null)
                {
                    uye.dogrulama_kodu = kod;
                    uye.onay = 0;
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Modified;
                    baglanti.SaveChanges();
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                }
                else
                {
                    uye_dogrulama yeni = new uye_dogrulama()
                    {
                        dogrulama_kodu = kod,
                        onay = 0,
                        uye_id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault().id
                    };
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                    baglanti.uye_dogrulama.Add(yeni);
                    baglanti.SaveChanges();
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                }
                try
                {
                    string MailBody = string.Empty;
                    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/TekrarKodGonder.html")))
                    {
                        MailBody = reader.ReadToEnd();
                    }
                    MailBody = MailBody.Replace("{Mail}", mail);
                    MailBody = MailBody.Replace("{dog_kod}", kod);
                    islem.MailGonder(mail, MailBody, "EduAdvisor Doğrulama Kodunuz");
                }
                catch (Exception)
                {
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string ProfilUyeDogrula(string dogrulama_kodu, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uye_dogrulama gelen = baglanti.uye_dogrulama.AsNoTracking().Where(x => x.dogrulama_kodu == dogrulama_kodu && x.onay == 0 && x.uyeler.mail == mail).FirstOrDefault();
                if (gelen != null)
                {
                    gelen.onay = 1;
                    gelen.dogrulama_tarihi = DateTime.Now;
                    gelen.dogrulama_kodu = "0";
                    baglanti.Entry(gelen).State = System.Data.Entity.EntityState.Modified;
                    baglanti.SaveChanges();
                    baglanti.Entry(gelen).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                    return "1";
                }
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string ProfilYorumDetayGuncelle(ProfilYorumGuncelleModel yorum, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                yorum guncel = baglanti.yorums.AsNoTracking().Where(x => x.id == yorum.id && x.uyeler.mail == mail && x.uye_sildi == false).FirstOrDefault();
                if (guncel == null)
                    return "1";
                if (yorum.yorum_resim1 != "-1" || yorum.yorum_resim2 != "-1" || yorum.yorum_resim3 != "-1")
                {
                    List<yorum_resimleri> resim = new List<yorum_resimleri>();
                    string fn = "";
                    if (yorum.yorum_resim1 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(yorum.yorum_resim1);
                        if (fn != "")
                        {
                            resim.Add(new yorum_resimleri { resim_adi = fn, yorum_id = guncel.id });
                        }
                    }
                    if (yorum.yorum_resim2 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(yorum.yorum_resim2);
                        if (fn != "")
                        {
                            resim.Add(new yorum_resimleri { resim_adi = fn, yorum_id = guncel.id });
                        }
                    }
                    if (yorum.yorum_resim3 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(yorum.yorum_resim3);
                        if (fn != "")
                        {
                            resim.Add(new yorum_resimleri { resim_adi = fn, yorum_id = guncel.id });
                        }
                    }
                    for (int i = 0; i < resim.Count; i++)
                        baglanti.Entry(resim[i]).State = System.Data.Entity.EntityState.Added;
                    baglanti.SaveChanges();
                    for (int i = 0; i < resim.Count; i++)
                        baglanti.Entry(resim[i]).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();

                }
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static yorum UyeProfilYorumGetir(int id, string mail)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.yorums.AsNoTracking().Where(x => x.id == id && x.uyeler.mail == mail && x.uye_sildi == false).FirstOrDefault();
        }
        public static List<yorum> UyeProfilYorumlariGetir(string mail)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.yorums.AsNoTracking().Where(x => x.uyeler.mail == mail && x.uye_sildi == false).OrderByDescending(x => x.tarih).ToList();
        }
        public static string UyeProfilYorumSil(int id, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                yorum y = baglanti.yorums.AsNoTracking().Where(x => x.id == id && x.uyeler.mail == mail).FirstOrDefault();
                y.uye_sildi = true;
                y.uye_silme_tarihi = DateTime.Now;
                baglanti.Entry(y).State = System.Data.Entity.EntityState.Modified;
                baglanti.SaveChanges();
                baglanti.Entry(y).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static List<sosyal_medya> SosyalMedyalar()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.sosyal_medya.AsNoTracking().OrderBy(x => x.sira_no).ToList();
        }
        public static string UyeProfilDondur(string mevcut, string sebep, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                mevcut = islem.sifrele(mevcut);
                uyeler tmp = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail && x.sifre == mevcut).FirstOrDefault();
                if (tmp == null)
                    return "-9";
                tmp.hesap_dondur = sebep;
                tmp.hesap_aktiflik = 0;
                tmp.dondurma_tarihi = DateTime.Now;
                baglanti.Entry(tmp).State = System.Data.Entity.EntityState.Modified;
                baglanti.SaveChanges();
                baglanti.Entry(tmp).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string UyeProfilSifreGuncelle(string mevcut, string yeni, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                mevcut = islem.sifrele(mevcut);
                uyeler tmp = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail && x.sifre == mevcut).FirstOrDefault();
                if (tmp == null)
                    return "-9";
                tmp.sifre = islem.sifrele(yeni);
                baglanti.Entry(tmp).State = System.Data.Entity.EntityState.Modified;
                baglanti.SaveChanges();
                baglanti.Entry(tmp).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string UyeProfilEgitimSil(int id)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uye_okullari tmp = baglanti.uye_okullari.AsNoTracking().Where(x => x.id == id).FirstOrDefault();
                baglanti.Entry(tmp).State = System.Data.Entity.EntityState.Deleted;
                baglanti.uye_okullari.Remove(tmp);
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static List<uye_okullari> UyeOkullariGetir(string mail)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<uye_okullari> donecek = baglanti.uye_okullari.AsNoTracking().Where(x => x.uyeler.mail == mail).ToList();
            return donecek;
        }
        public static string UyeProfilEgitimEkle(UyeProfilEgitimModel data, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uye_okullari tmp = new uye_okullari()
                {
                    baslangic = data.baslangic,
                    bitis = data.hala_devam == "on" ? "-1" : data.bitis,
                    eklenme_tarihi = DateTime.Now,
                    okul_id = baglanti.okullars.AsNoTracking().Where(x => x.seo_url == data.kampus_id).FirstOrDefault().id,
                    program_id = Convert.ToInt32(data.program_id),
                    program_tur_id = Convert.ToInt32(data.program_tur_id),
                    uye_id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault().id,
                };
                baglanti.Entry(tmp).State = System.Data.Entity.EntityState.Added;
                baglanti.uye_okullari.Add(tmp);
                baglanti.SaveChanges();
                baglanti.Entry(tmp).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static List<ValueTextModel> OkulProgramlariGetir(string okul_seo)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> donecek = baglanti.okul_programlari.AsNoTracking().Where(x => x.okullar.seo_url == okul_seo).Select(b => new ValueTextModel { text = b.program_havuzu.adi, value = b.program_havuzu.id.ToString() }).ToList();
            return donecek;
        }
        public static List<ValueTextModel> EgitimTuruneGoreProgramTurleriGetir(string egitim_seo)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> donecek = baglanti.onkayit_egitim_turleri.AsNoTracking().Where(x => x.egitim_turleri.seo_url == egitim_seo && x.id > 0).Select(b => new ValueTextModel { text = b.adi, value = b.id.ToString() }).ToList();
            return donecek;
        }
        public static List<ValueTextModel> GrupOkullariGetir(int id)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> donecek = baglanti.okul_gruplari.AsNoTracking().Where(x => x.id == id).Select(b => new ValueTextModel { text = b.okul_grup_iliski.Select(c => b.adi + " - " + c.okullar.adi).FirstOrDefault(), value = b.okul_grup_iliski.Select(c => c.okullar.seo_url).FirstOrDefault() }).ToList();
            return donecek;
        }
        public static string UyeProfilResimGuncelle(string data, string mail)
        {
            try
            {
                string filename = ProfilResimEkle(data);
                if (!filename.Equals(""))
                {
                    eduadvisorContext baglanti = ctx.Baglanti;
                    uyeler uye = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault();
                    uye.fotograf = filename;
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Modified;
                    baglanti.SaveChanges();
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                    return filename;
                }
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static AnasayfaYakinOkulModel YakinOkullarGetir(string lat, string lng, string detayli_incele, string indirimlipng)
        {
            AnasayfaYakinOkulModel okullar = new AnasayfaYakinOkulModel();
            List<HaritaOkulModel> maps = new List<HaritaOkulModel>();
            maps.Add(new HaritaOkulModel { name = "Konumunuz", latitude = lat, longitude = lng, Key = "Konum", description = "", image_url = "", phone = "", Url = "" });
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                maps.AddRange(baglanti.okul_lokasyon.Where(x => x.okullar.yayinda > 0 && x.okullar.okul_grup_iliski.Count > 0 && x.okullar.arsivde == false).Select(b => new HaritaOkulModel
                {
                    image_url = "/Content/img/okul/" + b.okullar.logo,
                    name = b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi + " - " + b.okullar.adi,
                    latitude = b.lat.ToString(),
                    longitude = b.lng.ToString(),
                    okulseo = b.okullar.seo_url,
                    grupseo = b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.seo_url,
                    description = b.okullar.aciklama,
                    phone = b.okullar.tel1,
                    Key = b.okullar.kurumsal_yonetici_sube.Count > 0 ? (b.okullar.okul_indirimleri.ToList().Count == 0 ? b.okullar.egitim_turleri.harita_key_def : b.okullar.egitim_turleri.harita_key_ozel) : "uye_olmayan",
                    detayliincele = detayli_incele,
                    indirimliokulimg = b.okullar.okul_indirimleri.Count > 0 ? indirimlipng : "",
                    id = b.okul_id
                }).ToList());
                for (int i = 1; i < maps.Count; i++)
                {
                    maps[i].description = maps[i].description == null ? "" : maps[i].description.Length <= 130 ? maps[i].description : maps[i].description.Substring(0, 127) + "..";
                    maps[i].latitude = maps[i].latitude.Replace(',', '.');
                    maps[i].longitude = maps[i].longitude.Replace(',', '.');
                    maps[i].Url = "/Okul-Detay/" + maps[i].grupseo + "/" + maps[i].okulseo + "-" + maps[i].id;
                    maps[i].id = 0;
                }
                okullar.maps = maps;
                decimal lat1 = DecimalCevir(lat), lng1 = DecimalCevir(lng), fark = DecimalCevir("0.2");
                okullar.list = baglanti.okul_lokasyon.Where(x => x.okullar.yayinda > 0 && x.okullar.okul_grup_iliski.Count > 0 && x.okullar.arsivde == false &&
                  ((x.lat >= lat1 && x.lat <= (lat1 + fark)) || (x.lat <= lat1 && x.lat >= (lat1 - fark))) &&
                  ((x.lng >= lng1 && x.lng <= (lng1 + fark)) || (x.lng <= lng1 && x.lng >= (lng1 - fark)))).Select(b => new
                  {
                      lat_fark = ((b.lat - lat1) < 0 ? ((b.lat - lat1) * (-1)) : (b.lat - lat1)),
                      lng_fark = ((b.lng - lng1) < 0 ? ((b.lng - lng1) * (-1)) : (b.lng - lng1)),
                      okullar = b.okullar
                  }).OrderBy(x => x.lat_fark)
                     .Select(b => new HaritaOkulModel
                     {
                         image_url = "/Content/img/okul/" + b.okullar.logo,
                         name = b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi + " - " + b.okullar.adi,
                         okulseo = b.okullar.seo_url,
                         grupseo = b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.seo_url,
                         id = b.okullar.id
                     }).Take(4).ToList();
                for (int i = 0; i < okullar.list.Count; i++)
                {
                    okullar.list[i].Url = "/Okul-Detay/" + okullar.list[i].grupseo + "/" + okullar.list[i].okulseo + "-" + okullar.list[i].id;
                    okullar.list[i].id = 0;
                }
            }
            catch (Exception e)
            {
            }
            return okullar;
        }
        public static decimal DecimalCevir(string deger)
        {
            if (Convert.ToDecimal("12,14") < Convert.ToDecimal("12.14"))
                return Convert.ToDecimal(deger.Replace('.', ','));
            else
                return Convert.ToDecimal(deger.Replace(',', '.'));
        }
        private static string ProfilResimEkle(string resim)
        {
            var bytes = Convert.FromBase64String(resim.Substring(resim.IndexOf(',') + 1));
            string filename = "Eduadvisor-uye-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + resim.Substring(11, resim.IndexOf(";") - 11);
            try
            {
                System.Drawing.Image image;
                using (var ms = new MemoryStream(bytes, 0, bytes.Length))
                {
                    image = System.Drawing.Image.FromStream(ms, true);
                }
                System.Drawing.Bitmap bmpKucuk;
                if (image.Height > 250 && image.Width < 250)
                {
                    bmpKucuk = new System.Drawing.Bitmap(image, (image.Width * 250) / image.Height, 250);
                }
                else if (image.Width > 250 && image.Height < 250)
                {
                    bmpKucuk = new System.Drawing.Bitmap(image, 250, (image.Height * 250) / image.Width);
                }
                else if (image.Width > 250 && image.Height > 250)
                {
                    if (image.Width > image.Height)
                        bmpKucuk = new System.Drawing.Bitmap(image, 250, (image.Height * 250) / image.Width);
                    else
                        bmpKucuk = new System.Drawing.Bitmap(image, (image.Width * 250) / image.Height, 250);
                }
                else
                    bmpKucuk = new System.Drawing.Bitmap(image);
                bmpKucuk.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/kul_profil/" + filename);
            }
            catch (Exception)
            {
                filename = "";
            }
            return filename;
        }
        public static string UyeProfilKullaniciGuncelle(UyeProfilKullaniciModel data, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uyeler uye = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault();
                uye.adi = data.adi;
                uye.soyadi = data.soyadi;
                uye.biyografi = data.biyografi;
                uye.Cinsiyet = Convert.ToInt32(data.cinsiyet);
                uye.haber_bulten = Convert.ToByte(data.haber == "on");
                uye.tel_no = data.tel;
                uye.yas = data.yas;
                uye.uye_adres = data.adres;
                uye.tel_ulke_kodu = data.ulke_kodu;
                uye.tel_bolge_kodu = data.bolge_kodu;
                baglanti.Entry(uye).State = System.Data.Entity.EntityState.Modified;
                baglanti.SaveChanges();
                baglanti.Entry(uye).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                List<uye_ilgi> ilgi = baglanti.uye_ilgi.AsNoTracking().Where(x => x.uyeler.mail == mail).ToList();
                if (data.ilgiler == null)
                    data.ilgiler = new List<int>();
                for (int i = 0; i < ilgi.Count; i++)
                {
                    if (!data.ilgiler.Contains(ilgi[i].egitim_id))
                    {
                        baglanti.Entry(ilgi[i]).State = System.Data.Entity.EntityState.Deleted;
                        ilgi.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < data.ilgiler.Count; i++)
                {
                    if (ilgi.Where(x => x.egitim_id == data.ilgiler[i]).ToList().Count == 0)
                    {
                        uye_ilgi tmp = new uye_ilgi();
                        tmp.egitim_id = data.ilgiler[i];
                        tmp.uye_id = uye.id;
                        baglanti.Entry(tmp).State = System.Data.Entity.EntityState.Added;
                        ilgi.Add(tmp);
                    }
                }
                baglanti.SaveChanges();
                for (int i = 0; i < ilgi.Count; i++)
                    baglanti.Entry(ilgi[i]).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
        }
        public static void ZiyaretciSayisiArttir(string ip)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            try
            {
                DateTime simdi = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00");
                if (baglanti.okul_gunluk.AsNoTracking().Where(x => x.zaman < simdi).ToList().Count > 0)
                {
                    List<okul_gunluk> gunlukler = baglanti.okul_gunluk.AsNoTracking().Where(x => x.zaman < simdi).ToList();
                    List<ValueTextModel> gunluk = gunlukler.Select(b => b.okul_id).GroupBy(x => x).Select(b => new ValueTextModel { text = b.Key.ToString(), value = b.Count().ToString() }).ToList();
                    for (int i = 0; i < gunluk.Count; i++)
                    {
                        int id = Convert.ToInt32(gunluk[i].text);
                        okul_sayac guncel = baglanti.okul_sayac.AsNoTracking().Where(x => x.okul_id == id).FirstOrDefault();
                        if (guncel != null)
                        {
                            guncel.sayac += Convert.ToInt32(gunluk[i].value);
                            baglanti.Entry(guncel).State = System.Data.Entity.EntityState.Modified;
                            baglanti.SaveChanges();
                            baglanti.Entry(guncel).State = System.Data.Entity.EntityState.Detached;
                            baglanti.SaveChanges();
                        }
                        else
                        {
                            guncel = new okul_sayac();
                            guncel.okul_id = id;
                            guncel.sayac = Convert.ToInt32(gunluk[i].value);
                            baglanti.Entry(guncel).State = System.Data.Entity.EntityState.Added;
                            baglanti.okul_sayac.Add(guncel);
                            baglanti.SaveChanges();
                            baglanti.Entry(guncel).State = System.Data.Entity.EntityState.Detached;
                            baglanti.SaveChanges();
                        }
                    }
                    for (int i = 0; i < gunlukler.Count; i++)
                    {
                        baglanti.Entry(gunlukler[i]).State = System.Data.Entity.EntityState.Deleted;
                        baglanti.okul_gunluk.Remove(gunlukler[i]);
                    }
                    baglanti.SaveChanges();
                }
            }
            catch (Exception e)
            {
            }
            try
            {
                DateTime zaman = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00");
                if (baglanti.ziyaretcis.AsNoTracking().Where(x => x.ip == ip && x.zaman >= zaman).ToList().Count == 0)
                {
                    ziyaretci yeni = new ziyaretci();
                    yeni.ip = ip;
                    yeni.zaman = DateTime.Now;
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                    baglanti.ziyaretcis.Add(yeni);
                    baglanti.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
        public static string AramaBackGetir()
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                return baglanti.genel_ayarlar.AsNoTracking().Select(x => x.arama_back).FirstOrDefault();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static UyeSeviyeModel SeviyeleriGetir()
        {
            UyeSeviyeModel donecek = new UyeSeviyeModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                donecek.seviyeler = baglanti.uye_seviyeleri.AsNoTracking().ToList();
                donecek.bilgilendirmeler = baglanti.seviye_bilgilendirme.AsNoTracking().OrderBy(x => x.sira_no).ToList();
            }
            catch (Exception)
            {
            }
            return donecek;
        }
        public static uyeler UyeTamGetir(string mail)
        {
            uyeler donecek = new uyeler();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                donecek = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault();
            }
            catch (Exception)
            {
            }
            return donecek;
        }
        public static string OnKayitEkle(OnKayitEkleModel data, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                if (baglanti.on_kayit_basvuru.AsNoTracking().Where(x => x.basvurdugu_okul == data.okul_id && x.uyeler.mail == mail && x.program_id == data.program_id && x.durumu == 0).ToList().Count == 0)
                {
                    string sifre = islem.randomKodUret();
                    while (baglanti.on_kayit_basvuru.AsNoTracking().Where(x => x.on_kayitkodu == sifre).ToList().Count > 0)
                    {
                        sifre = islem.randomKodUret();
                    }
                    on_kayit_basvuru yeni = new on_kayit_basvuru()
                    {
                        adi = data.adi,
                        adres = data.acik_adres == null ? " " : data.acik_adres,
                        baslayacagi_tarih = data.baslama_tarih,
                        basvurdugu_okul = data.okul_id,
                        basvuru_tarihi = DateTime.Now,
                        cinsiyet = data.cinsiyet,
                        dil_seviye = data.dil_seviye.ToString(),
                        dogum_il = data.dogum_sehir,
                        dogum_ulke = data.dogum_ulke,
                        dog_tar = data.dogum_tarih,
                        durumu = 0,
                        email = data.mail,
                        kurs_hafta = data.kurs_hafta == 0 ? "" : data.kurs_hafta.ToString(),
                        on_kayitkodu = sifre,
                        pass_no = data.pass_no,
                        pass_tarih = data.pass_tarih,
                        program_id = data.program_id,
                        program_tur_id = data.program_tur_id,
                        soyadi = data.soyadi,
                        tel_cep = data.cep_no,
                        tel_ev = data.ev_no,
                        uye_id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault().id,
                        uyruk = data.milliyet,
                        yas_il = data.yasadigi_sehir,
                        yas_ulke = data.yasadigi_ulke
                    };
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                    baglanti.on_kayit_basvuru.Add(yeni);
                    baglanti.SaveChanges();
                    try
                    {
                        string MailBody = string.Empty;
                        using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/OnKayitYapildi.html")))
                        {
                            MailBody = reader.ReadToEnd();
                        }
                        MailBody = MailBody.Replace("{Sifre}", sifre);
                        islem.MailGonder(data.mail, MailBody, "Eduadvisor Ön Kayıt Kodunuz");
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {

                    }
                    catch (Exception)
                    {
                        List<on_basvuru_alici_listesi> alicilar = baglanti.on_basvuru_alici_listesi.AsNoTracking().ToList();
                        for (int i = 0; i < alicilar.Count; i++)
                        {
                            string MailBody = string.Empty;
                            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/EduOnKayitYapildi.html")))
                            {
                                MailBody = reader.ReadToEnd();
                            }
                            MailBody = MailBody.Replace("{Adi}", alicilar[i].adi);
                            MailBody = MailBody.Replace("{OkulAdi}", yeni.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi + " - " + yeni.okullar.adi);
                            MailBody = MailBody.Replace("{ProgramAdi}", yeni.program_havuzu.adi);
                            islem.MailGonder(alicilar[i].mail, MailBody, "Eduadvisor Sitesinden Başvurunuz Var!");
                        }
                    }
                    return "1";
                }
                return "-9";
            }
            catch (Exception e)
            {
                return "0";
            }
        }
        public static OnKayitModel OnKayitBilgileriGetir(string grupSeo, string okulSeo, string mail)
        {
            OnKayitModel donecek = new OnKayitModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                donecek = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).Select(b => new OnKayitModel
                {
                    Cinsiyet = b.Cinsiyet.ToString(),
                    Email = b.mail,
                    il_id = b.il_id,
                    Telefon = b.tel_no,
                    ulke_id = b.ulke_id,
                    Uye_Adi = b.adi,
                    Uye_Soyadi = b.soyadi,
                    programlar = baglanti.okul_grup_iliski.Where(x => x.okul_gruplari.seo_url == grupSeo && x.okullar.seo_url == okulSeo).
                    Select(d => d.okullar).FirstOrDefault().okul_programlari.Where(x => x.id > 0).
                    Select(d => new ValueTextModel { text = d.program_havuzu.adi, value = d.program_id.ToString() }).ToList(),
                    program_turleri = baglanti.okul_grup_iliski.Where(x => x.okul_gruplari.seo_url == grupSeo && x.okullar.seo_url == okulSeo).
                    Select(d => d.okullar).FirstOrDefault().egitim_turleri.onkayit_egitim_turleri.Where(x => x.id > 0).
                    Select(d => new ValueTextModel { text = d.adi, value = d.id.ToString() }).ToList(),
                    logo = baglanti.okul_grup_iliski.Where(x => x.okul_gruplari.seo_url == grupSeo && x.okullar.seo_url == okulSeo).
                    Select(d => d.okullar.logo).FirstOrDefault(),
                    okul_adi = baglanti.okul_grup_iliski.Where(x => x.okul_gruplari.seo_url == grupSeo && x.okullar.seo_url == okulSeo).
                    Select(d => d.okul_gruplari.adi + " - " + d.okullar.adi).FirstOrDefault(),
                    egitim_turu = baglanti.okul_grup_iliski.Where(x => x.okul_gruplari.seo_url == grupSeo && x.okullar.seo_url == okulSeo).
                    Select(d => d.okullar.egitim_turleri).FirstOrDefault(),
                    okul_id = baglanti.okul_grup_iliski.Where(x => x.okul_gruplari.seo_url == grupSeo && x.okullar.seo_url == okulSeo).
                    Select(d => d.okullar.id).FirstOrDefault(),
                    uye_adres = b.uye_adres
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
            }
            return donecek;
        }
        public static string BireyselKayitOl(string adi, string soyadi, string imageUrl, string mail, string sifre = "", bool haber = false, int tur = 2)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                if (baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).ToList().Count == 0)
                {
                    uyeler yeni = new uyeler()
                    {
                        adi = adi,
                        soyadi = soyadi,
                        fotograf = imageUrl,
                        mail = mail,
                        tarih = DateTime.Now,
                        haber_bulten = Convert.ToByte(haber),
                        kayit_tur = tur,
                        sifre = islem.sifrele(sifre),
                        hesap_aktiflik = 1,
                        il_id = -1,
                        ilce_id = -1,
                        ulke_id = -1,
                        yas = "0",
                        puani = 0
                    };
                    string kod = islem.DogrulamaKodUret();
                    while (baglanti.uye_dogrulama.AsNoTracking().Where(x => x.dogrulama_kodu == kod).ToList().Count > 0)
                    {
                        kod = islem.DogrulamaKodUret();
                    }
                    uye_dogrulama dog = new uye_dogrulama()
                    {
                        dogrulama_kodu = kod,
                        onay = 0
                    };
                    yeni.uye_dogrulama = dog;
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                    baglanti.uyelers.Add(yeni);
                    baglanti.SaveChanges();
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                    try
                    {
                        string MailBody = string.Empty;
                        using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/UyeKayitOl.html")))
                        {
                            MailBody = reader.ReadToEnd();
                        }
                        MailBody = MailBody.Replace("{Mail}", mail);
                        MailBody = MailBody.Replace("{Kod}", kod);
                        MailBody = MailBody.Replace("{Sifre}", sifre);
                        islem.MailGonder(mail, MailBody, "EduAdvisor Giriş Şifreniz");
                    }
                    catch (Exception)
                    {
                    }
                    return "1";
                }
                return "-9";
            }
            catch (Exception e)
            {
                return "0";
            }
        }
        public static bool BireyselKayitOlKontrol(string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                return baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).ToList().Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string YorumEkle(YorumModel data, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                yorum yeni = new yorum()
                {
                    baslik = data.yorum_baslik,
                    icerik = data.yorum_icerik,
                    okul_bas = data.yorum_bas,
                    okul_bit = data.yorum_bitis_chck == "on" ? "-1" : data.yorum_bitis,
                    okul_id = Convert.ToInt32(data.okul_id),
                    program_tur_id = Convert.ToInt32(data.program_tur_id == "0" ? "-1" : data.program_tur_id),
                    program_id = Convert.ToInt32(data.program_id == "0" ? "-1" : data.program_id),
                    puani = Convert.ToInt32(data.yorum_puan),
                    tarih = DateTime.Now,
                    uye_id = baglanti.uyelers.Where(x => x.mail == mail).FirstOrDefault().id,
                };
                if (data.yorum_resim1 != "-1" || data.yorum_resim2 != "-1" || data.yorum_resim3 != "-1")
                {
                    List<yorum_resimleri> resimler = new List<yorum_resimleri>();
                    string fn = "";
                    if (data.yorum_resim1 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(data.yorum_resim1);
                        if (fn != "")
                            resimler.Add(new yorum_resimleri { resim_adi = fn });
                    }
                    if (data.yorum_resim2 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(data.yorum_resim2);
                        if (fn != "")
                            resimler.Add(new yorum_resimleri { resim_adi = fn });
                    }
                    if (data.yorum_resim3 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(data.yorum_resim3);
                        if (fn != "")
                            resimler.Add(new yorum_resimleri { resim_adi = fn });
                    }
                    yeni.yorum_resimleri = resimler;
                }
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                baglanti.yorums.Add(yeni);
                baglanti.SaveChanges();
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return yeni.id.ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string SifreHatirlat(string mail)
        {
            try
            {
                string sifre = islem.randomSifreUret();
                eduadvisorContext baglanti = ctx.Baglanti;
                uyeler uye = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault();
                if (uye != null)
                {
                    uye.sifre = islem.sifrele(sifre);
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Modified;
                    uye.sifre = islem.sifrele(sifre);
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                    string MailBody = string.Empty;
                    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/SifreHatirlat.html")))
                    {
                        MailBody = reader.ReadToEnd();
                    }

                    MailBody = MailBody.Replace("{Sifre}", sifre);
                    islem.MailGonder(mail, MailBody, "EduAdvisor Şifre Sıfırlama");
                    return "1";
                }
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static bool KurumsalKayitOgrenciEkle(KurumsalKayitModel data, string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                kurumsal_kayit yeni = new kurumsal_kayit()
                {
                    okul_adi = data.kampussubeadi,
                    web_site = "",
                    ulke_adi = data.ulke_adi,
                    il_adi = data.sehir_adi,
                    yildiz_sayisi = Convert.ToInt32(data.yorum_puan),
                    yorum_baslik = data.yorum_baslik,
                    yorum_icerik = data.yorum_icerik,
                    baslangic = data.yorum_baslangic,
                    bitis = data.yorum_bitis_chck == "on" ? "-1" : data.yorum_bitis,
                    tarih = DateTime.Now,
                    uye_id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).Select(x => x.id).FirstOrDefault(),
                    egitim_id = baglanti.egitim_turleri.AsNoTracking().Where(x => x.seo_url == data.egitim_seo).Select(x => x.id).FirstOrDefault(),
                    grupadi = data.okul_adi,
                    fakulte_adi = data.fakulte_adi,
                    tur = 0,
                };
                if (data.yorum_resim_1 != "-1" || data.yorum_resim_2 != "-1" || data.yorum_resim_3 != "-1")
                {
                    List<kurumsal_kayit_resim> resimler = new List<kurumsal_kayit_resim>();
                    string fn = "";
                    if (data.yorum_resim_1 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(data.yorum_resim_1);
                        if (fn != "")
                            resimler.Add(new kurumsal_kayit_resim { resim_adi = fn });
                    }
                    if (data.yorum_resim_2 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(data.yorum_resim_2);
                        if (fn != "")
                            resimler.Add(new kurumsal_kayit_resim { resim_adi = fn });
                    }
                    if (data.yorum_resim_3 != "-1")
                    {
                        fn = KurumsalKayitResimEkle(data.yorum_resim_3);
                        if (fn != "")
                            resimler.Add(new kurumsal_kayit_resim { resim_adi = fn });
                    }
                    yeni.kurumsal_kayit_resim = resimler;
                }
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                baglanti.kurumsal_kayit.Add(yeni);
                baglanti.SaveChanges();
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                islem.MailGonder("webyazilim@dogruyer.com.tr", e.Message, "Kurumsal Kayıt Hataya Düştü");
                return false;
            }
        }
        private static string KurumsalKayitResimEkle(string yorum_resim)
        {
            var bytes = Convert.FromBase64String(yorum_resim.Substring(yorum_resim.IndexOf(',') + 1));
            Thread.Sleep(10);
            string filename = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yorum_resim.Substring(11, yorum_resim.IndexOf(";") - 11);
            try
            {
                System.Drawing.Image image;
                using (var ms = new MemoryStream(bytes, 0, bytes.Length))
                {
                    image = System.Drawing.Image.FromStream(ms, true);
                }
                System.Drawing.Bitmap bmpKucuk;
                if (image.Height > 250 && image.Width < 250)
                {
                    bmpKucuk = new System.Drawing.Bitmap(image, (image.Width * 250) / image.Height, 250);
                }
                else if (image.Width > 250 && image.Height < 250)
                {
                    bmpKucuk = new System.Drawing.Bitmap(image, 250, (image.Height * 250) / image.Width);
                }
                else if (image.Width > 250 && image.Height > 250)
                {
                    if (image.Width > image.Height)
                        bmpKucuk = new System.Drawing.Bitmap(image, 250, (image.Height * 250) / image.Width);
                    else
                        bmpKucuk = new System.Drawing.Bitmap(image, (image.Width * 250) / image.Height, 250);
                }
                else
                    bmpKucuk = new System.Drawing.Bitmap(image);
                bmpKucuk.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/yorum/" + filename);
            }
            catch (Exception)
            {
                filename = "";
            }
            return filename;
        }
        public static GirisYapanModel UyeGetir(string mail)
        {
            GirisYapanModel donecek = new GirisYapanModel();
            try
            {
                DateTime simdi = DateTime.Now;
                eduadvisorContext baglanti = ctx.Baglanti;

                donecek = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).Select(b => new GirisYapanModel
                {
                    adi = b.adi == "" ? b.mail.Substring(0, b.mail.IndexOf("@")) : b.adi + " " + b.soyadi,
                    fotograf = b.fotograf,
                    gelen_mesajlar = baglanti.uye_mesajlari.Where(x => x.okundu == 0 && x.alici == b.id && x.turu == 0).OrderByDescending(x => x.tarih).Select(c => c.icerik).Take(3).ToList(),
                    okunmayan_mesaj = baglanti.uye_mesajlari.Where(x => x.okundu == 0 && x.alici == b.id && x.turu == 0).ToList().Count,
                    okunmayan_bildirim = baglanti.toplu_mesaj.Where(x => x.bas_tarih <= DateTime.Now && x.silindi < 1 &&
                    (x.toplu_mesaj_kitle.Where(y => y.egitim_id == -1 || (y.egitim_id == 0 && b.uye_ilgi.Count > 0) || (x.seviye_mi == 0 && b.uye_ilgi.Where(d => d.egitim_id == y.egitim_id).ToList().Count > 0) ||
                     (x.seviye_mi == 1 && (baglanti.uye_seviyeleri.Where(g => g.id == y.egitim_id).FirstOrDefault().puani > b.puani && b.puani >=
                     (baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).ToList().Count > 0 ?
                      baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).OrderByDescending(g => g.puani).FirstOrDefault().puani : 0))
                    )).ToList().Count > 0) && (b.indirim_uye.Where(g => g.mesaj_id == x.id).ToList().Count == 0) && ((x.okul_id > 0 && baglanti.okullars.Where(y => y.id == x.okul_id && y.yayinda > 0 && y.arsivde == false).ToList().Count > 0) || x.okul_id < 0)).ToList().Count,
                    kampanya_bildirimler = baglanti.toplu_mesaj.Where(x => x.bas_tarih <= DateTime.Now && x.silindi < 1 &&
                      (x.toplu_mesaj_kitle.Where(y => y.egitim_id == -1 || (y.egitim_id == 0 && b.uye_ilgi.Count > 0) || (x.seviye_mi == 0 && b.uye_ilgi.Where(d => d.egitim_id == y.egitim_id).ToList().Count > 0) ||
                       (x.seviye_mi == 1 && (baglanti.uye_seviyeleri.Where(g => g.id == y.egitim_id).FirstOrDefault().puani > b.puani && b.puani >=
                       (baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).ToList().Count > 0 ?
                        baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).OrderByDescending(g => g.puani).FirstOrDefault().puani : 0))
                      )).ToList().Count > 0) && (b.indirim_uye.Where(g => g.mesaj_id == x.id).ToList().Count == 0) && x.okul_id > 0 && baglanti.okullars.Where(y => y.id == x.okul_id && y.yayinda > 0 && y.arsivde == false).ToList().Count > 0).OrderByDescending(x => x.bas_tarih).Take(4)
                      .Select(c => new ValueTextModel { text = c.baslik, value = c.id.ToString() })
                      .ToList(),
                    edu_bildirimler = baglanti.toplu_mesaj.Where(x => x.bas_tarih <= DateTime.Now && x.silindi < 1 &&
                    (x.toplu_mesaj_kitle.Where(y => y.egitim_id == -1 || (y.egitim_id == 0 && b.uye_ilgi.Count > 0) || (x.seviye_mi == 0 && b.uye_ilgi.Where(d => d.egitim_id == y.egitim_id).ToList().Count > 0) ||
                     (x.seviye_mi == 1 && (baglanti.uye_seviyeleri.Where(g => g.id == y.egitim_id).FirstOrDefault().puani > b.puani && b.puani >=
                     (baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).ToList().Count > 0 ?
                      baglanti.uye_seviyeleri.Where(g => g.id != y.egitim_id && g.puani < baglanti.uye_seviyeleri.Where(h => h.id == y.egitim_id).FirstOrDefault().puani).OrderByDescending(g => g.puani).FirstOrDefault().puani : 0))
                    )).ToList().Count > 0) && (b.indirim_uye.Where(g => g.mesaj_id == x.id && g.durumu == 0).ToList().Count == 0) && x.okul_id < 0).OrderByDescending(x => x.bas_tarih).Take(4)
                      .Select(c => new ValueTextModel { text = c.baslik, value = c.id.ToString() })
                      .ToList(),
                    dogrulandi = b.uye_dogrulama.onay == 1
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
            }
            return donecek;
        }
        public static bool GirisYap(string mail, string sifre)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                sifre = islem.sifrele(sifre);
                uyeler uye = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail && x.sifre == sifre).FirstOrDefault();
                if (uye != null)
                {
                    uye.hesap_aktiflik = 1;
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Modified;
                    baglanti.SaveChanges();
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<ValueTextModel> SorulanSorular()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> donecek = baglanti.sorulars.AsNoTracking().OrderBy(x => x.sira_no).Select(b => new ValueTextModel
            {
                value = b.soru,
                text = b.cevap
            }).ToList();
            return donecek;
        }
        public static List<ValueTextModel> SiteHaritasi(string dil)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> donecek = baglanti.site_haritasi.AsNoTracking().OrderBy(x => x.sira_no).Select(b => new ValueTextModel
            {
                value = b.link,
                text = dil == "tr-TR" ? b.adi : b.adi_ingilizce
            }).ToList();
            return donecek;
        }
        public static string SayfaGetir(string seo_url)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            string donecek = baglanti.sayfalars.AsNoTracking().Where(x => x.seo_url == seo_url).FirstOrDefault().turkce;
            return donecek;
        }
        public static string SoruSor(string mail, string soru)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                List<soru_alici_listesi> liste = baglanti.soru_alici_listesi.AsNoTracking().ToList();
                for (int i = 0; i < liste.Count; i++)
                {
                    string MailBody = string.Empty;
                    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/SoruSor.html")))
                    {
                        MailBody = reader.ReadToEnd();
                    }
                    MailBody = MailBody.Replace("{KisiAdi}", liste[i].adi);
                    MailBody = MailBody.Replace("{Email}", mail);
                    MailBody = MailBody.Replace("{Mesaj}", soru);
                    islem.MailGonder(liste[i].mail, MailBody, "Soru Alıcı Listesinden Sorunuz Var");
                }
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static List<AnasayfaYorumModel> AnasayfaYorumVeOkullar(string dil)
        {
            List<AnasayfaYorumModel> yorumlar = new List<AnasayfaYorumModel>();
            eduadvisorContext baglanti = ctx.Baglanti;
            yorumlar = baglanti.yorums.AsNoTracking().Where(x => x.onay == 1 && x.okullar.yayinda > 0 && x.okullar.arsivde == false && x.uye_sildi == false).Select(b => new AnasayfaYorumModel()
            {
                adi = b.uyeler.adi + " " + b.uyeler.soyadi,
                grup_seo = b.okullar.merkez_id < 1 ? b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.seo_url : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.okullar.merkez_id).FirstOrDefault().okul_gruplari.seo_url,
                image_url = b.uyeler.fotograf,
                okul_adi = (b.okullar.merkez_id < 1 ? b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.okullar.merkez_id).FirstOrDefault().okul_gruplari.adi) + " - " +
                        (b.okullar.egitim_id == 1 ? (dil == "tr-TR" ? b.okullar.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi : b.okullar.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi_ingilizce) + " (" + b.okullar.adi + ")"
                        : b.okullar.adi),
                okul_seo = b.okullar.seo_url,
                seviyesi = baglanti.uye_seviyeleri.Where(x => x.puani <= b.uyeler.puani).OrderByDescending(x => x.puani).FirstOrDefault().adi,
                yorum_baslik = b.baslik,
                yorum_icerik = b.icerik,
                yorum_puani = b.puani,
                yorum_tarih = b.tarih,
                okul_id = b.okullar.id.ToString()
            }).OrderByDescending(x => x.yorum_tarih).Take(6).ToList();
            return yorumlar;
        }
        public static bool KurumsalKayitYetkiliEkle(KurumsalKayitModel data)
        {
            try
            {
                if (data.yetkili_telefon == null)
                    data.yetkili_telefon = "";
                eduadvisorContext baglanti = ctx.Baglanti;
                kurumsal_kayit yeni = new kurumsal_kayit()
                {
                    aciklama = data.yetkili_mesaj,
                    adi = data.yetkili_adi,
                    egitim_id = baglanti.egitim_turleri.AsNoTracking().Where(x => x.seo_url == data.egitim_seo).FirstOrDefault().id,
                    email = data.yetkili_mail,
                    fakulte_adi = data.fakulte_adi,
                    grupadi = data.okul_adi,
                    il_adi = data.sehir_adi,
                    skype = data.yetkili_skype,
                    soyadi = data.yetkili_soyadi,
                    tarih = DateTime.Now,
                    telefon = data.yetkili_telefon,
                    tur = 1,
                    ulke_adi = data.ulke_adi,
                    okul_adi = data.kampussubeadi
                };
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                baglanti.kurumsal_kayit.Add(yeni);
                baglanti.SaveChanges();
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<ValueTextModel> EgitimTuruneGoreOkulGruplariGetir(string egitim_seo)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> donecek = baglanti.okul_gruplari.AsNoTracking().Where(x => x.egitim_turleri.seo_url == egitim_seo).Select(b => new ValueTextModel { text = b.adi, value = b.id.ToString() }).ToList();
            return donecek;
        }
        public static List<ValueTextModel> FakulteleriGetir(string dil)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.fakulte_havuzu.AsNoTracking().Select(b => new ValueTextModel { text = (dil == "tr-TR" ? b.adi : b.adi_ingilizce), value = b.id.ToString() }).ToList();
        }
        public static bool BizeUlasinGonder(BizeUlasinModel data, string ip)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                mesajlar yeni = new mesajlar()
                {
                    adi = data.adi,
                    konu = data.konu,
                    mail = data.email,
                    mesaj = data.mesaj,
                    tarih = DateTime.Now,
                    tel = data.telefon,
                    ip = ip
                };
                baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                baglanti.mesajlars.Add(yeni);
                baglanti.SaveChanges();
                List<mesaj_alici_listesi> alicilar = baglanti.mesaj_alici_listesi.AsNoTracking().ToList();
                for (int i = 0; i < alicilar.Count; i++)
                {
                    try
                    {
                        string MailBody = string.Empty;
                        using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/MesajGonder.html")))
                        {
                            MailBody = reader.ReadToEnd();
                        }
                        MailBody = MailBody.Replace("{KisiAdi}", alicilar[i].alici_adi);
                        MailBody = MailBody.Replace("{MailAdi}", data.adi);
                        MailBody = MailBody.Replace("{Telefon}", data.telefon);
                        MailBody = MailBody.Replace("{Konu}", data.konu);
                        MailBody = MailBody.Replace("{Email}", data.email);
                        MailBody = MailBody.Replace("{Mesaj}", data.mesaj);
                        islem.MailGonder(alicilar[i].alici_mail, MailBody, "EduAdvisor Web Sitesinden Mesajınız Var! ");
                    }
                    catch (Exception)
                    {
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string AboneOl(string mail)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                if (baglanti.abonelers.AsNoTracking().Where(x => x.mail == mail).ToList().Count == 0)
                {
                    aboneler yeni = new aboneler()
                    {
                        mail = mail
                    };
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                    baglanti.abonelers.Add(yeni);
                    baglanti.SaveChanges();
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                    return "1";
                }
                else
                    return "-1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static OkulDetayModel OkulDetayGetir(string grupSeo, string okulSeo, string mail = "-1", string ip = "", int id = 0)
        {
            OkulDetayModel donecek = new OkulDetayModel();
            eduadvisorContext baglanti = ctx.Baglanti;
            donecek = baglanti.okullars.AsNoTracking().Where(x => x.seo_url == okulSeo && x.id == id && ((x.merkez_id <= 0 && x.okul_grup_iliski.FirstOrDefault().okul_gruplari.seo_url == grupSeo) || (x.merkez_id > 0 && baglanti.okul_grup_iliski.Where(y => y.okul_id == x.merkez_id).FirstOrDefault().okul_gruplari.seo_url == grupSeo)) && x.yayinda > 0 && x.arsivde == false)
                .Select(b => new OkulDetayModel
                {
                    okul = b,
                    grupadi = b.merkez_id < 1 ? b.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi : baglanti.okul_grup_iliski.Where(x => x.okul_id == b.merkez_id).FirstOrDefault().okul_gruplari.adi
                })
                .FirstOrDefault();

            if (donecek != null && donecek.okul.id > 0)
            {
                if (!mail.Equals("-1"))
                {
                    try
                    {
                        if (baglanti.okul_ziyaret.AsNoTracking().Where(x => x.uyeler.mail == mail && x.okul_id == donecek.okul.id).ToList().Count == 0)
                        {
                            okul_ziyaret yeni = new okul_ziyaret();
                            yeni.uye_id = baglanti.uyelers.AsNoTracking().Where(x => x.mail == mail).FirstOrDefault().id;
                            yeni.okul_id = Convert.ToInt32(donecek.okul.id);
                            yeni.tarih = DateTime.Now;
                            baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                            baglanti.okul_ziyaret.Add(yeni);
                            baglanti.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                try
                {
                    DateTime simdi = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00");
                    if (baglanti.okul_gunluk.AsNoTracking().Where(x => x.ip == ip && x.zaman >= simdi).ToList().Count == 0)
                    {
                        string sql = "insert into okul_gunluk(ip,zaman,okul_id) values('" + ip + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'," + donecek.okul.id + ")";
                        int sonuc = baglanti.Database.ExecuteSqlCommand(sql);
                    }
                }
                catch (Exception e)
                {
                }
            }
            return donecek;
        }
        public static SiteGrupDetayModel GrupDetayGetir(string grupSeo, string dil)
        {
            SiteGrupDetayModel grp = new SiteGrupDetayModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                okul_gruplari gelen_grup = baglanti.okul_gruplari.AsNoTracking().Where(x => x.seo_url.Equals(grupSeo)).FirstOrDefault();
                int merkez_id = gelen_grup.okul_grup_iliski.FirstOrDefault().okul_id;
                List<GrupDetayOkulModel> okullar;
                if (gelen_grup.egitim_id != 1)
                {
                    okullar = baglanti.okullars.AsNoTracking().Where(x => (x.id == merkez_id || x.merkez_id == merkez_id) && x.yayinda > 0 && x.arsivde == false).Select(b => new GrupDetayOkulModel
                    {
                        okul_adi = b.adi,
                        ulke_adi = b.ulke_id > 0 ? baglanti.ulkes.Where(d => d.id == b.ulke_id).Select(d => (dil == "tr-TR" ? d.adi : d.adi_ing)).FirstOrDefault() : "",
                        sehir_adi = b.sehir_id > 0 ? baglanti.ils.Where(d => d.id == b.sehir_id).FirstOrDefault().adi : "",
                        seo_url = b.seo_url,
                        fakulte_adi = "",
                        id = b.id
                    }).OrderBy(x => x.okul_adi).ToList();
                }
                else
                {
                    okullar = baglanti.okullars.AsNoTracking().Where(x => (x.id == merkez_id || x.merkez_id == merkez_id) && x.yayinda > 0 && x.arsivde == false).Select(b => new GrupDetayOkulModel
                    {
                        okul_adi = b.adi,
                        fakulte_adi = (dil == "tr-TR" ? b.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi : b.okul_fakulteleri.FirstOrDefault().fakulte_havuzu.adi_ingilizce),
                        ulke_adi = b.ulke_id > 0 ? baglanti.ulkes.Where(d => d.id == b.ulke_id).Select(d => (dil == "tr-TR" ? d.adi : d.adi_ing)).FirstOrDefault() : "",
                        sehir_adi = b.sehir_id > 0 ? baglanti.ils.Where(d => d.id == b.sehir_id).FirstOrDefault().adi : "",
                        seo_url = b.seo_url,
                        id = b.id
                    }).OrderBy(x => x.fakulte_adi).ToList();
                }
                grp.adi = gelen_grup.adi;
                grp.seo_url = gelen_grup.seo_url;
                grp.logo = gelen_grup.okul_grup_iliski.FirstOrDefault().okullar.logo;
                grp.okullar = okullar;
                grp.egitim_id = gelen_grup.egitim_id;
                grp.puani = gelen_grup.okul_grup_iliski.Where(x => x.okullar.yayinda > 0 && x.okullar.arsivde == false).FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 1 ?
                    Math.Round(Convert.ToDouble(gelen_grup.okul_grup_iliski.Where(x => x.okullar.yayinda > 0 && x.okullar.arsivde == false).FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).Sum() / (float)gelen_grup.okul_grup_iliski.Where(x => x.okullar.yayinda > 0 && x.okullar.arsivde == false).FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count), 1).ToString()
                    : (gelen_grup.okul_grup_iliski.Where(x => x.okullar.yayinda > 0 && x.okullar.arsivde == false).FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 0 ? gelen_grup.okul_grup_iliski.Where(x => x.okullar.yayinda > 0 && x.okullar.arsivde == false).FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).FirstOrDefault().ToString() : "0");

            }
            catch (Exception)
            {
            }
            return grp;
        }
        public static KurumsalKayitProgramGetirModel KurumsalKayitProgramlariGetir(string egitim_seo)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> programlar = baglanti.program_havuzu.AsNoTracking().Where(x => x.egitim_turleri.seo_url == egitim_seo && x.id > 0).Select(b => new ValueTextModel { text = b.adi, value = b.id.ToString() }).ToList();
            List<ValueTextModel> turler = baglanti.onkayit_egitim_turleri.AsNoTracking().Where(x => x.egitim_turleri.seo_url == egitim_seo && x.id > 0).Select(b => new ValueTextModel { text = b.adi, value = b.id.ToString() }).ToList();
            KurumsalKayitProgramGetirModel donecek = new KurumsalKayitProgramGetirModel()
            {
                programlar = programlar,
                turler = turler
            };
            return donecek;
        }
        public static List<egitim_turleri> EgitimTurleriGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<egitim_turleri> donecek = baglanti.egitim_turleri.AsNoTracking().Where(x => x.silindi < 1).ToList();
            return donecek;
        }
        public static string SeoUrlEgitimIdGetir(string kat)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.egitim_turleri.AsNoTracking().Where(x => x.seo_url == kat).FirstOrDefault().id.ToString();
        }
        public static List<OkulSonuclariItemModel> OkulListeleSonuclari(OkulSonuclariFiltreleModel gelenler, string dil)
        {
            int ulke = Convert.ToInt32(gelenler.ulke_id);
            int sehir = Convert.ToInt32(gelenler.sehir_id);
            string[] puan_turleri = gelenler.puan_turleri.Split(',');
            List<Tuple<int, int>> pt = new List<Tuple<int, int>>();
            for (int i = 0; i < puan_turleri.Length; i++)
            {
                if (!puan_turleri[i].Equals(""))
                    pt.Add(new Tuple<int, int>(Convert.ToInt32(puan_turleri[i]), Convert.ToInt32(puan_turleri[i]) + 1));
            }
            eduadvisorContext baglanti = ctx.Baglanti;
            List<okul_grup_iliski> silinecekler = baglanti.okul_grup_iliski.AsNoTracking().Where(x => baglanti.okullars.Where(c => c.id == x.okul_id && c.merkez_id > 0).ToList().Count > 0).ToList();
            for (int i = 0; i < silinecekler.Count; i++)
            {
                baglanti.Entry(silinecekler[i]).State = System.Data.Entity.EntityState.Deleted;
                baglanti.okul_grup_iliski.Remove(silinecekler[i]);
            }
            baglanti.SaveChanges();
            List<OkulSonuclariItemModel> okullar;
            okullar = baglanti.okul_gruplari.AsNoTracking().Where(x => x.egitim_id.ToString() == gelenler.egitim_id &&
                  x.adi.ToLower().Replace("i", "").Replace("ı", "").Replace("ö", "o").Replace("ü", "u").Replace("ş", "s").Replace("ğ", "g").Replace("ç", "c").Contains(
                     (gelenler.aranacak_kelime == null || gelenler.aranacak_kelime.Equals("")) ? x.adi.ToLower().Replace("i", "").Replace("ı", "").Replace("ö", "o").Replace("ü", "u").Replace("ş", "s").Replace("ğ", "g").Replace("ç", "c") :
                     gelenler.aranacak_kelime.ToLower().Replace("i", "").Replace("ı", "").Replace("ö", "o").Replace("ü", "u").Replace("ş", "s").Replace("ğ", "g").Replace("ç", "c")) &&
                 baglanti.okullars.Where(y => y.yayinda > 0 && y.arsivde == false && y.ulke_id == (ulke < 1 ? y.ulke_id : ulke) && y.sehir_id == (sehir < 1 ? y.sehir_id : sehir) &&
                 ((y.merkez_id <= 0 && y.okul_grup_iliski.Where(c => c.grup_id == x.id).ToList().Count > 0) ||
                 (y.merkez_id > 0 && baglanti.okullars.Where(c => c.id == y.merkez_id).FirstOrDefault().okul_grup_iliski.Where(c => c.grup_id == x.id).ToList().Count > 0)))
                 .ToList().Count > 0
                 ).Select(b => new OkulSonuclariItemModel
                 {
                     okul_adi = b.adi,
                     yer = b.okul_grup_iliski.FirstOrDefault().okullar.ulke_id > 0 ? baglanti.ils.Where(c => c.id == b.okul_grup_iliski.FirstOrDefault().okullar.sehir_id).Select(d => (dil == "tr-TR" ? d.ulke.adi : d.ulke.adi_ing) + "-" + d.adi).FirstOrDefault() : "",
                     logo = b.okul_grup_iliski.FirstOrDefault().okullar.logo,
                     puani = b.okul_grup_iliski.FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 1 ?
                           (b.okul_grup_iliski.FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).Sum() / (decimal)b.okul_grup_iliski.FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count)
                            : (b.okul_grup_iliski.FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 0 ? b.okul_grup_iliski.FirstOrDefault().okullar.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).FirstOrDefault() : 0),
                     seo_url = b.seo_url
                 }).ToList();
            List<OkulSonuclariItemModel> tmp_okullar = new List<OkulSonuclariItemModel>();
            for (int i = 0; i < pt.Count; i++)
            {
                List<OkulSonuclariItemModel> tmp_okullar2 = okullar.Where(x => x.puani >= pt[i].Item1 && x.puani <= pt[i].Item2).ToList();
                for (int j = 0; j < tmp_okullar2.Count; j++)
                    tmp_okullar.Add(tmp_okullar2[j]);
            }
            if (pt.Count > 0)
                okullar = tmp_okullar;
            for (int i = 0; i < okullar.Count; i++)
                okullar[i].puani = Math.Round(okullar[i].puani, 1);
            if (gelenler.sirala == 0)
                return okullar.OrderByDescending(x => x.puani).ToList();
            else if (gelenler.sirala == 1)
                return okullar.OrderBy(x => x.puani).ToList();
            else if (gelenler.sirala == 2)
                return okullar.OrderBy(x => x.okul_adi).ToList();
            return okullar.OrderByDescending(x => x.okul_adi).ToList();
        }
        public static List<ValueTextModel> UlkeleriGetir(string dil)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> donecek = baglanti.ulkes.AsNoTracking().OrderBy(x => x.sira_no).Select(b => new ValueTextModel
            {
                text = dil == "tr-TR" ? b.adi : b.adi_ing,
                value = b.id.ToString()
            }).ToList();
            return donecek;
        }
        public static List<ValueTextModel> SehirleriGetir(int id)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<ValueTextModel> donecek = baglanti.ils.AsNoTracking().Where(x => x.ulke_id == id).OrderBy(x => x.sira_no).Select(b => new ValueTextModel
            {
                text = b.adi,
                value = b.id.ToString()
            }).ToList();
            return donecek;
        }
    }
}