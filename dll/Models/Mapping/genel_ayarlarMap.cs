using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class genel_ayarlarMap : EntityTypeConfiguration<genel_ayarlar>
    {
        public genel_ayarlarMap()
        {
            // Primary Key
            this.HasKey(t => t.firma_adi);

            // Properties
            this.Property(t => t.firma_adi)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.firma_logo)
                .HasMaxLength(256);

            this.Property(t => t.smtp_user)
                .HasMaxLength(64);

            this.Property(t => t.smtp_pass)
                .HasMaxLength(32);

            this.Property(t => t.smtp_host)
                .HasMaxLength(64);

            this.Property(t => t.smtp_mail)
                .HasMaxLength(64);

            this.Property(t => t.seo_anahtar)
                .HasMaxLength(256);

            this.Property(t => t.seo_aciklama)
                .HasMaxLength(256);

            this.Property(t => t.copyright_satiri_turkce)
                .HasMaxLength(128);

            this.Property(t => t.copyright_satiri_ingilizce)
                .HasMaxLength(128);

            this.Property(t => t.copyright_satiri_gavurca)
                .HasMaxLength(128);

            this.Property(t => t.arama_back)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("genel_ayarlar");
            this.Property(t => t.firma_adi).HasColumnName("firma_adi");
            this.Property(t => t.firma_logo).HasColumnName("firma_logo");
            this.Property(t => t.recaptcha).HasColumnName("recaptcha");
            this.Property(t => t.smtp_user).HasColumnName("smtp_user");
            this.Property(t => t.smtp_pass).HasColumnName("smtp_pass");
            this.Property(t => t.smtp_port).HasColumnName("smtp_port");
            this.Property(t => t.smtp_host).HasColumnName("smtp_host");
            this.Property(t => t.smtp_mail).HasColumnName("smtp_mail");
            this.Property(t => t.bakim_modu).HasColumnName("bakim_modu");
            this.Property(t => t.seo_anahtar).HasColumnName("seo_anahtar");
            this.Property(t => t.seo_aciklama).HasColumnName("seo_aciklama");
            this.Property(t => t.copyright_satiri_turkce).HasColumnName("copyright_satiri_turkce");
            this.Property(t => t.copyright_satiri_ingilizce).HasColumnName("copyright_satiri_ingilizce");
            this.Property(t => t.copyright_satiri_gavurca).HasColumnName("copyright_satiri_gavurca");
            this.Property(t => t.ziyaretci_sayaci_aktif).HasColumnName("ziyaretci_sayaci_aktif");
            this.Property(t => t.ilk_yorum_tur).HasColumnName("ilk_yorum_tur");
            this.Property(t => t.ilk_yorum_id).HasColumnName("ilk_yorum_id");
            this.Property(t => t.arama_back).HasColumnName("arama_back");
        }
    }
}
