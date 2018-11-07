using dll.App_Classes;
using dll.Models;
using EduApi.Models;
using EduApi.SiteModels;
using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using static PCLCrypto.WinRTCrypto;

namespace EduApi.Siniflar
{
    public class islem
    {
        public static byte[] Keys = new byte[16];


        public static byte[] keyMaterial = new byte[] { 48, 130, 2, 34, 48, 13, 6, 9, 59, 78, 159, 248, 187, 98, 17, 41 };
        public static DataTable Table;
        public static DllContext ctx = new DllContext();
        public static genel_ayarlar GenelAyarlarGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.genel_ayarlar.AsNoTracking().FirstOrDefault();
        }
        public static bool MailGonder(string mail, string icerik, string konu)
        {
            genel_ayarlar gn_ayarlar = GenelAyarlarGetir();
            try
            {
                MailMessage Mail = new MailMessage();
                Mail.From = new MailAddress(gn_ayarlar.smtp_mail, "Eduadvisor Yönetimi");
                Mail.To.Add(mail);
                Mail.Subject = konu;
                Mail.Body = icerik;
                Mail.IsBodyHtml = true;
                SmtpClient smpt = new SmtpClient();
                smpt.Credentials = new NetworkCredential(gn_ayarlar.smtp_user, gn_ayarlar.smtp_pass);
                smpt.Port = Convert.ToInt32(gn_ayarlar.smtp_port);
                smpt.Host = gn_ayarlar.smtp_host;
                smpt.Send(Mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string randomSifreUret()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string sifre = "";
            var random = new Random();
            for (int i = 0; sifre.Length < 8; i++)
            {
                sifre += chars[random.Next(chars.Length)].ToString();
            }
            return sifre;
        }
        public static string randomKodUret()
        {
            string chars = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            string sifre = "";
            var random = new Random();
            for (int i = 0; sifre.Length < 10; i++)
            {
                sifre += chars[random.Next(chars.Length)].ToString();
            }
            return sifre;
        }
        public static string topla()
        {
            string virgullu = "";
            int milsaniye = DateTime.Now.Millisecond;
            while (milsaniye < 11)
            {
                milsaniye = DateTime.Now.Millisecond;
            }
            for (int i = 0; i < Keys.Length; i++)
            {
                virgullu += (Convert.ToInt32(Keys[i]) * milsaniye).ToString() + "Gm+D/f";
            }
            virgullu += milsaniye;
            return virgullu;
        }
        public static void keyayikla(string gelenkey)
        {
            gelenkey = gelenkey.Substring(gelenkey.IndexOf("Gk02Lm") + 6);
            string[] bolen = { "GmDf" };
            string[] gelenler = gelenkey.Split(bolen, StringSplitOptions.None);
            int son = Convert.ToInt32(gelenler[16]);
            for (int i = 0; i < 16; i++)
            {
                Keys[i] = Convert.ToByte((Convert.ToInt32(gelenler[i]) / son));
            }
        }
        public static string Decrypt(string metin)
        {
            try
            {
                metin = metin.Replace("ujm0165", "/").Replace("xdr738", "+").Replace("Qgp79538", "=");
                var sym = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbc);
                ICryptographicKey Decryptkey = sym.CreateSymmetricKey(Keys);
                var metinByte = Convert.FromBase64String(metin);
                var yenimetinByte = CryptographicEngine.Decrypt(Decryptkey, metinByte);
                var yenimetin = Encoding.UTF8.GetString(yenimetinByte, 0, yenimetinByte.Length).TrimEnd();
                return yenimetin;
            }
            catch (Exception ex)
            {
                return metin;
            }
        }
        public static string Crypto(string acikmetin)
        {
            if (acikmetin == null)
                return "";
            try
            {
                int sayi = (((acikmetin.Length / 16) + 1) * 16) - acikmetin.Length;
                int cikacak = 0;
                for (int i = 0; i < acikmetin.Length; i++)
                {
                    if (acikmetin[i].ToString().Equals("Ğ", StringComparison.CurrentCultureIgnoreCase) || acikmetin[i].ToString().Equals("Ü", StringComparison.CurrentCultureIgnoreCase) ||
                        acikmetin[i].ToString().Equals("Ş", StringComparison.CurrentCultureIgnoreCase) || acikmetin[i].ToString().Equals("İ") || acikmetin[i].ToString().Equals("ı") ||
                        acikmetin[i].ToString().Equals("Ö", StringComparison.CurrentCultureIgnoreCase) || acikmetin[i].ToString().Equals("Ç", StringComparison.CurrentCultureIgnoreCase))
                        cikacak++;
                }
                if (sayi - cikacak >= 0)
                    sayi -= cikacak;
                else
                    sayi = sayi + 16 - cikacak;
                for (int i = 0; i < sayi; i++)
                {
                    acikmetin += " ";
                }
                var sym = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbc);
                ICryptographicKey Cryptokey = sym.CreateSymmetricKey(Keys);
                var acikmetinByte = Encoding.UTF8.GetBytes(acikmetin);
                var sifreliByte = CryptographicEngine.Encrypt(Cryptokey, acikmetinByte);
                var sifreliMetin = Convert.ToBase64String(sifreliByte);
                return sifreliMetin;
            }
            catch (Exception)
            {
                return acikmetin;
            }
        }
        public static void degerleriKaristir()
        {
            Random r = new Random();
            int count = 0;
            string alinanlar = "";
            while (count < 16)
            {
                int temp = r.Next(0, 16);
                while (alinanlar.IndexOf("," + temp + ",") != -1)
                {
                    temp = r.Next(0, 16);
                }
                Keys[count] = keyMaterial[temp];
                alinanlar += "," + temp + ",";
                count++;
            }
        }
        public static string sifrele(string girilen)
        {
            string sonuc = FormsAuthentication.HashPasswordForStoringInConfigFile(girilen, "MD5");
            return sonuc;
        }
        public static string DogrulamaKodUret()
        {
            string chars = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            string sifre = "";
            var random = new Random();
            for (int i = 0; sifre.Length < 6; i++)
            {
                sifre += chars[random.Next(chars.Length)].ToString();
            }
            return sifre;
        }
        public static UyeModel girisyap(string mail, string sifre)
        {
            UyeModel uye = new UyeModel();
            eduadvisorContext baglanti = ctx.Baglanti;
            uyeler u = baglanti.uyelers.AsNoTracking().Where(x => x.mail.Equals(mail) && x.sifre.Equals(sifre) && (x.silindi < 1) && (x.kara_liste < 1)).FirstOrDefault();
            if (u != null)
            {
                u.hesap_aktiflik = 1;
                baglanti.SaveChanges();
                uye.Adi = u.adi;
                uye.adres = u.uye_adres;
                uye.biyografi = u.biyografi;
                uye.Cinsiyet = u.Cinsiyet == null || u.Cinsiyet == 2 ? "Belirtmek İstemiyorum" : (u.Cinsiyet == 0 ? "Kadın" : "Erkek");
                uye.fotograf = u.fotograf;
                uye.Haber_Bulten = Convert.ToBoolean(u.haber_bulten) ? "1" : "0";
                uye.Id = u.id.ToString();
                uye.mail = u.mail;
                uye.puani = u.puani.ToString();
                uye.Soyadi = u.soyadi;
                uye.tel_no = u.tel_no;
                uye.Yas = u.yas;
                uye.giris = "1";
                uye.dog_kod = u.uye_dogrulama.dogrulama_kodu;
            }
            else
                uye.giris = "E-posta veya şifre hatalı!";
            return uye;
        }
        public static UyeModel FacebookGirisi(FacebookProfile bilgiler)
        {
            UyeModel uye = new UyeModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uyeler u = baglanti.uyelers.AsNoTracking().Where(x => x.mail.Equals(bilgiler.email)).FirstOrDefault();
                if (u == null)
                {
                    string sifre = randomSifreUret();
                    string resimadi = "Eduadvisor-uye-" + DateTime.Now.ToString("yyyyMMddHmmss");
                    try
                    {
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData(string.Format("http://graph.facebook.com/{0}/picture", bilgiler.Id));
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
                    u = new uyeler
                    {
                        sifre = sifrele(sifre),
                        adi = bilgiler.FirstName,
                        Cinsiyet = 2,
                        haber_bulten = Convert.ToByte(true),
                        hesap_aktiflik = 1,
                        kara_liste = Convert.ToByte(false),
                        kayit_tur = 0,
                        soyadi = bilgiler.LastName,
                        fotograf = resimadi,
                        tarih = DateTime.Now,
                        biyografi = "",
                        ilce_id = 0,
                        il_id = 0,
                        ulke_id = 0,
                        silindi = 0,
                        mail = bilgiler.email,
                        puani = 0,
                        uye_adres = "",
                        yas = "0"
                    };
                    baglanti.uyelers.Add(u);
                    baglanti.SaveChanges();
                    string kod = DogrulamaKodUret();
                    while (baglanti.uye_dogrulama.AsNoTracking().Where(x => x.dogrulama_kodu.Equals(kod)).ToList().Count > 0)
                    {
                        kod = DogrulamaKodUret();
                    }
                    baglanti.uye_dogrulama.Add(new uye_dogrulama { uye_id = u.id, dogrulama_kodu = kod });
                    baglanti.SaveChanges();
                    string MailBody = string.Empty;
                    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/UyeKayitOl.html")))
                    {
                        MailBody = reader.ReadToEnd();
                    }
                    MailBody = MailBody.Replace("{Mail}", u.mail);
                    MailBody = MailBody.Replace("{Kod}", kod);
                    MailBody = MailBody.Replace("{Sifre}", sifre);
                    MailGonder(u.mail, MailBody, "EduAdvisor Giriş Şifreniz");
                }
                else
                {
                    u.hesap_aktiflik = 1;
                    baglanti.SaveChanges();
                }
                uye.Adi = u.adi;
                uye.adres = u.uye_adres;
                uye.biyografi = u.biyografi;
                uye.Cinsiyet = u.Cinsiyet == null || u.Cinsiyet == 2 ? "Belirtmek İstemiyorum" : (u.Cinsiyet == 0 ? "Kadın" : "Erkek");
                uye.fotograf = u.fotograf;
                uye.Haber_Bulten = u.haber_bulten == null ? "0" : (u.haber_bulten == 1 ? "1" : "0");
                uye.Id = u.id.ToString();
                uye.mail = u.mail;
                uye.puani = u.puani.ToString();
                uye.Soyadi = u.soyadi;
                uye.tel_no = u.tel_no;
                uye.Yas = u.yas;
                uye.giris = "1";
                uye.dog_kod = u.uye_dogrulama.dogrulama_kodu;
            }
            catch (Exception)
            {
                uye.giris = "Beklenmedik Bir Hata Oluştu! Tekrar Deneyiniz.";
            }
            return uye;
        }
        public static List<TextValueModel> DigerEgitimTurleriGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            string sql = "select adi as 'Text',cast(id as nvarchar(5)) as 'Value' from egitim_turleri e where id>4 and silindi<1";
            return baglanti.Database.SqlQuery<TextValueModel>(sql).ToList();
        }
        public static OkulListeleSonucModel OkulListeleSonuclari(OkulListeleSearchModel gelenler, string dil)
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
            string sql = "delete from okul_grup_iliski where (select COUNT(*) from okullar o where o.id=okul_id and o.merkez_id>0)>0;" +
                        "select * from (select row_number() over(order by " + (gelenler.sirala == 0 ? "puani desc" : (gelenler.sirala == 1 ? "puani" : (gelenler.sirala == 2 ? "okul_adi" : "okul_adi desc"))) + ") as 'satir',* from (select og.adi as 'okul_adi'," +
                        "(select top 1 u.adi+'-'+i.adi from okul_grup_iliski ogi inner join okullar o on ogi.okul_id=o.id inner join il i on o.sehir_id=i.id inner join ulke u on i.ulke_id=u.id " +
                        "where ogi.grup_id=og.id and o.merkez_id<=0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end) as 'yer'," +
                        "'https://eduadvisor.co.uk/Content/img/okul/'+(select top 1 o.logo from okul_grup_iliski ogi inner join okullar o on ogi.okul_id=o.id " +
                        "where ogi.grup_id=og.id and o.merkez_id<=0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=o.sehir_id) as 'logo'," +
                        "og.seo_url," +
                        "case when (select COUNT(*) from okul_grup_iliski ogi inner join okullar o on ogi.okul_id=o.id inner join yorum y on o.id=y.okul_id " +
                        "where ogi.grup_id=og.id and o.merkez_id<=0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end and y.uye_sildi=0 and y.onay=1)>1 then " +
                        "(select cast(ROUND(SUM(y.puani)/COUNT(*),1) as nvarchar(10)) from okul_grup_iliski ogi inner join okullar o on ogi.okul_id=o.id inner join yorum y on o.id=y.okul_id " +
                        "where ogi.grup_id=og.id and o.merkez_id<=0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end and y.uye_sildi=0 and y.onay=1 group by y.okul_id) " +
                        "when (select COUNT(*) from okul_grup_iliski ogi inner join okullar o on ogi.okul_id=o.id inner join yorum y on o.id=y.okul_id " +
                        "where ogi.grup_id=og.id and o.merkez_id<=0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end and y.uye_sildi=0 and y.onay=1)>1 then " +
                        "(select cast(SUM(y.puani) as nvarchar(10)) from okul_grup_iliski ogi inner join okullar o on ogi.okul_id=o.id inner join yorum y on o.id=y.okul_id " +
                        "where ogi.grup_id=og.id and o.merkez_id<=0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end and y.uye_sildi=0 and y.onay=1  group by y.okul_id) " +
                        "else '0' end as 'puani' " +
                        "from okul_gruplari og where og.egitim_id=" + gelenler.egitim_id + " and " +
                        "((select COUNT(*) from okullar o inner join okul_grup_iliski ogi on o.id=ogi.okul_id " +
                        "where ogi.grup_id=og.id and o.merkez_id<=0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end)>0 " +
                        "or " +
                        "(select COUNT(*) from okullar o inner join okul_grup_iliski ogi on o.merkez_id=ogi.okul_id " +
                        "where ogi.grup_id=og.id and o.merkez_id>0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end)>0) " +
                        "and " +
                        "REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(lower(og.adi),'i',''),'ı',''),'ö',''),'ü',''),'ş',''),'ğ',''),'ç','') " +
                        "LIKE case when(" + (string.IsNullOrEmpty(gelenler.aranacak_kelime) ? "''" : "'" + gelenler.aranacak_kelime + "'") + "='') then '%'+REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(lower(og.adi),'i',''),'ı',''),'ö',''),'ü',''),'ş',''),'ğ',''),'ç','')+'%' " +
                        "else '%'+REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(lower(" + (string.IsNullOrEmpty(gelenler.aranacak_kelime) ? "''" : "'" + gelenler.aranacak_kelime + "'") + "),'i',''),'ı',''),'ö',''),'ü',''),'ş',''),'ğ',''),'ç','')+'%' end" +
                        ") as tbl) as tbl2 where satir between " + (gelenler.page * 10 + 1) + " and " + (gelenler.page * 10 + 10);
            List<OkulListeleItemModel> okullar = baglanti.Database.SqlQuery<OkulListeleItemModel>(sql).ToList();
            OkulListeleSonucModel donecek = new OkulListeleSonucModel();
            donecek.okullar = okullar;
            sql = "select CAST(COUNT(*) as nvarchar(10)) from okul_gruplari og where og.egitim_id=" + gelenler.egitim_id + " and " +
                        "((select COUNT(*) from okullar o inner join okul_grup_iliski ogi on o.id=ogi.okul_id " +
                        "where ogi.grup_id=og.id and o.merkez_id<=0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end)>0 " +
                        "or " +
                        "(select COUNT(*) from okullar o inner join okul_grup_iliski ogi on o.merkez_id=ogi.okul_id " +
                        "where ogi.grup_id=og.id and o.merkez_id>0 and o.yayinda>0 and o.arsivde=0 and o.ulke_id=case when(" + gelenler.ulke_id + ">0) then " + gelenler.ulke_id + " else o.ulke_id end and o.sehir_id=case when(" + gelenler.sehir_id + ">0) then " + gelenler.sehir_id + " else o.sehir_id end)>0) " +
                        "and " +
                        "REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(lower(og.adi),'i',''),'ı',''),'ö',''),'ü',''),'ş',''),'ğ',''),'ç','') " +
                        "LIKE case when(" + (string.IsNullOrEmpty(gelenler.aranacak_kelime) ? "''" : "'" + gelenler.aranacak_kelime + "'") + "='') then '%'+REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(lower(og.adi),'i',''),'ı',''),'ö',''),'ü',''),'ş',''),'ğ',''),'ç','')+'%' " +
                        "else '%'+REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(lower(" + (string.IsNullOrEmpty(gelenler.aranacak_kelime) ? "''" : "'" + gelenler.aranacak_kelime + "'") + "),'i',''),'ı',''),'ö',''),'ü',''),'ş',''),'ğ',''),'ç','')+'%' end";
            donecek.TotalCount = baglanti.Database.SqlQuery<string>(sql).FirstOrDefault();
            //List<OkulListeleItemModel> tmp_okullar = new List<OkulListeleItemModel>();
            //for (int i = 0; i < pt.Count; i++)
            //{
            //    List<OkulListeleItemModel> tmp_okullar2 = okullar.Where(x => Convert.ToDecimal(x.puani) >= pt[i].Item1 && Convert.ToDecimal(x.puani) <= pt[i].Item2).ToList();
            //    for (int j = 0; j < tmp_okullar2.Count; j++)
            //        tmp_okullar.Add(tmp_okullar2[j]);
            //}
            //if (pt.Count > 0)
            //    okullar = tmp_okullar;
            return donecek;
        }
        public static decimal DecimalCevir(string deger)
        {
            bool virgul_mu = true;
            try
            {
                decimal tmp = Convert.ToDecimal("12,13");
                virgul_mu = tmp != 1213;
            }
            catch (Exception)
            {
                virgul_mu = false;
            }
            deger = virgul_mu ? deger.Replace('.', ',') : deger.Replace(',', '.');
            return Convert.ToDecimal(deger) / 1.000000000000000000000000000000000m;
        }
        public static ApiOkulDetayModel OkulDetayGetir(OkulDetayPostModel gelen)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            //            select
            //case when(o.merkez_id<=0) then (select og.adi from okul_grup_iliski ogi,okul_gruplari og where ogi.okul_id=o.id and og.id=ogi.grup_id) else           
            //(select og.adi from okul_grup_iliski ogi,okul_gruplari og where ogi.okul_id=o.merkez_id and og.id=ogi.grup_id) end as 'grup_adi',
            //case when (o.egitim_id=1 and fh.adi is not null) then fh.adi+ ' ('+o.adi+')' else o.adi end as 'okul_adi',
            //'https://eduadvisor.co.uk/Content/img/okul/'+o.logo,o.web_site as 'web_site_link',case when(oi.id is not null) then oi.orani else '' end as 'indirim_orani',
            //from okullar o left join okul_lokasyon ol on o.id=ol.okul_id left join okul_fakulteleri ff on ff.okul_id = o.id
            //left join fakulte_havuzu fh on  ff.fakulte_id = fh.id left join okul_indirimleri oi on o.id=oi.okul_id where o.yayinda=1 and o.arsivde=0 and
            //(select count(*) from okul_grup_iliski ogi where ogi.okul_id=o.merkez_id or ogi.okul_id=o.id)>0

            ApiOkulDetayModel sonuc = baglanti.okullars.AsNoTracking().Where(x => x.id.ToString() == gelen.okul_id && x.seo_url == gelen.seo_url && x.yayinda == 1 && x.arsivde == false).Select(b => new ApiOkulDetayModel
            {
                grup_adi = b.okul_grup_iliski.Count == 0 ? baglanti.okul_grup_iliski.Where(x => x.okul_id == b.merkez_id).FirstOrDefault().okul_gruplari.adi : b.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi,
                okul_adi = b.adi,
                logo = "https://eduadvisor.co.uk/Content/img/okul/" + b.logo,
                web_site_link = b.web_site,
                indirim_orani = b.okul_indirimleri.Count > 0 ? b.okul_indirimleri.FirstOrDefault().orani.ToString() : "",
                ozellikler = b.okul_ozellikleri.Select(c => c.egitim_ozellikleri.ozellikler.adi).ToList(),
                programlari = b.okul_programlari.Select(c => c.program_havuzu.adi).ToList(),
                resimler = b.okul_fotograflar.Select(c => new TextValueModel
                {
                    Value = "https://eduadvisor.co.uk/Content/img/okul/" + c.resim_adi,
                    Text = "Yönetim"
                }).ToList(),
                puani = b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 1 ? (b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).Sum() / (float)b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count).ToString()
                      : (b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 0 ? b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).FirstOrDefault().ToString() : "0"),
                yorumlar = b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).OrderByDescending(x => x.tarih).Select(c => new Models.YorumModel
                {
                    uye_adi_tarih = c.uyeler.adi + " " + c.uyeler.soyadi + " | " + c.tarih.Day + "." + c.tarih.Month + "." + c.tarih.Year,
                    puani_begeni_sayisi = "Puanı : " + c.puani + " | Beğeni :" + (c.yorum_begeniler.Count + c.yorum_begeni_okul.Count).ToString(),
                    programi = c.program_havuzu.adi + "-" + c.onkayit_egitim_turleri.adi,
                    resimler = c.yorum_resimleri.Select(d => d.resim_adi).ToList(),
                    uye_resim = "https://eduadvisor.co.uk/Content/img/kul_profil/" + c.uyeler.fotograf,
                    yorumu = c.icerik,
                    baslik = c.baslik,
                    yanitlandi = c.yorum_yanitlari.onay.ToString(),
                    yanit_icerik = c.yorum_yanitlari.yanit,
                    yanit_resim_url = "https://eduadvisor.co.uk/Content/img/okul/" + b.logo
                }).Take(5).ToList(),
                aciklama = b.aciklama,
                lat = b.okul_lokasyon != null ? b.okul_lokasyon.lat : -999,
                lng = b.okul_lokasyon != null ? b.okul_lokasyon.lng : -999
            }).FirstOrDefault();
            if (sonuc == null)
                return sonuc;
            sonuc.tam_adi = sonuc.grup_adi + "-" + sonuc.okul_adi;
            for (int i = 0; i < sonuc.yorumlar.Count; i++)
            {
                for (int j = 0; j < sonuc.yorumlar[i].resimler.Count; j++)
                {
                    sonuc.resimler.Add(new TextValueModel
                    {
                        Value = "https://eduadvisor.co.uk/Content/img/yorum/" + sonuc.yorumlar[i].resimler[j],
                        Text = sonuc.yorumlar[i].baslik
                    });
                }
                sonuc.yorumlar[i].resimler = null;
            }
            if (!gelen.uye_id.Equals("-1"))
            {
                try
                {
                    if (baglanti.okul_ziyaret.AsNoTracking().Where(x => x.uye_id.ToString() == gelen.uye_id && x.okul_id.ToString() == gelen.okul_id).ToList().Count == 0)
                    {
                        okul_ziyaret yeni = new okul_ziyaret();
                        yeni.uye_id = Convert.ToInt32(gelen.uye_id);
                        yeni.okul_id = Convert.ToInt32(gelen.okul_id);
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
            return sonuc;
        }
        public static AnasayfaModel AnasayfaOkullari()
        {
            AnasayfaModel donecek = new AnasayfaModel();
            eduadvisorContext baglanti = ctx.Baglanti;
            var lstencokyorumalan = baglanti.okullars.Where(x => x.yayinda > 0 && x.arsivde == false && x.yorums.Where(b => b.onay == 1 && b.uye_sildi == false).ToList().Count > 0).Select(b => new
            {
                yorum_sayisi = b.yorums.Where(x => x.onay == 1 && x.uye_sildi == false).ToList().Count,
                adi = (b.merkez_id <= 0 ? (b.okul_grup_iliski.Count > 0 ? b.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi + " - " : "") :
                     (baglanti.okul_grup_iliski.Where(c => c.okul_id == b.merkez_id).ToList().Count > 0 ? baglanti.okul_grup_iliski.Where(c => c.okul_id == b.merkez_id).FirstOrDefault().okul_gruplari.adi + "-" : "")) + b.adi,
                b.id,
                b.logo,
            }).OrderByDescending(x => x.yorum_sayisi).Take(4).ToList();
            List<SlaytOkulModel> lst_encokyorumalan = new List<SlaytOkulModel>();
            for (int i = 0; i < lstencokyorumalan.Count; i++)
            {
                lst_encokyorumalan.Add(new SlaytOkulModel
                {
                    id = lstencokyorumalan[i].id.ToString(),
                    okul_adi = lstencokyorumalan[i].adi + " (" + lstencokyorumalan[i].yorum_sayisi.ToString() + ")",
                    resim_adi = "https://eduadvisor.co.uk/Content/img/okul/" + lstencokyorumalan[i].logo,
                });
            }

            var lstencokgoruntulenen = baglanti.okullars.AsNoTracking().Where(x => x.yayinda > 0 && x.arsivde == false).Select(b => new
            {
                goruntuleme_sayisi = (b.okul_sayac != null ? b.okul_sayac.sayac : 0) + (b.okul_gunluk.ToList().Count),
                adi = (b.merkez_id <= 0 ? (b.okul_grup_iliski.Count > 0 ? b.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi + " - " : "") :
                    (baglanti.okul_grup_iliski.Where(c => c.okul_id == b.merkez_id).ToList().Count > 0 ? baglanti.okul_grup_iliski.Where(c => c.okul_id == b.merkez_id).FirstOrDefault().okul_gruplari.adi + "-" : "")) + b.adi,
                b.id,
                b.logo
            }).OrderByDescending(x => x.goruntuleme_sayisi).Take(4).ToList();
            List<SlaytOkulModel> lst_encokgoruntulenen = new List<SlaytOkulModel>();
            for (int i = 0; i < lstencokgoruntulenen.Count; i++)
            {
                lst_encokgoruntulenen.Add(new SlaytOkulModel
                {
                    id = lstencokgoruntulenen[i].id.ToString(),
                    okul_adi = lstencokgoruntulenen[i].adi,
                    resim_adi = "https://eduadvisor.co.uk/Content/img/okul/" + lstencokgoruntulenen[i].logo,
                });
            }

            var encokbegenilenler = baglanti.egitim_turleri.AsNoTracking().Where(x => x.id < 5 && x.okullars.Count > 0).Where(x => x.okullars.Where(y => y.yorums.Where(b => b.onay == 1 && b.uye_sildi == false).ToList().Count > 0 && y.yayinda > 0 && y.arsivde == false).ToList().Count > 0).Select(d => new
            {
                id = d.okullars.Where(x => x.yayinda > 0 && x.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 0).Select(b => new
                {
                    b.id,
                    ortpuan = b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 1 ? (int)(b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).Sum() / (float)b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count)
                   : (int)(b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 0 ? b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).FirstOrDefault() : 0)
                }).OrderByDescending(x => x.ortpuan).Take(1).FirstOrDefault().id,
                d.adi,
                logo = d.okullars.Where(x => x.yayinda > 0 && x.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 0).Select(b => new
                {
                    b.logo,
                    ortpuan = b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 1 ? (int)(b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).Sum() / (float)b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count)
                     : (b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 0 ? b.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).Select(c => c.puani).FirstOrDefault() : 0)
                }).OrderByDescending(x => x.ortpuan).Take(1).FirstOrDefault().logo

            }).Take(4).ToList();


            List<SlaytOkulModel> lst_enbegenilenler = new List<SlaytOkulModel>();
            for (int i = 0; i < encokbegenilenler.Count; i++)
            {
                lst_enbegenilenler.Add(new SlaytOkulModel
                {
                    id = encokbegenilenler[i].id.ToString(),
                    okul_adi = encokbegenilenler[i].adi,
                    resim_adi = "https://eduadvisor.co.uk/Content/img/okul/" + encokbegenilenler[i].logo,
                });
            }

            var lstpopulersehir = baglanti.okullars.Where(x => x.yayinda > 0 && x.ulke_id > 0 && x.yorums.Where(c => c.onay == 1 && c.uye_sildi == false).ToList().Count > 0 && x.arsivde == false).Select(b => new
            {
                b.sehir_id,
                yorum_sayisi = b.yorums.Where(x => x.onay == 1 && x.uye_sildi == false).ToList().Count
            }).GroupBy(f => f.sehir_id).
              Select(f => new { yer = baglanti.ils.Where(c => c.id == f.Key).Select(c => c.ulke.adi + " (" + c.adi + ")").FirstOrDefault(), sehir_id = f.Key, toplam = f.Sum(c => c.yorum_sayisi) })
              .OrderByDescending(d => d.toplam).Take(4).ToList();

            List<TextValueModel> lst_enpopuler = new List<TextValueModel>();
            for (int i = 0; i < lstpopulersehir.Count; i++)
            {
                lst_enpopuler.Add(new TextValueModel { Text = (lstpopulersehir[i].yer), Value = (lstpopulersehir[i].sehir_id.ToString()) });
            }
            donecek.encokyorumalanlar = lst_encokyorumalan;
            donecek.encokgoruntulenenler = lst_encokgoruntulenen;
            donecek.enbegenilenler = lst_enbegenilenler;
            donecek.enpopulersehirler = lst_enpopuler;
            return donecek;
        }
        public static List<UlkeModel> UlkeSehirGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<UlkeModel> ulkeler = baglanti.ulkes.AsNoTracking().Select(b => new UlkeModel
            {
                ulke = new TextValueModel { Text = b.adi, Value = b.id.ToString() },
                sehirler = b.ils.Select(c => new TextValueModel
                {
                    Text = c.adi,
                    Value = c.id.ToString()
                }).ToList()
            }).ToList();
            return ulkeler;
        }
        public static List<TextValueModel> FakulteleriGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<TextValueModel> fakulteler = baglanti.fakulte_havuzu.AsNoTracking().Select(b => new TextValueModel
            {
                Text = b.adi,
                Value = b.id.ToString()
            }).ToList();
            return fakulteler;
        }
        public static List<GruplarModel> GruplariGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<GruplarModel> gruplar = baglanti.okul_gruplari.AsNoTracking().Select(b => new GruplarModel
            {
                Text = b.adi,
                Value = b.id.ToString(),
                egitim_id = b.egitim_id.ToString()
            }).ToList();
            return gruplar;
        }
        public static List<GruplarModel> ProgramlariGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<GruplarModel> programlar = baglanti.program_havuzu.AsNoTracking().Select(b => new GruplarModel
            {
                Text = b.adi,
                Value = b.id.ToString(),
                egitim_id = b.egitim_id.ToString()
            }).ToList();
            return programlar;
        }
        public static List<GruplarModel> ProgramTurleriGetir()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<GruplarModel> turler = baglanti.onkayit_egitim_turleri.AsNoTracking().Select(b => new GruplarModel
            {
                Text = b.adi,
                Value = b.id.ToString(),
                egitim_id = b.egitim_id.ToString()
            }).ToList();
            return turler;
        }
        public static string OkulEkle(YeniOkulModel yeni)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                kurumsal_kayit yeni_k = new kurumsal_kayit()
                {
                    tur = Convert.ToInt32(yeni.ekleyen_tip),
                    okul_adi = yeni.okul_adi,
                    web_site = "",
                    egitim_id = Convert.ToInt32(yeni.egitim_id),
                    ulke_adi = yeni.ulke_adi,
                    il_adi =yeni.sehir_adi,
                    tarih = DateTime.Now,
                    email = yeni.yetkili.mail,
                    aciklama = yeni.yetkili.mesajiniz,
                    adi = yeni.yetkili.adi,
                    baslangic = yeni.ogrenci.egitim_baslangic,
                    bitis = yeni.ogrenci.egitim_bitis,
                    fakulte_adi = yeni.fakulte_adi,
                    grupadi = yeni.grup_adi,
                    skype = yeni.yetkili.skype_id,
                    soyadi = yeni.yetkili.soyadi,
                    telefon = yeni.yetkili.telefon,
                    yildiz_sayisi = yeni.ekleyen_tip.Equals("0") ? Convert.ToInt32(yeni.ogrenci.puani) : 0,
                    yorum_baslik = yeni.ogrenci.baslik,
                    yorum_icerik = yeni.ogrenci.yorumu,
                    uye_id = yeni.ekleyen_tip.Equals("1") ? -1 : Convert.ToInt32(yeni.ogrenci.uye_id)
                };
                List<kurumsal_kayit_resim> yorum_resimleri = new List<kurumsal_kayit_resim>();
                #region resimler
                string filename = "", filename1 = "", filename2 = "";
                if (!yeni.ogrenci.resim1.Equals(""))
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(yeni.ogrenci.resim1.Substring(yeni.ogrenci.resim1.IndexOf(',') + 1));
                        filename = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.ogrenci.resim1.Substring(11, yeni.ogrenci.resim1.IndexOf(";") - 11);
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
                        yorum_resimleri.Add(new kurumsal_kayit_resim { resim_adi = filename });
                    }
                    catch (Exception)
                    {
                    }
                }
                if (!yeni.ogrenci.resim2.Equals(""))
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(yeni.ogrenci.resim2.Substring(yeni.ogrenci.resim2.IndexOf(',') + 1));
                        filename1 = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.ogrenci.resim2.Substring(11, yeni.ogrenci.resim2.IndexOf(";") - 11);
                        while (filename.Equals(filename1))
                        {
                            filename1 = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.ogrenci.resim2.Substring(11, yeni.ogrenci.resim2.IndexOf(";") - 11);
                        }
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

                        bmpKucuk.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/yorum/" + filename1);
                        yorum_resimleri.Add(new kurumsal_kayit_resim { resim_adi = filename1 });
                    }
                    catch (Exception)
                    {
                    }
                }
                if (!yeni.ogrenci.resim3.Equals(""))
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(yeni.ogrenci.resim3.Substring(yeni.ogrenci.resim3.IndexOf(',') + 1));
                        filename2 = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.ogrenci.resim3.Substring(11, yeni.ogrenci.resim3.IndexOf(";") - 11);
                        while (filename2.Equals(filename1))
                        {
                            filename2 = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.ogrenci.resim2.Substring(11, yeni.ogrenci.resim2.IndexOf(";") - 11);
                        }
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

                        bmpKucuk.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/yorum/" + filename2);
                        yorum_resimleri.Add(new kurumsal_kayit_resim { resim_adi = filename2 });
                    }
                    catch (Exception)
                    {
                    }
                }
                #endregion               
                yeni_k.kurumsal_kayit_resim = yorum_resimleri;
                baglanti.Entry(yeni_k).State = System.Data.Entity.EntityState.Added;
                baglanti.kurumsal_kayit.Add(yeni_k);
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string UyeEkle(YeniUyeModel gelen)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            uyeler uye = baglanti.uyelers.Where(x => x.mail.Equals(gelen.mail)).FirstOrDefault();
            if (uye == null)
            {
                uye = new uyeler
                {
                    sifre = sifrele(gelen.sifre),
                    adi = gelen.mail.Split('@').First(),
                    Cinsiyet = 2,
                    haber_bulten = Convert.ToByte(gelen.haber.Equals("1")),
                    hesap_aktiflik = 1,
                    kara_liste = Convert.ToByte(false),
                    kayit_tur = 0,
                    soyadi = "",
                    fotograf = "profil.png",
                    tarih = DateTime.Now,
                    biyografi = "",
                    ilce_id = 0,
                    il_id = 0,
                    ulke_id = 0,
                    silindi = 0,
                    mail = gelen.mail,
                    puani = 0,
                    uye_adres = "",
                    yas = "0"
                };
                string kod = DogrulamaKodUret();
                while (baglanti.uye_dogrulama.Where(x => x.dogrulama_kodu.Equals(kod)).ToList().Count > 0)
                {
                    kod = DogrulamaKodUret();
                }
                uye_dogrulama uyd = new uye_dogrulama();
                uyd.dogrulama_kodu = kod;
                uye.uye_dogrulama = uyd;
                baglanti.Entry(uye).State = System.Data.Entity.EntityState.Added;
                baglanti.uyelers.Add(uye);
                baglanti.SaveChanges();
                string MailBody = string.Empty;
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/MailTemplate/UyeKayitOl.html")))
                {
                    MailBody = reader.ReadToEnd();
                }
                MailBody = MailBody.Replace("{Mail}", gelen.mail);
                MailBody = MailBody.Replace("{Kod}", kod);
                MailBody = MailBody.Replace("{Sifre}", gelen.sifre);
                MailGonder(gelen.mail, MailBody, "EduAdvisor Giriş Şifreniz");
                return "1";
            }
            else
                return "-9";
        }
        public static string YorumEkle(YeniOkulYorumModel yeni)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                yorum gelen = new yorum();
                gelen.baslik = yeni.baslik;
                gelen.okul_bas = yeni.egitim_baslangic;
                gelen.okul_bit = yeni.egitim_bitis;
                gelen.program_id = Convert.ToInt32((yeni.program_id));
                gelen.program_tur_id = Convert.ToInt32((yeni.program_turu));
                gelen.puani = Convert.ToInt32((yeni.puani));
                gelen.uye_id = Convert.ToInt32((yeni.uye_id));
                gelen.icerik = yeni.yorumu;
                gelen.okul_id = Convert.ToInt32((yeni.okul_id));
                #region resimler
                string filename = "", filename1 = "", filename2 = "";
                List<yorum_resimleri> resimler = new List<yorum_resimleri>();
                if (!yeni.resim1.Equals(""))
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(yeni.resim1.Substring(yeni.resim1.IndexOf(',') + 1));
                        filename = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.resim1.Substring(11, yeni.resim1.IndexOf(";") - 11);
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
                        resimler.Add(new yorum_resimleri { resim_adi = filename });
                    }
                    catch (Exception)
                    {
                    }
                }
                if (!yeni.resim2.Equals(""))
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(yeni.resim2.Substring(yeni.resim2.IndexOf(',') + 1));
                        filename1 = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.resim2.Substring(11, yeni.resim2.IndexOf(";") - 11);
                        while (filename.Equals(filename1))
                        {
                            filename1 = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.resim2.Substring(11, yeni.resim2.IndexOf(";") - 11);
                        }
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

                        bmpKucuk.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/yorum/" + filename1);
                        resimler.Add(new yorum_resimleri { resim_adi = filename1 });
                    }
                    catch (Exception)
                    {
                    }
                }
                if (!yeni.resim3.Equals(""))
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(yeni.resim3.Substring(yeni.resim3.IndexOf(',') + 1));
                        filename2 = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.resim3.Substring(11, yeni.resim3.IndexOf(";") - 11);
                        while (filename2.Equals(filename1))
                        {
                            filename2 = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + yeni.resim2.Substring(11, yeni.resim2.IndexOf(";") - 11);
                        }
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

                        bmpKucuk.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/yorum/" + filename2);
                        resimler.Add(new yorum_resimleri { resim_adi = filename2 });
                    }
                    catch (Exception)
                    {
                    }
                }
                #endregion
                gelen.yorum_resimleri = resimler;
                gelen.onay = 0;
                gelen.tarih = DateTime.Now;
                baglanti.Entry(gelen).State = System.Data.Entity.EntityState.Added;
                baglanti.yorums.Add(gelen);
                baglanti.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string MesajGonder(iletisimModel yeni)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                yorum gelen = new yorum();
                if (baglanti.mesaj_ayarlari.FirstOrDefault().mesaj_kaydet > 0)
                {
                    mesajlar yeni_mesaj = new mesajlar()
                    {
                        adi = yeni.adsoyad,
                        ip = "mobil-app",
                        konu = yeni.konu,
                        mesaj = yeni.mesaj,
                        mail = yeni.eposta,
                        tel = yeni.telefon,
                        tarih = DateTime.Now
                    };
                    baglanti.Entry(yeni_mesaj).State = System.Data.Entity.EntityState.Added;
                    baglanti.mesajlars.Add(yeni_mesaj);
                    baglanti.SaveChanges();
                }
                if (baglanti.mesaj_ayarlari.FirstOrDefault().mail_gonder > 0)
                {
                    List<mesaj_alici_listesi> alicilar = baglanti.mesaj_alici_listesi.ToList();
                    for (int i = 0; i < alicilar.Count; i++)
                    {
                        try
                        {
                            string MailBody = string.Empty;
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/MailTemplate/MesajGonder.html")))
                            {
                                MailBody = reader.ReadToEnd();
                            }
                            MailBody = MailBody.Replace("{KisiAdi}", alicilar[i].alici_adi);
                            MailBody = MailBody.Replace("{MailAdi}", yeni.adsoyad);
                            MailBody = MailBody.Replace("{Telefon}", yeni.telefon);
                            MailBody = MailBody.Replace("{Konu}", yeni.konu);
                            MailBody = MailBody.Replace("{Email}", yeni.eposta);
                            MailBody = MailBody.Replace("{Mesaj}", yeni.mesaj);
                            MailGonder(alicilar[i].alici_mail, MailBody, "EduAdvisor Web Sitesinden Mesajınız Var! ");
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string SayfaAciklamaGetir(string id)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                sayfalar s = baglanti.sayfalars.AsNoTracking().Where(x => x.id.ToString().Equals(id)).FirstOrDefault();
                return s.turkce;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static List<SorulanSorularModel> SorulanSorular()
        {
            List<SorulanSorularModel> sorular = new List<SorulanSorularModel>();
            eduadvisorContext baglanti = ctx.Baglanti;
            var lst = baglanti.sorulars.AsNoTracking().OrderBy(x => x.sira_no).ToList();
            for (int i = 0; i < lst.Count; i++)
                sorular.Add(new SorulanSorularModel { Baslik = (lst[i].soru), Cevap = (lst[i].cevap) });
            return sorular;
        }
        public static string OnKayitOlustur(OnKayitFormModel yeni)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                on_kayit_basvuru gelen = new on_kayit_basvuru();
                gelen.adres = yeni.acik_adres;
                gelen.adi = yeni.adi;
                gelen.cinsiyet = Convert.ToInt32(yeni.cinsiyet);
                gelen.dogum_il = Convert.ToInt32(yeni.dogum_sehir);
                gelen.dog_tar = yeni.dogum_tarihi;
                gelen.dogum_ulke = Convert.ToInt32(yeni.dogum_ulke);
                gelen.baslayacagi_tarih = yeni.egitim_baslangici;
                gelen.email = yeni.email;
                gelen.kurs_hafta = yeni.kurs_katilim_suresi;
                gelen.uyruk = Convert.ToInt32(yeni.milliyeti);
                gelen.pass_no = yeni.pasaport_no;
                gelen.pass_tarih = yeni.pasaport_tarihi;
                gelen.program_id = Convert.ToInt32(yeni.programi);
                gelen.program_tur_id = Convert.ToInt32(yeni.program_turu);
                gelen.soyadi = yeni.soyadi;
                gelen.tel_cep = yeni.telefon_no_cep;
                gelen.tel_ev = yeni.telefon_no_ev;
                gelen.uye_id = Convert.ToInt32(yeni.uye_id);
                gelen.dil_seviye = yeni.yabanci_dil;
                gelen.yas_il = Convert.ToInt32(yeni.yasadigi_sehir);
                gelen.yas_ulke = Convert.ToInt32(yeni.yasadigi_ulke);
                gelen.basvurdugu_okul = Convert.ToInt32(yeni.okul_id);
                gelen.basvuru_tarihi = DateTime.Now;
                gelen.mezun_okul = "-1";
                if (baglanti.on_kayit_basvuru.AsNoTracking().Where(x => x.uye_id == gelen.uye_id && x.basvurdugu_okul == gelen.basvurdugu_okul && x.program_id == gelen.program_id && x.durumu <= 1).ToList().Count > 0)
                    return "-9";
                string sifre = randomKodUret();
                while (baglanti.on_kayit_basvuru.AsNoTracking().Where(x => x.on_kayitkodu.Equals(sifre)).ToList().Count > 0)
                {
                    sifre = randomKodUret();
                }
                gelen.on_kayitkodu = sifre;
                baglanti.Entry(gelen).State = System.Data.Entity.EntityState.Added;
                baglanti.on_kayit_basvuru.Add(gelen);
                baglanti.SaveChanges();

                string MailBody = string.Empty;
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/MailTemplate/OnKayitYapildi.html")))
                {
                    MailBody = reader.ReadToEnd();
                }
                MailBody = MailBody.Replace("{Sifre}", sifre);
                MailGonder(gelen.email, MailBody, "Eduadvisor Ön Kayıt Kodunuz");
                gelen = baglanti.on_kayit_basvuru.AsNoTracking().Where(x => x.id == gelen.id).FirstOrDefault();
                List<kurumsal_yoneticiler> alicilar = gelen.okullar.kurumsal_yonetici_sube.Select(c => c.kurumsal_yoneticiler).ToList();
                for (int i = 0; i < alicilar.Count; i++)
                {
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/MailTemplate/KurumsalOnKayitYapildi.html")))
                    {
                        MailBody = reader.ReadToEnd();
                    }
                    MailGonder(alicilar[i].mail, MailBody, "Eduadvisor Sitesinden Başvurunuz Var !");
                }

                List<on_basvuru_alici_listesi> alicilar2 = baglanti.on_basvuru_alici_listesi.AsNoTracking().ToList();
                for (int i = 0; i < alicilar2.Count; i++)
                {
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/MailTemplate/EduOnKayitYapildi.html")))
                    {
                        MailBody = reader.ReadToEnd();
                    }
                    MailBody = MailBody.Replace("{Adi}", alicilar2[i].adi);
                    MailBody = MailBody.Replace("{OkulAdi}", gelen.okullar.adi);
                    MailBody = MailBody.Replace("{ProgramAdi}", gelen.program_havuzu.adi);
                    MailGonder(alicilar2[i].mail, MailBody, "Eduadvisor Sitesinden Başvurunuz Var!");
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static UyeModel GoogleGirisi(GoogleProfile bilgiler)
        {
            UyeModel uye = new UyeModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uyeler u = baglanti.uyelers.AsNoTracking().Where(x => x.mail.Equals(bilgiler.Email)).FirstOrDefault();
                if (u == null)
                {
                    string sifre = randomSifreUret();
                    string resimadi = "Eduadvisor-uye-" + DateTime.Now.ToString("yyyyMMddHmmss");
                    try
                    {
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData(bilgiler.Picture);
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
                    u = new uyeler
                    {
                        sifre = sifrele(sifre),
                        adi = bilgiler.GivenName,
                        Cinsiyet = 2,
                        haber_bulten = Convert.ToByte(true),
                        hesap_aktiflik = 1,
                        kara_liste = Convert.ToByte(false),
                        kayit_tur = 0,
                        soyadi = bilgiler.FamilyName,
                        fotograf = resimadi,
                        tarih = DateTime.Now,
                        biyografi = "",
                        ilce_id = 0,
                        il_id = 0,
                        ulke_id = 0,
                        silindi = 0,
                        mail = bilgiler.Email,
                        puani = 0,
                        uye_adres = "",
                        yas = "0"
                    };
                    baglanti.uyelers.Add(u);
                    baglanti.SaveChanges();
                    string kod = DogrulamaKodUret();
                    while (baglanti.uye_dogrulama.AsNoTracking().Where(x => x.dogrulama_kodu.Equals(kod)).ToList().Count > 0)
                    {
                        kod = DogrulamaKodUret();
                    }
                    baglanti.uye_dogrulama.Add(new uye_dogrulama { uye_id = u.id, dogrulama_kodu = kod });
                    baglanti.SaveChanges();
                    string MailBody = string.Empty;
                    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/UyeKayitOl.html")))
                    {
                        MailBody = reader.ReadToEnd();
                    }
                    MailBody = MailBody.Replace("{Mail}", u.mail);
                    MailBody = MailBody.Replace("{Kod}", kod);
                    MailBody = MailBody.Replace("{Sifre}", sifre);
                    MailGonder(u.mail, MailBody, "EduAdvisor Giriş Şifreniz");
                }
                else
                {
                    u.hesap_aktiflik = 1;
                    baglanti.SaveChanges();
                }
                uye.Adi = u.adi;
                uye.adres = u.uye_adres;
                uye.biyografi = u.biyografi;
                uye.Cinsiyet = u.Cinsiyet == null || u.Cinsiyet == 2 ? "Belirtmek İstemiyorum" : (u.Cinsiyet == 0 ? "Kadın" : "Erkek");
                uye.fotograf = u.fotograf;
                uye.Haber_Bulten = u.haber_bulten == null ? "0" : (u.haber_bulten == 1 ? "1" : "0");
                uye.Id = u.id.ToString();
                uye.mail = u.mail;
                uye.puani = u.puani.ToString();
                uye.Soyadi = u.soyadi;
                uye.tel_no = u.tel_no;
                uye.Yas = u.yas;
                uye.giris = "1";
                uye.dog_kod = u.uye_dogrulama.dogrulama_kodu;
            }
            catch (Exception)
            {
                uye.giris = "Beklenmedik Bir Hata Oluştu! Tekrar Deneyiniz.";
            }
            return uye;
        }
        public static UyeModel LinkedinGirisi(LinkedinProfile bilgiler)
        {
            UyeModel uye = new UyeModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uyeler u = baglanti.uyelers.AsNoTracking().Where(x => x.mail.Equals(bilgiler.Email)).FirstOrDefault();
                if (u == null)
                {
                    string sifre = randomSifreUret();
                    string resimadi = "Eduadvisor-uye-" + DateTime.Now.ToString("yyyyMMddHmmss");
                    try
                    {
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData(bilgiler.pictureUrl);
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
                    u = new uyeler
                    {
                        sifre = sifrele(sifre),
                        adi = bilgiler.firstName,
                        Cinsiyet = 2,
                        haber_bulten = Convert.ToByte(true),
                        hesap_aktiflik = 1,
                        kara_liste = Convert.ToByte(false),
                        kayit_tur = 0,
                        soyadi = bilgiler.lastName,
                        fotograf = resimadi,
                        tarih = DateTime.Now,
                        biyografi = "",
                        ilce_id = 0,
                        il_id = 0,
                        ulke_id = 0,
                        silindi = 0,
                        mail = bilgiler.Email,
                        puani = 0,
                        uye_adres = "",
                        yas = "0"
                    };
                    baglanti.uyelers.Add(u);
                    baglanti.SaveChanges();
                    string kod = DogrulamaKodUret();
                    while (baglanti.uye_dogrulama.AsNoTracking().Where(x => x.dogrulama_kodu.Equals(kod)).ToList().Count > 0)
                    {
                        kod = DogrulamaKodUret();
                    }
                    baglanti.uye_dogrulama.Add(new uye_dogrulama { uye_id = u.id, dogrulama_kodu = kod });
                    baglanti.SaveChanges();
                    string MailBody = string.Empty;
                    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/MailTemplate/UyeKayitOl.html")))
                    {
                        MailBody = reader.ReadToEnd();
                    }
                    MailBody = MailBody.Replace("{Mail}", u.mail);
                    MailBody = MailBody.Replace("{Kod}", kod);
                    MailBody = MailBody.Replace("{Sifre}", sifre);
                    MailGonder(u.mail, MailBody, "EduAdvisor Giriş Şifreniz");
                }
                else
                {
                    u.hesap_aktiflik = 1;
                    baglanti.SaveChanges();
                }
                uye.Adi = u.adi;
                uye.adres = u.uye_adres;
                uye.biyografi = u.biyografi;
                uye.Cinsiyet = u.Cinsiyet == null || u.Cinsiyet == 2 ? "Belirtmek İstemiyorum" : (u.Cinsiyet == 0 ? "Kadın" : "Erkek");
                uye.fotograf = u.fotograf;
                uye.Haber_Bulten = u.haber_bulten == null ? "0" : (u.haber_bulten == 1 ? "1" : "0");
                uye.Id = u.id.ToString();
                uye.mail = u.mail;
                uye.puani = u.puani.ToString();
                uye.Soyadi = u.soyadi;
                uye.tel_no = u.tel_no;
                uye.Yas = u.yas;
                uye.giris = "1";
                uye.dog_kod = u.uye_dogrulama.dogrulama_kodu;
            }
            catch (Exception)
            {
                uye.giris = "Beklenmedik Bir Hata Oluştu! Tekrar Deneyiniz.";
            }
            return uye;
        }
        public static ProfilimKullaniciBilgileriModel ProfilimKullaniciBilgileriGetir(string id)
        {
            ProfilimKullaniciBilgileriModel donecek = new ProfilimKullaniciBilgileriModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                if (baglanti.uyelers.AsNoTracking().Where(x => x.id.ToString().Equals(id)).ToList().Count > 0)
                {
                    donecek = baglanti.uyelers.AsNoTracking().Where(x => x.id.ToString().Equals(id)).Select(b => new ProfilimKullaniciBilgileriModel
                    {
                        adi = b.adi,
                        soyadi = b.soyadi,
                        biyografi = b.biyografi,
                        cinsiyet = b.Cinsiyet == null || b.Cinsiyet == 2 ? "Belirtmek İstemiyorum" : (b.Cinsiyet == 1 ? "Erkek" : "Kadın"),
                        email = b.mail,
                        haber_bulten = b.haber_bulten == null ? "0" : b.haber_bulten.ToString(),
                        sehir_id = b.il_id.ToString(),
                        ulke_id = b.ulke_id.ToString(),
                        telefon = b.tel_no,
                        yas = b.yas,
                        ilgili_egitimler = b.uye_ilgi.Select(c => c.egitim_id.ToString()).ToList(),
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return donecek;
        }
        public static string ProfilimKullaniciGuncelle(ProfilimKullaniciBilgileriModel gelen)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uyeler uye = baglanti.uyelers.AsNoTracking().Where(x => x.mail.Equals(gelen.email)).FirstOrDefault();
                if (uye != null)
                {
                    uye.biyografi = gelen.biyografi;
                    uye.adi = gelen.adi;
                    uye.soyadi = gelen.soyadi;
                    uye.biyografi = gelen.biyografi;
                    uye.Cinsiyet = Convert.ToInt32(gelen.cinsiyet);
                    uye.haber_bulten = Convert.ToByte(gelen.haber_bulten.Equals("1"));
                    uye.il_id = Convert.ToInt32(gelen.sehir_id);
                    uye.ulke_id = Convert.ToInt32(gelen.ulke_id);
                    uye.tel_no = gelen.telefon;
                    uye.yas = gelen.yas;
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Modified;
                    baglanti.SaveChanges();
                    baglanti.Entry(uye).State = System.Data.Entity.EntityState.Detached;
                    baglanti.SaveChanges();
                    List<uye_ilgi> ilgiler = baglanti.uye_ilgi.AsNoTracking().Where(x => x.uye_id == uye.id).ToList();
                    bool save_yap = false;
                    for (int i = 0; i < ilgiler.Count; i++)
                    {
                        if (!gelen.ilgili_egitimler.Contains(ilgiler[i].egitim_id.ToString()))
                        {
                            baglanti.Entry(ilgiler[i]).State = System.Data.Entity.EntityState.Deleted;
                            baglanti.uye_ilgi.Remove(ilgiler[i]);
                            save_yap = true;
                        }
                    }
                    if (save_yap)
                        baglanti.SaveChanges();
                    save_yap = false;
                    for (int i = 0; i < gelen.ilgili_egitimler.Count; i++)
                    {
                        int egitim_id = Convert.ToInt32(gelen.ilgili_egitimler[i]);
                        if (baglanti.uye_ilgi.AsNoTracking().Where(x => x.uye_id.Equals(uye.id) && x.egitim_id.Equals(egitim_id)).ToList().Count == 0)
                        {
                            uye_ilgi y = new uye_ilgi();
                            y.egitim_id = egitim_id;
                            y.uye_id = uye.id;
                            baglanti.Entry(y).State = System.Data.Entity.EntityState.Added;
                            baglanti.uye_ilgi.Add(y);
                            save_yap = true;
                        }
                    }
                    if (save_yap)
                        baglanti.SaveChanges();
                    return "1";
                }
                else
                    return "-9";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static ProfilResimResultModel ProfilResimGuncelle(ProfilResimModel gelen)
        {
            ProfilResimResultModel donecek = new ProfilResimResultModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uyeler uye = baglanti.uyelers.AsNoTracking().Where(x => x.id.ToString().Equals(gelen.id)).FirstOrDefault();
                string filename = "";
                try
                {
                    var bytes = Convert.FromBase64String(gelen.resim.Substring(gelen.resim.IndexOf(',') + 1));
                    filename = "Eduadvisor-uye-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + gelen.resim.Substring(11, gelen.resim.IndexOf(";") - 11);
                    System.Drawing.Image image;
                    using (var ms = new MemoryStream(bytes, 0, bytes.Length))
                    {
                        image = System.Drawing.Image.FromStream(ms, true);
                    }
                    System.Drawing.Bitmap bmpKucuk;
                    if (image.Height > 150 && image.Width < 150)
                    {
                        bmpKucuk = new System.Drawing.Bitmap(image, (image.Width * 150) / image.Height, 150);
                    }
                    else if (image.Width > 150 && image.Height < 150)
                    {
                        bmpKucuk = new System.Drawing.Bitmap(image, 150, (image.Height * 150) / image.Width);
                    }
                    else if (image.Width > 150 && image.Height > 150)
                    {
                        if (image.Width > image.Height)
                            bmpKucuk = new System.Drawing.Bitmap(image, 150, (image.Height * 150) / image.Width);
                        else
                            bmpKucuk = new System.Drawing.Bitmap(image, (image.Width * 150) / image.Height, 150);
                    }
                    else
                        bmpKucuk = new System.Drawing.Bitmap(image);

                    bmpKucuk.Save("C:/Inetpub/vhosts/eduadvisor.co.uk/httpdocs/Content/img/kul_profil/" + filename);
                }
                catch (Exception)
                {
                    filename = uye.fotograf;
                }
                uye.fotograf = filename;
                baglanti.Entry(uye).State = System.Data.Entity.EntityState.Modified;
                baglanti.SaveChanges();
                baglanti.Entry(uye).State = System.Data.Entity.EntityState.Detached;
                baglanti.SaveChanges();
                donecek.sonuc = "1";
                donecek.data = filename;
            }
            catch (Exception e)
            {
                donecek.sonuc = e.Message;
            }
            return donecek;
        }
        public static List<TextValueModel> ProfilimSeviyeSistemiBilgileri()
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            List<seviye_bilgilendirme> seviyeler = baglanti.seviye_bilgilendirme.AsNoTracking().OrderBy(x => x.sira_no).ToList();
            List<TextValueModel> bilgilendirmeler = new List<TextValueModel>();
            for (int j = 0; j < seviyeler.Count; j++)
                bilgilendirmeler.Add(new TextValueModel { Text = (seviyeler[j].baslik), Value = (seviyeler[j].icerik) });
            return bilgilendirmeler;
        }
        public static GirisUyeDoldurModel GirisUyeGetir(string id)
        {
            GirisUyeDoldurModel donecek = new GirisUyeDoldurModel();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                if (baglanti.uyelers.AsNoTracking().Where(x => x.id.ToString().Equals(id)).ToList().Count > 0)
                {
                    donecek = baglanti.uyelers.AsNoTracking().Where(x => x.id.ToString().Equals(id)).Select(b => new GirisUyeDoldurModel
                    {
                        adi = b.adi,
                        soyadi = b.soyadi,
                        fotograf = b.fotograf,
                        dog_kod = b.uye_dogrulama.dogrulama_kodu
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return donecek;
        }
        public static List<ProfilimEgitimAldigimOkullarModel> ProfilimEgitimAldigiOkullarGetir(string id)
        {
            List<ProfilimEgitimAldigimOkullarModel> okullar = new List<ProfilimEgitimAldigimOkullarModel>();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                okullar = baglanti.uye_okullari.AsNoTracking().Where(x => x.uye_id.ToString().Equals(id) && x.okullar.yayinda > 0 && x.okullar.arsivde == false).Select(b => new ProfilimEgitimAldigimOkullarModel
                {
                    egitim_alinan_donem = b.baslangic + "-" + (b.bitis.Equals("-1") ? "Halen Okuyor" : b.bitis),
                    egitim_turu = b.okullar.egitim_id.ToString(),
                    egitim_turu_adi = b.okullar.egitim_turleri.adi,
                    grup_adi = b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi,
                    grup_id = b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.id.ToString(),
                    okul_adi = b.okullar.adi,
                    okul_id = b.okul_id.ToString(),
                    program_adi = b.program_havuzu.adi,
                    program_turu = b.onkayit_egitim_turleri.adi,
                    id = b.id.ToString()
                }).ToList();
            }
            catch (Exception)
            {
            }
            return okullar;
        }
        public static List<KampusluGrupGetirModel> KampusluGrupGetir()
        {
            List<KampusluGrupGetirModel> gruplar = new List<KampusluGrupGetirModel>();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                gruplar = baglanti.okul_gruplari.AsNoTracking().Where(x => x.okul_grup_iliski.Where(y => y.okullar.yayinda > 0 && y.okullar.arsivde == false).ToList().Count > 0)
                         .Select(b => new KampusluGrupGetirModel
                         {
                             grup = new TextValueModel { Text = b.adi, Value = b.id.ToString() },
                             egitim_id = b.egitim_id.ToString()
                         }).OrderBy(x => x.grup.Value).ToList();
                for (int i = 0; i < gruplar.Count; i++)
                {
                    int grup_id = Convert.ToInt32(gruplar[i].grup.Value);
                    gruplar[i].kampusler = baglanti.okul_grup_iliski.AsNoTracking().Where(x => x.grup_id == grup_id && x.okullar.yayinda > 0 && x.okullar.arsivde == false).
                                        Select(b => new TextValueModel
                                        {
                                            Text = b.okullar.adi,
                                            Value = b.okullar.id.ToString()
                                        }).ToList();
                    if (gruplar[i].kampusler.Count == 0)
                    {
                        gruplar.RemoveAt(i);
                        i--;
                    }
                }
            }
            catch (Exception)
            {
            }
            return gruplar;
        }
        public static string ProfilimEgitimAldigiOkulEkle(ProfilimEgitimAldigimOkulEkleModel gelen)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                if (baglanti.uye_okullari.AsNoTracking().Where(x => x.uye_id.ToString() == gelen.uye_id &&
                x.okul_id.ToString() == gelen.okul_id && x.program_id.ToString() == gelen.program_id && x.program_tur_id.ToString() == gelen.program_tur_id).ToList().Count > 0)
                    return "-9";
                else
                {
                    uye_okullari yeni = new uye_okullari()
                    {
                        baslangic = gelen.baslangic,
                        bitis = gelen.bitis,
                        eklenme_tarihi = DateTime.Now,
                        okul_id = Convert.ToInt32(gelen.okul_id),
                        program_id = Convert.ToInt32(gelen.program_id),
                        program_tur_id = Convert.ToInt32(gelen.program_tur_id),
                        uye_id = Convert.ToInt32(gelen.uye_id)
                    };
                    baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                    baglanti.uye_okullari.Add(yeni);
                    baglanti.SaveChanges();
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string ProfilimEgitimAldigiOkulSil(string id)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                uye_okullari silinecek = baglanti.uye_okullari.AsNoTracking().Where(x => x.id.ToString() == id).FirstOrDefault();
                if (silinecek != null)
                {
                    baglanti.Entry(silinecek).State = System.Data.Entity.EntityState.Deleted;
                    baglanti.uye_okullari.Remove(silinecek);
                    baglanti.SaveChanges();
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static List<ProfilimZiyaretEdilenOkullarModel> ProfilimZiyaretEdilenOkullarGetir(string id)
        {
            List<ProfilimZiyaretEdilenOkullarModel> okullar = new List<ProfilimZiyaretEdilenOkullarModel>();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                okullar = baglanti.okul_ziyaret.AsNoTracking().Where(x => x.uye_id.ToString() == id && x.okullar.yayinda > 0 && x.okullar.arsivde == false).Select(b => new ProfilimZiyaretEdilenOkullarModel
                {
                    grup_adi = b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi,
                    grup_id = b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.id.ToString(),
                    okul_adi = b.okullar.adi,
                    okul_id = b.okul_id.ToString(),
                    logo = "https://eduadvisor.co.uk/Content/img/okul/" + b.okullar.logo
                }).ToList();
            }
            catch (Exception)
            {
            }
            return okullar;
        }
        public static List<ProfilimOnKayitlarimModel> ProfilimOnKayitlarimGetir(string id)
        {
            List<ProfilimOnKayitlarimModel> basvurular = new List<ProfilimOnKayitlarimModel>();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                basvurular = baglanti.on_kayit_basvuru.AsNoTracking().Where(x => x.uye_id.ToString() == id).Select(b => new ProfilimOnKayitlarimModel
                {
                    acik_adres = b.adres,
                    adi_soyadi = b.adi + " " + b.soyadi,
                    baslama_tarih = b.baslayacagi_tarih,
                    basvurdugu_okul = b.okullar.okul_grup_iliski.ToList().Count == 0 ? b.okullar.adi : b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi + "-" + b.okullar.adi,
                    basvuru_tarihi = b.basvuru_tarihi.ToString(),
                    cinsiyet = b.cinsiyet == 0 ? "Kadın" : (b.cinsiyet == 1 ? "Erkek" : "Belirtmek İstemiyorum"),
                    dil_seviye = b.dil_seviye,
                    dogum_yeri = baglanti.ils.Where(x => x.id == b.dogum_il).Select(x => x.adi + "/" + x.ulke.adi).FirstOrDefault(),
                    yas_yer = baglanti.ils.Where(x => x.id == b.yas_il).Select(x => x.adi + "/" + x.ulke.adi).FirstOrDefault(),
                    dog_tar = b.dog_tar,
                    email = b.email,
                    kurs_hafta = b.kurs_hafta,
                    on_kayit_kodu = b.on_kayitkodu,
                    pass_no = b.pass_no,
                    pass_tarih = b.pass_tarih,
                    program = b.program_havuzu.adi,
                    program_tur = b.onkayit_egitim_turleri.adi,
                    tel_cep = b.tel_cep,
                    tel_ev = b.tel_ev,
                    uyruk = baglanti.ulkes.Where(x => x.id == b.uyruk).FirstOrDefault().adi,
                    durumu = b.durumu == 2 ? "Silindi" : (b.durumu == 1 ? "Onaylandı" : "İnceleniyor"),
                    DilBilgisiGozukecek = b.okullar.egitim_id == 2 ? "True" : "False"
                }).ToList();
            }
            catch (Exception)
            {
            }
            return basvurular;
        }
        public static List<ProfilimYorumlarModel> ProfilimYorumlariGetir(string id)
        {
            List<ProfilimYorumlarModel> yorumlar = new List<ProfilimYorumlarModel>();
            try
            {

                eduadvisorContext baglanti = ctx.Baglanti;
                yorumlar = baglanti.yorums.AsNoTracking().Where(x => x.uye_id.ToString() == id && x.okullar.arsivde == false && x.okullar.yayinda > 0 && x.uye_sildi == false).Select(b => new ProfilimYorumlarModel
                {
                    id = b.id.ToString(),
                    baslik = b.baslik,
                    okul_adi = b.okullar.okul_grup_iliski.ToList().Count == 0 ? b.okullar.adi : b.okullar.okul_grup_iliski.FirstOrDefault().okul_gruplari.adi + "-" + b.okullar.adi,
                    puani = b.puani.ToString(),
                    tarih = b.tarih.ToString(),
                    icerik = b.icerik,
                    durumu = b.onay == 0 ? "Beklemede" : (b.onay == 1 ? "Onaylandı" : "Onaylanmadı"),
                    resimler = b.yorum_resimleri.Select(x => new TextValueModel { Text = "https://eduadvisor.co.uk/Content/img/yorum/" + x.resim_adi, Value = x.id.ToString() }).ToList()
                }).OrderBy(x => x.tarih).ToList();
            }
            catch (Exception e)
            {
            }
            return yorumlar;
        }
        public static string ProfilimYorumResimleriGuncelle(ProfilimYorumlarModel gelen)
        {
            try
            {
                for (int i = 0; i < gelen.resimler.Count; i++)
                    gelen.resimler[i].Value = gelen.resimler[i].Value;
                eduadvisorContext baglanti = ctx.Baglanti;
                List<yorum_resimleri> resimler = baglanti.yorum_resimleri.AsNoTracking().Where(x => x.yorum_id.ToString() == gelen.id).ToList();
                if (gelen.resim_1_degisti)
                    if (gelen.resimler.Count > 0)
                        ProfilimYorumResimleriGuncelleResimIslemi(gelen.resimler[0], gelen.id, baglanti, resimler.Where(x => x.id.ToString() == gelen.resimler[0].Value).FirstOrDefault());
                if (gelen.resim_2_degisti)
                    if (gelen.resimler.Count > 1)
                        ProfilimYorumResimleriGuncelleResimIslemi(gelen.resimler[1], gelen.id, baglanti, resimler.Where(x => x.id.ToString() == gelen.resimler[1].Value).FirstOrDefault());
                if (gelen.resim_3_degisti)
                    if (gelen.resimler.Count > 2)
                        ProfilimYorumResimleriGuncelleResimIslemi(gelen.resimler[2], gelen.id, baglanti, resimler.Where(x => x.id.ToString() == gelen.resimler[2].Value).FirstOrDefault());
                if (gelen.resim_1_degisti || gelen.resim_2_degisti || gelen.resim_3_degisti)
                    baglanti.SaveChanges();

                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static void ProfilimYorumResimleriGuncelleResimIslemi(TextValueModel gelen, string yorum_id, eduadvisorContext baglanti, yorum_resimleri guncel = null)
        {
            string filename = "";
            if (gelen.Value != "0")
            {
                if (gelen.Text != "-1")
                {
                    try
                    {
                        var bytes = Convert.FromBase64String(gelen.Text.Substring(gelen.Text.IndexOf(',') + 1));
                        filename = "Eduadvisor-okul-yorum-" + DateTime.Now.ToString("ddHHssyyyyffffffmmMM") + "." + gelen.Text.Substring(11, gelen.Text.IndexOf(";") - 11);
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
                    }
                    if (gelen.Value == "-1")
                    {
                        yorum_resimleri yeni = new yorum_resimleri()
                        {
                            yorum_id = Convert.ToInt32(yorum_id),
                            resim_adi = filename
                        };
                        baglanti.Entry(yeni).State = System.Data.Entity.EntityState.Added;
                        baglanti.yorum_resimleri.Add(yeni);
                    }
                    else
                    {
                        guncel.resim_adi = filename;
                        baglanti.Entry(guncel).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                else
                {
                    baglanti.Entry(guncel).State = System.Data.Entity.EntityState.Deleted;
                    baglanti.yorum_resimleri.Remove(guncel);
                }
            }
        }
        public static List<ApiGrupDetayOkulModel> GrupDetayGetir(string seo_url, string dil = "tr-TR")
        {
            List<ApiGrupDetayOkulModel> okullar = new List<ApiGrupDetayOkulModel>();
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                okullar = baglanti.Database.SqlQuery<ApiGrupDetayOkulModel>("select o.adi as 'okul_adi',case when(o.ulke_id>0) then " + (dil == "tr-TR" ? "u.adi" : "u.adi_ing") + " else '' end as 'ulke_adi'," +
                     "case when(o.sehir_id>0) then i.adi else '' end as 'sehir_adi',o.seo_url,case when(ff.id>0) then " + (dil == "tr-TR" ? "fh.adi" : "fh.adi_ingilizce") + " else '' end as 'fakulte_adi',o.id " +
                     "from okullar o left join okul_fakulteleri ff on o.id=ff.okul_id left join fakulte_havuzu fh on ff.fakulte_id=fh.id left join ulke u on u.id=o.ulke_id left join il i on i.id=o.sehir_id " +
                     "where (o.id=(select top 1 ogi.okul_id from okul_gruplari og inner join okul_grup_iliski ogi on og.id=ogi.grup_id where og.seo_url='" + seo_url + "') or o.merkez_id=(select top 1 ogi.okul_id from okul_gruplari og inner join okul_grup_iliski ogi on og.id=ogi.grup_id where og.seo_url='" + seo_url + "')) and o.yayinda>0 and o.arsivde=0").ToList();
            }
            catch (Exception e)
            {
            }
            return okullar;
        }
        public static List<TextValueModel> Ulkeler()
        {
            List<TextValueModel> ulkeler = new List<TextValueModel>();
            ulkeler.Add(new TextValueModel { Text = "Tüm Ülkeler", Value = "-1" });
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                ulkeler.AddRange(baglanti.Database.SqlQuery<TextValueModel>("select cast(id as varchar(5)) as 'Value',adi as 'Text' from ulke order by sira_no").ToList());
            }
            catch (Exception e)
            {
            }
            return ulkeler;
        }
        public static List<TextValueModel> SehirleriGetir(int id)
        {
            List<TextValueModel> sehirler = new List<TextValueModel>();
            sehirler.Add(new TextValueModel { Text = "Tüm Şehirler", Value = "-1" });
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                sehirler.AddRange(baglanti.Database.SqlQuery<TextValueModel>("select cast(id as varchar(5)) as 'Value',adi as 'Text' from il where ulke_id=" + id + " order by sira_no").ToList());
            }
            catch (Exception e)
            {
            }
            return sehirler;
        }
        public static string YakinOkulSayisiGetir(YakinOkulSayiModel value)
        {
            try
            {
                eduadvisorContext baglanti = ctx.Baglanti;
                return baglanti.okul_lokasyon.Where(x => x.okullar.yayinda > 0 && x.okullar.okul_grup_iliski.Count > 0 && x.okullar.arsivde == false).Count().ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static List<Models.HaritaOkulModel> SizeYakinOkullarGetir(SizeYakinOkullarPostModel value)
        {
            eduadvisorContext baglanti = ctx.Baglanti;
            return baglanti.Database.SqlQuery<Models.HaritaOkulModel>("select o.id,o.seo_url,ol.lat,ol.lng," +
                   "case when (o.egitim_id=1 and fh.adi is not null) then  case when(o.merkez_id<=0) then case when(select count(*) from okul_grup_iliski ogi where ogi.okul_id=o.id)>0 then " +
                   "(select og.adi from okul_grup_iliski ogi,okul_gruplari og where ogi.okul_id=o.id and og.id=ogi.grup_id) else '' end else " +
                   "case when(select count(*) from okul_grup_iliski ogi where ogi.okul_id=o.merkez_id)>0 then " +
                   "(select og.adi from okul_grup_iliski ogi,okul_gruplari og where ogi.okul_id=o.merkez_id and og.id=ogi.grup_id) " +
                   "else '' end end + ' - ' + fh.adi +  ' ('+o.adi+')' else  case when(o.merkez_id<=0) then case when(select count(*) from okul_grup_iliski ogi where ogi.okul_id=o.id)>0 then " +
                   "(select og.adi from okul_grup_iliski ogi,okul_gruplari og where ogi.okul_id=o.id and og.id=ogi.grup_id) else '' end else " +
                   "case when(select count(*) from okul_grup_iliski ogi where ogi.okul_id=o.merkez_id)>0 then " +
                   "(select og.adi from okul_grup_iliski ogi,okul_gruplari og where ogi.okul_id=o.merkez_id and og.id=ogi.grup_id) " +
                   "else '' end end + ' - ' + o.adi end " +
                   "as 'Label' from okul_lokasyon ol inner join okullar o on o.id=ol.okul_id left join okul_fakulteleri ff on ff.okul_id = o.id left join fakulte_havuzu fh on  ff.fakulte_id = fh.id " +
                   "where (select count(*) from okul_grup_iliski ogi where ogi.okul_id=o.merkez_id or ogi.okul_id=o.id)>0 and lng between " + value.lng.Replace(",", ".") + "-0.04 and " + value.lng.Replace(",", ".") + "+0.04 and lat between " + value.lat.Replace(",", ".") + "-0.04 and " + value.lat.Replace(",", ".") + "+0.04 and o.egitim_id=" + value.egitim_id + " and o.arsivde=0 and o.yayinda=1").ToList();
        }
    }
}