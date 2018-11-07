using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_fiyat_ogr_turMap : EntityTypeConfiguration<okul_fiyat_ogr_tur>
    {
        public okul_fiyat_ogr_turMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("okul_fiyat_ogr_tur");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.fiyat_ogr_tur_id).HasColumnName("fiyat_ogr_tur_id");

            // Relationships
            this.HasRequired(t => t.fiyat_ogr_tur)
                .WithMany(t => t.okul_fiyat_ogr_tur)
                .HasForeignKey(d => d.fiyat_ogr_tur_id);
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_fiyat_ogr_tur)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
