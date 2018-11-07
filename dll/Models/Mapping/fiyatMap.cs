using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class fiyatMap : EntityTypeConfiguration<fiyat>
    {
        public fiyatMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("fiyat");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_program_id).HasColumnName("okul_program_id");
            this.Property(t => t.fiyat_tur_id).HasColumnName("fiyat_tur_id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");

            // Relationships
            this.HasRequired(t => t.fiyat_tur)
                .WithMany(t => t.fiyats)
                .HasForeignKey(d => d.fiyat_tur_id);
            this.HasRequired(t => t.okul_programlari)
                .WithMany(t => t.fiyats)
                .HasForeignKey(d => d.okul_program_id);
            this.HasOptional(t => t.okullar)
                .WithMany(t => t.fiyats)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
