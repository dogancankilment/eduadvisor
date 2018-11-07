using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class fiyat_turMap : EntityTypeConfiguration<fiyat_tur>
    {
        public fiyat_turMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.adi_ing)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("fiyat_tur");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ing).HasColumnName("adi_ing");
            this.Property(t => t.sira_no).HasColumnName("sira_no");

            // Relationships
            this.HasRequired(t => t.egitim_turleri)
                .WithMany(t => t.fiyat_tur)
                .HasForeignKey(d => d.egitim_id);

        }
    }
}
