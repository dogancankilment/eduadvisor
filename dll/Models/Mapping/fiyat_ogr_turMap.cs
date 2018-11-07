using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class fiyat_ogr_turMap : EntityTypeConfiguration<fiyat_ogr_tur>
    {
        public fiyat_ogr_turMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.adi_ing)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("fiyat_ogr_tur");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ing).HasColumnName("adi_ing");
        }
    }
}
