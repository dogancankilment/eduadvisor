using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class ilMap : EntityTypeConfiguration<il>
    {
        public ilMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("il");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.ulke_id).HasColumnName("ulke_id");
            this.Property(t => t.eyalet_id).HasColumnName("eyalet_id");
            this.Property(t => t.sira_no).HasColumnName("sira_no");

            // Relationships
            this.HasRequired(t => t.ulke)
                .WithMany(t => t.ils)
                .HasForeignKey(d => d.ulke_id);

        }
    }
}
