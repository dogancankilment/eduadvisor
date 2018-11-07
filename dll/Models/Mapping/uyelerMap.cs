using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class uyelerMap : EntityTypeConfiguration<uyeler>
    {
        public uyelerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.soyadi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.fotograf)
                .IsRequired()
                .HasMaxLength(70);

            this.Property(t => t.mail)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.yas)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.sifre)
                .HasMaxLength(50);

            this.Property(t => t.biyografi)
                .HasMaxLength(200);

            this.Property(t => t.tel_no)
                .HasMaxLength(25);

            this.Property(t => t.hesap_dondur)
                .HasMaxLength(300);

            this.Property(t => t.uye_adres)
                .HasMaxLength(100);

            this.Property(t => t.tel_bolge_kodu)
                .HasMaxLength(15);

            this.Property(t => t.tel_ulke_kodu)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("uyeler");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.soyadi).HasColumnName("soyadi");
            this.Property(t => t.fotograf).HasColumnName("fotograf");
            this.Property(t => t.mail).HasColumnName("mail");
            this.Property(t => t.Cinsiyet).HasColumnName("Cinsiyet");
            this.Property(t => t.il_id).HasColumnName("il_id");
            this.Property(t => t.ilce_id).HasColumnName("ilce_id");
            this.Property(t => t.ulke_id).HasColumnName("ulke_id");
            this.Property(t => t.yas).HasColumnName("yas");
            this.Property(t => t.tarih).HasColumnName("tarih");
            this.Property(t => t.haber_bulten).HasColumnName("haber_bulten");
            this.Property(t => t.sifre).HasColumnName("sifre");
            this.Property(t => t.kayit_tur).HasColumnName("kayit_tur");
            this.Property(t => t.biyografi).HasColumnName("biyografi");
            this.Property(t => t.tel_no).HasColumnName("tel_no");
            this.Property(t => t.puani).HasColumnName("puani");
            this.Property(t => t.hesap_dondur).HasColumnName("hesap_dondur");
            this.Property(t => t.hesap_aktiflik).HasColumnName("hesap_aktiflik");
            this.Property(t => t.dondurma_tarihi).HasColumnName("dondurma_tarihi");
            this.Property(t => t.kara_liste).HasColumnName("kara_liste");
            this.Property(t => t.kara_liste_tarih).HasColumnName("kara_liste_tarih");
            this.Property(t => t.silindi).HasColumnName("silindi");
            this.Property(t => t.silinme_tarihi).HasColumnName("silinme_tarihi");
            this.Property(t => t.uye_adres).HasColumnName("uye_adres");
            this.Property(t => t.tel_bolge_kodu).HasColumnName("tel_bolge_kodu");
            this.Property(t => t.tel_ulke_kodu).HasColumnName("tel_ulke_kodu");
        }
    }
}
