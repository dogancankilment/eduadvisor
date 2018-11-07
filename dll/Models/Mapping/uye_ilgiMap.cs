using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class uye_ilgiMap : EntityTypeConfiguration<uye_ilgi>
    {
        public uye_ilgiMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("uye_ilgi");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");

            // Relationships
            this.HasRequired(t => t.egitim_turleri)
                .WithMany(t => t.uye_ilgi)
                .HasForeignKey(d => d.egitim_id);
            this.HasRequired(t => t.uyeler)
                .WithMany(t => t.uye_ilgi)
                .HasForeignKey(d => d.uye_id);

        }
    }
}
