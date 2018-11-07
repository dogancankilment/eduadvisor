using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using dll.Models.Mapping;

namespace dll.Models
{
    public partial class eduadvisorContext : DbContext
    {
        static eduadvisorContext()
        {
            Database.SetInitializer<eduadvisorContext>(null);
        }

        public eduadvisorContext()
            : base("Name=eduadvisorContext")
        {
        }

        public DbSet<aboneler> abonelers { get; set; }
        public DbSet<anket_cevaplari> anket_cevaplari { get; set; }
        public DbSet<anket_cevaplayan> anket_cevaplayan { get; set; }
        public DbSet<Anket_Sorulari> Anket_Sorulari { get; set; }
        public DbSet<Anketler> Anketlers { get; set; }
        public DbSet<egitim_durumlari> egitim_durumlari { get; set; }
        public DbSet<egitim_duzeyleri> egitim_duzeyleri { get; set; }
        public DbSet<egitim_ozellikleri> egitim_ozellikleri { get; set; }
        public DbSet<egitim_turleri> egitim_turleri { get; set; }
        public DbSet<eyalet> eyalets { get; set; }
        public DbSet<fakulte_havuzu> fakulte_havuzu { get; set; }
        public DbSet<fiyat> fiyats { get; set; }
        public DbSet<fiyat_deger> fiyat_deger { get; set; }
        public DbSet<fiyat_deger_haftalik> fiyat_deger_haftalik { get; set; }
        public DbSet<fiyat_ogr_tur> fiyat_ogr_tur { get; set; }
        public DbSet<fiyat_tur> fiyat_tur { get; set; }
        public DbSet<genel_ayarlar> genel_ayarlar { get; set; }
        public DbSet<giris_kayitlari> giris_kayitlari { get; set; }
        public DbSet<il> ils { get; set; }
        public DbSet<ilce> ilces { get; set; }
        public DbSet<indirim_uye> indirim_uye { get; set; }
        public DbSet<indirim_uye_bilgilendirme> indirim_uye_bilgilendirme { get; set; }
        public DbSet<kurs_hafta> kurs_hafta { get; set; }
        public DbSet<kurumsal_giris_kayitlari> kurumsal_giris_kayitlari { get; set; }
        public DbSet<kurumsal_kayit> kurumsal_kayit { get; set; }
        public DbSet<kurumsal_kayit_resim> kurumsal_kayit_resim { get; set; }
        public DbSet<kurumsal_yonetici_sube> kurumsal_yonetici_sube { get; set; }
        public DbSet<kurumsal_yoneticiler> kurumsal_yoneticiler { get; set; }
        public DbSet<mesaj_alici_listesi> mesaj_alici_listesi { get; set; }
        public DbSet<mesaj_ayarlari> mesaj_ayarlari { get; set; }
        public DbSet<mesajlar> mesajlars { get; set; }
        public DbSet<okul_fakulteleri> okul_fakulteleri { get; set; }
        public DbSet<okul_fiyat_ogr_tur> okul_fiyat_ogr_tur { get; set; }
        public DbSet<okul_fotograflar> okul_fotograflar { get; set; }
        public DbSet<okul_grup_iliski> okul_grup_iliski { get; set; }
        public DbSet<okul_gruplari> okul_gruplari { get; set; }
        public DbSet<okul_gunluk> okul_gunluk { get; set; }
        public DbSet<okul_indirimleri> okul_indirimleri { get; set; }
        public DbSet<okul_lokasyon> okul_lokasyon { get; set; }
        public DbSet<okul_ozellikleri> okul_ozellikleri { get; set; }
        public DbSet<okul_programlari> okul_programlari { get; set; }
        public DbSet<okul_sayac> okul_sayac { get; set; }
        public DbSet<okul_sistem> okul_sistem { get; set; }
        public DbSet<okul_ziyaret> okul_ziyaret { get; set; }
        public DbSet<okullar> okullars { get; set; }
        public DbSet<on_basvuru_alici_listesi> on_basvuru_alici_listesi { get; set; }
        public DbSet<on_kayit_basvuru> on_kayit_basvuru { get; set; }
        public DbSet<onkayit_egitim_turleri> onkayit_egitim_turleri { get; set; }
        public DbSet<ozellikler> ozelliklers { get; set; }
        public DbSet<popup> popups { get; set; }
        public DbSet<program_havuzu> program_havuzu { get; set; }
        public DbSet<sayfalar> sayfalars { get; set; }
        public DbSet<seviye_bilgilendirme> seviye_bilgilendirme { get; set; }
        public DbSet<seviye_odulleri> seviye_odulleri { get; set; }
        public DbSet<site_haritasi> site_haritasi { get; set; }
        public DbSet<soru_alici_listesi> soru_alici_listesi { get; set; }
        public DbSet<sorular> sorulars { get; set; }
        public DbSet<sosyal_medya> sosyal_medya { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<toplu_mesaj> toplu_mesaj { get; set; }
        public DbSet<toplu_mesaj_kitle> toplu_mesaj_kitle { get; set; }
        public DbSet<ulke> ulkes { get; set; }
        public DbSet<uye_dogrulama> uye_dogrulama { get; set; }
        public DbSet<uye_ilgi> uye_ilgi { get; set; }
        public DbSet<uye_mesajlari> uye_mesajlari { get; set; }
        public DbSet<uye_mesajlari_kurumsal> uye_mesajlari_kurumsal { get; set; }
        public DbSet<uye_okullari> uye_okullari { get; set; }
        public DbSet<uye_seviyeleri> uye_seviyeleri { get; set; }
        public DbSet<uyeler> uyelers { get; set; }
        public DbSet<yetkiler> yetkilers { get; set; }
        public DbSet<yoneticiler> yoneticilers { get; set; }
        public DbSet<yorum> yorums { get; set; }
        public DbSet<yorum_begeni_okul> yorum_begeni_okul { get; set; }
        public DbSet<yorum_begeniler> yorum_begeniler { get; set; }
        public DbSet<yorum_resimleri> yorum_resimleri { get; set; }
        public DbSet<yorum_sikayet_nedenleri> yorum_sikayet_nedenleri { get; set; }
        public DbSet<yorum_sikayetleri> yorum_sikayetleri { get; set; }
        public DbSet<yorum_yanitlari> yorum_yanitlari { get; set; }
        public DbSet<ziyaretci> ziyaretcis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new abonelerMap());
            modelBuilder.Configurations.Add(new anket_cevaplariMap());
            modelBuilder.Configurations.Add(new anket_cevaplayanMap());
            modelBuilder.Configurations.Add(new Anket_SorulariMap());
            modelBuilder.Configurations.Add(new AnketlerMap());
            modelBuilder.Configurations.Add(new egitim_durumlariMap());
            modelBuilder.Configurations.Add(new egitim_duzeyleriMap());
            modelBuilder.Configurations.Add(new egitim_ozellikleriMap());
            modelBuilder.Configurations.Add(new egitim_turleriMap());
            modelBuilder.Configurations.Add(new eyaletMap());
            modelBuilder.Configurations.Add(new fakulte_havuzuMap());
            modelBuilder.Configurations.Add(new fiyatMap());
            modelBuilder.Configurations.Add(new fiyat_degerMap());
            modelBuilder.Configurations.Add(new fiyat_deger_haftalikMap());
            modelBuilder.Configurations.Add(new fiyat_ogr_turMap());
            modelBuilder.Configurations.Add(new fiyat_turMap());
            modelBuilder.Configurations.Add(new genel_ayarlarMap());
            modelBuilder.Configurations.Add(new giris_kayitlariMap());
            modelBuilder.Configurations.Add(new ilMap());
            modelBuilder.Configurations.Add(new ilceMap());
            modelBuilder.Configurations.Add(new indirim_uyeMap());
            modelBuilder.Configurations.Add(new indirim_uye_bilgilendirmeMap());
            modelBuilder.Configurations.Add(new kurs_haftaMap());
            modelBuilder.Configurations.Add(new kurumsal_giris_kayitlariMap());
            modelBuilder.Configurations.Add(new kurumsal_kayitMap());
            modelBuilder.Configurations.Add(new kurumsal_kayit_resimMap());
            modelBuilder.Configurations.Add(new kurumsal_yonetici_subeMap());
            modelBuilder.Configurations.Add(new kurumsal_yoneticilerMap());
            modelBuilder.Configurations.Add(new mesaj_alici_listesiMap());
            modelBuilder.Configurations.Add(new mesaj_ayarlariMap());
            modelBuilder.Configurations.Add(new mesajlarMap());
            modelBuilder.Configurations.Add(new okul_fakulteleriMap());
            modelBuilder.Configurations.Add(new okul_fiyat_ogr_turMap());
            modelBuilder.Configurations.Add(new okul_fotograflarMap());
            modelBuilder.Configurations.Add(new okul_grup_iliskiMap());
            modelBuilder.Configurations.Add(new okul_gruplariMap());
            modelBuilder.Configurations.Add(new okul_gunlukMap());
            modelBuilder.Configurations.Add(new okul_indirimleriMap());
            modelBuilder.Configurations.Add(new okul_lokasyonMap());
            modelBuilder.Configurations.Add(new okul_ozellikleriMap());
            modelBuilder.Configurations.Add(new okul_programlariMap());
            modelBuilder.Configurations.Add(new okul_sayacMap());
            modelBuilder.Configurations.Add(new okul_sistemMap());
            modelBuilder.Configurations.Add(new okul_ziyaretMap());
            modelBuilder.Configurations.Add(new okullarMap());
            modelBuilder.Configurations.Add(new on_basvuru_alici_listesiMap());
            modelBuilder.Configurations.Add(new on_kayit_basvuruMap());
            modelBuilder.Configurations.Add(new onkayit_egitim_turleriMap());
            modelBuilder.Configurations.Add(new ozelliklerMap());
            modelBuilder.Configurations.Add(new popupMap());
            modelBuilder.Configurations.Add(new program_havuzuMap());
            modelBuilder.Configurations.Add(new sayfalarMap());
            modelBuilder.Configurations.Add(new seviye_bilgilendirmeMap());
            modelBuilder.Configurations.Add(new seviye_odulleriMap());
            modelBuilder.Configurations.Add(new site_haritasiMap());
            modelBuilder.Configurations.Add(new soru_alici_listesiMap());
            modelBuilder.Configurations.Add(new sorularMap());
            modelBuilder.Configurations.Add(new sosyal_medyaMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new toplu_mesajMap());
            modelBuilder.Configurations.Add(new toplu_mesaj_kitleMap());
            modelBuilder.Configurations.Add(new ulkeMap());
            modelBuilder.Configurations.Add(new uye_dogrulamaMap());
            modelBuilder.Configurations.Add(new uye_ilgiMap());
            modelBuilder.Configurations.Add(new uye_mesajlariMap());
            modelBuilder.Configurations.Add(new uye_mesajlari_kurumsalMap());
            modelBuilder.Configurations.Add(new uye_okullariMap());
            modelBuilder.Configurations.Add(new uye_seviyeleriMap());
            modelBuilder.Configurations.Add(new uyelerMap());
            modelBuilder.Configurations.Add(new yetkilerMap());
            modelBuilder.Configurations.Add(new yoneticilerMap());
            modelBuilder.Configurations.Add(new yorumMap());
            modelBuilder.Configurations.Add(new yorum_begeni_okulMap());
            modelBuilder.Configurations.Add(new yorum_begenilerMap());
            modelBuilder.Configurations.Add(new yorum_resimleriMap());
            modelBuilder.Configurations.Add(new yorum_sikayet_nedenleriMap());
            modelBuilder.Configurations.Add(new yorum_sikayetleriMap());
            modelBuilder.Configurations.Add(new yorum_yanitlariMap());
            modelBuilder.Configurations.Add(new ziyaretciMap());
        }
    }
}
