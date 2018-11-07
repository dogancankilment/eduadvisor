using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yorumMap : EntityTypeConfiguration<yorum>
    {
        public yorumMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.baslik)
                .HasMaxLength(100);

            this.Property(t => t.icerik)
                .HasMaxLength(300);

            this.Property(t => t.okul_bas)
                .HasMaxLength(8);

            this.Property(t => t.okul_bit)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("yorum");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.baslik).HasColumnName("baslik");
            this.Property(t => t.icerik).HasColumnName("icerik");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.tarih).HasColumnName("tarih");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.puani).HasColumnName("puani");
            this.Property(t => t.okul_bas).HasColumnName("okul_bas");
            this.Property(t => t.okul_bit).HasColumnName("okul_bit");
            this.Property(t => t.onay).HasColumnName("onay");
            this.Property(t => t.onaylanma_tarihi).HasColumnName("onaylanma_tarihi");
            this.Property(t => t.silinme_tarihi).HasColumnName("silinme_tarihi");
            this.Property(t => t.program_id).HasColumnName("program_id");
            this.Property(t => t.program_tur_id).HasColumnName("program_tur_id");
            this.Property(t => t.uye_sildi).HasColumnName("uye_sildi");
            this.Property(t => t.uye_silme_tarihi).HasColumnName("uye_silme_tarihi");

            // Relationships
            this.HasOptional(t => t.okullar)
                .WithMany(t => t.yorums)
                .HasForeignKey(d => d.okul_id);
            this.HasOptional(t => t.onkayit_egitim_turleri)
                .WithMany(t => t.yorums)
                .HasForeignKey(d => d.program_tur_id);
            this.HasOptional(t => t.program_havuzu)
                .WithMany(t => t.yorums)
                .HasForeignKey(d => d.program_id);
            this.HasOptional(t => t.uyeler)
                .WithMany(t => t.yorums)
                .HasForeignKey(d => d.uye_id);

        }
    }
}
