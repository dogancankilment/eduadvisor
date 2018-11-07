using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class uye_okullariMap : EntityTypeConfiguration<uye_okullari>
    {
        public uye_okullariMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.baslangic)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.bitis)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("uye_okullari");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.program_id).HasColumnName("program_id");
            this.Property(t => t.program_tur_id).HasColumnName("program_tur_id");
            this.Property(t => t.baslangic).HasColumnName("baslangic");
            this.Property(t => t.bitis).HasColumnName("bitis");
            this.Property(t => t.eklenme_tarihi).HasColumnName("eklenme_tarihi");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.uye_okullari)
                .HasForeignKey(d => d.okul_id);
            this.HasRequired(t => t.onkayit_egitim_turleri)
                .WithMany(t => t.uye_okullari)
                .HasForeignKey(d => d.program_tur_id);
            this.HasRequired(t => t.program_havuzu)
                .WithMany(t => t.uye_okullari)
                .HasForeignKey(d => d.program_id);
            this.HasRequired(t => t.uyeler)
                .WithMany(t => t.uye_okullari)
                .HasForeignKey(d => d.uye_id);

        }
    }
}
