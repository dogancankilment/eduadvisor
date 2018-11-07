using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class kurumsal_kayitMap : EntityTypeConfiguration<kurumsal_kayit>
    {
        public kurumsal_kayitMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.okul_adi)
                .HasMaxLength(345);

            this.Property(t => t.web_site)
                .HasMaxLength(345);

            this.Property(t => t.ulke_adi)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.il_adi)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.yorum_baslik)
                .HasMaxLength(145);

            this.Property(t => t.yorum_icerik)
                .HasMaxLength(345);

            this.Property(t => t.baslangic)
                .HasMaxLength(8);

            this.Property(t => t.bitis)
                .HasMaxLength(8);

            this.Property(t => t.skype)
                .HasMaxLength(50);

            this.Property(t => t.email)
                .HasMaxLength(85);

            this.Property(t => t.telefon)
                .HasMaxLength(45);

            this.Property(t => t.aciklama)
                .HasMaxLength(200);

            this.Property(t => t.adi)
                .HasMaxLength(100);

            this.Property(t => t.soyadi)
                .HasMaxLength(200);

            this.Property(t => t.fakulte_adi)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.grupadi)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("kurumsal_kayit");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");
            this.Property(t => t.okul_adi).HasColumnName("okul_adi");
            this.Property(t => t.web_site).HasColumnName("web_site");
            this.Property(t => t.ulke_adi).HasColumnName("ulke_adi");
            this.Property(t => t.il_adi).HasColumnName("il_adi");
            this.Property(t => t.yildiz_sayisi).HasColumnName("yildiz_sayisi");
            this.Property(t => t.yorum_baslik).HasColumnName("yorum_baslik");
            this.Property(t => t.yorum_icerik).HasColumnName("yorum_icerik");
            this.Property(t => t.baslangic).HasColumnName("baslangic");
            this.Property(t => t.bitis).HasColumnName("bitis");
            this.Property(t => t.tarih).HasColumnName("tarih");
            this.Property(t => t.tur).HasColumnName("tur");
            this.Property(t => t.skype).HasColumnName("skype");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.telefon).HasColumnName("telefon");
            this.Property(t => t.aciklama).HasColumnName("aciklama");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.soyadi).HasColumnName("soyadi");
            this.Property(t => t.fakulte_adi).HasColumnName("fakulte_adi");
            this.Property(t => t.grupadi).HasColumnName("grupadi");

            // Relationships
            this.HasOptional(t => t.egitim_turleri)
                .WithMany(t => t.kurumsal_kayit)
                .HasForeignKey(d => d.egitim_id);

        }
    }
}
