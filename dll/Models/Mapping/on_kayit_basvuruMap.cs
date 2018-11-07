using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class on_kayit_basvuruMap : EntityTypeConfiguration<on_kayit_basvuru>
    {
        public on_kayit_basvuruMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.soyadi)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.dog_tar)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.email)
                .IsRequired()
                .HasMaxLength(145);

            this.Property(t => t.pass_no)
                .HasMaxLength(45);

            this.Property(t => t.pass_tarih)
                .HasMaxLength(10);

            this.Property(t => t.adres)
                .IsRequired()
                .HasMaxLength(155);

            this.Property(t => t.tel_ev)
                .HasMaxLength(25);

            this.Property(t => t.tel_cep)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.mezun_okul)
                .HasMaxLength(145);

            this.Property(t => t.on_kayitkodu)
                .HasMaxLength(15);

            this.Property(t => t.kurs_hafta)
                .HasMaxLength(45);

            this.Property(t => t.dil_seviye)
                .HasMaxLength(45);

            this.Property(t => t.baslayacagi_tarih)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("on_kayit_basvuru");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.soyadi).HasColumnName("soyadi");
            this.Property(t => t.cinsiyet).HasColumnName("cinsiyet");
            this.Property(t => t.uyruk).HasColumnName("uyruk");
            this.Property(t => t.dog_tar).HasColumnName("dog_tar");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.dogum_ulke).HasColumnName("dogum_ulke");
            this.Property(t => t.dogum_il).HasColumnName("dogum_il");
            this.Property(t => t.pass_no).HasColumnName("pass_no");
            this.Property(t => t.pass_tarih).HasColumnName("pass_tarih");
            this.Property(t => t.yas_ulke).HasColumnName("yas_ulke");
            this.Property(t => t.yas_il).HasColumnName("yas_il");
            this.Property(t => t.adres).HasColumnName("adres");
            this.Property(t => t.tel_ev).HasColumnName("tel_ev");
            this.Property(t => t.tel_cep).HasColumnName("tel_cep");
            this.Property(t => t.program_tur_id).HasColumnName("program_tur_id");
            this.Property(t => t.program_id).HasColumnName("program_id");
            this.Property(t => t.mezun_okul).HasColumnName("mezun_okul");
            this.Property(t => t.on_kayitkodu).HasColumnName("on_kayitkodu");
            this.Property(t => t.kurs_hafta).HasColumnName("kurs_hafta");
            this.Property(t => t.dil_seviye).HasColumnName("dil_seviye");
            this.Property(t => t.basvuru_tarihi).HasColumnName("basvuru_tarihi");
            this.Property(t => t.basvurdugu_okul).HasColumnName("basvurdugu_okul");
            this.Property(t => t.baslayacagi_tarih).HasColumnName("baslayacagi_tarih");
            this.Property(t => t.durumu).HasColumnName("durumu");
            this.Property(t => t.okundu).HasColumnName("okundu");

            // Relationships
            this.HasRequired(t => t.il)
                .WithMany(t => t.on_kayit_basvuru)
                .HasForeignKey(d => d.dogum_il);
            this.HasOptional(t => t.il1)
                .WithMany(t => t.on_kayit_basvuru1)
                .HasForeignKey(d => d.yas_il);
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.on_kayit_basvuru)
                .HasForeignKey(d => d.basvurdugu_okul);
            this.HasRequired(t => t.ulke)
                .WithMany(t => t.on_kayit_basvuru)
                .HasForeignKey(d => d.dogum_ulke);
            this.HasOptional(t => t.onkayit_egitim_turleri)
                .WithMany(t => t.on_kayit_basvuru)
                .HasForeignKey(d => d.program_tur_id);
            this.HasOptional(t => t.program_havuzu)
                .WithMany(t => t.on_kayit_basvuru)
                .HasForeignKey(d => d.program_id);
            this.HasRequired(t => t.uyeler)
                .WithMany(t => t.on_kayit_basvuru)
                .HasForeignKey(d => d.uye_id);
            this.HasRequired(t => t.ulke1)
                .WithMany(t => t.on_kayit_basvuru1)
                .HasForeignKey(d => d.uyruk);
            this.HasOptional(t => t.ulke2)
                .WithMany(t => t.on_kayit_basvuru2)
                .HasForeignKey(d => d.yas_ulke);

        }
    }
}
