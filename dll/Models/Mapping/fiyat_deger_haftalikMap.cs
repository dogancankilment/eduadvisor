using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class fiyat_deger_haftalikMap : EntityTypeConfiguration<fiyat_deger_haftalik>
    {
        public fiyat_deger_haftalikMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("fiyat_deger_haftalik");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.fiyat_id).HasColumnName("fiyat_id");
            this.Property(t => t.fiyat_ogr_tur_id).HasColumnName("fiyat_ogr_tur_id");
            this.Property(t => t.C1_hafta).HasColumnName("1_hafta");
            this.Property(t => t.C2_hafta).HasColumnName("2_hafta");
            this.Property(t => t.C3_hafta).HasColumnName("3_hafta");
            this.Property(t => t.C4_hafta).HasColumnName("4_hafta");
            this.Property(t => t.C6_hafta).HasColumnName("6_hafta");
            this.Property(t => t.C8_hafta).HasColumnName("8_hafta");
            this.Property(t => t.C10_hafta).HasColumnName("10_hafta");
            this.Property(t => t.C12_hafta).HasColumnName("12_hafta");
            this.Property(t => t.C24_hafta).HasColumnName("24_hafta");
            this.Property(t => t.C36_hafta).HasColumnName("36_hafta");

            // Relationships
            this.HasRequired(t => t.fiyat)
                .WithMany(t => t.fiyat_deger_haftalik)
                .HasForeignKey(d => d.fiyat_id);
            this.HasRequired(t => t.fiyat_ogr_tur)
                .WithMany(t => t.fiyat_deger_haftalik)
                .HasForeignKey(d => d.fiyat_ogr_tur_id);

        }
    }
}
