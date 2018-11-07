using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class fiyat_degerMap : EntityTypeConfiguration<fiyat_deger>
    {
        public fiyat_degerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("fiyat_deger");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.fiyat_id).HasColumnName("fiyat_id");
            this.Property(t => t.fiyat_ogr_tur_id).HasColumnName("fiyat_ogr_tur_id");
            this.Property(t => t.deger).HasColumnName("deger");

            // Relationships
            this.HasRequired(t => t.fiyat)
                .WithMany(t => t.fiyat_deger)
                .HasForeignKey(d => d.fiyat_id);
            this.HasRequired(t => t.fiyat_ogr_tur)
                .WithMany(t => t.fiyat_deger)
                .HasForeignKey(d => d.fiyat_ogr_tur_id);

        }
    }
}
