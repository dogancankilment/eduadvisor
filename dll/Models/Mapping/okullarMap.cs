using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okullarMap : EntityTypeConfiguration<okullar>
    {
        public okullarMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .HasMaxLength(200);

            this.Property(t => t.kurulus_tarihi)
                .HasMaxLength(15);

            this.Property(t => t.logo)
                .IsRequired()
                .HasMaxLength(70);

            this.Property(t => t.adres)
                .HasMaxLength(200);

            this.Property(t => t.mobil_tel)
                .HasMaxLength(25);

            this.Property(t => t.okul_email)
                .HasMaxLength(100);

            this.Property(t => t.tel1)
                .HasMaxLength(30);

            this.Property(t => t.tel2)
                .HasMaxLength(30);

            this.Property(t => t.fax)
                .HasMaxLength(30);

            this.Property(t => t.web_site)
                .HasMaxLength(80);

            this.Property(t => t.skype_id)
                .HasMaxLength(50);

            this.Property(t => t.fiyat_link)
                .HasMaxLength(300);

            this.Property(t => t.seo_url)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("okullar");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");
            this.Property(t => t.kurulus_tarihi).HasColumnName("kurulus_tarihi");
            this.Property(t => t.logo).HasColumnName("logo");
            this.Property(t => t.ulke_id).HasColumnName("ulke_id");
            this.Property(t => t.sehir_id).HasColumnName("sehir_id");
            this.Property(t => t.ilce_id).HasColumnName("ilce_id");
            this.Property(t => t.adres).HasColumnName("adres");
            this.Property(t => t.mobil_tel).HasColumnName("mobil_tel");
            this.Property(t => t.okul_email).HasColumnName("okul_email");
            this.Property(t => t.tel1).HasColumnName("tel1");
            this.Property(t => t.tel2).HasColumnName("tel2");
            this.Property(t => t.fax).HasColumnName("fax");
            this.Property(t => t.web_site).HasColumnName("web_site");
            this.Property(t => t.skype_id).HasColumnName("skype_id");
            this.Property(t => t.fiyat_link).HasColumnName("fiyat_link");
            this.Property(t => t.aciklama).HasColumnName("aciklama");
            this.Property(t => t.yayinda).HasColumnName("yayinda");
            this.Property(t => t.merkez_id).HasColumnName("merkez_id");
            this.Property(t => t.okul_kayit_tarihi).HasColumnName("okul_kayit_tarihi");
            this.Property(t => t.seo_url).HasColumnName("seo_url");
            this.Property(t => t.arsivde).HasColumnName("arsivde");

            // Relationships
            this.HasRequired(t => t.egitim_turleri)
                .WithMany(t => t.okullars)
                .HasForeignKey(d => d.egitim_id);

        }
    }
}
