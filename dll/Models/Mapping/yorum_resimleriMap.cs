using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yorum_resimleriMap : EntityTypeConfiguration<yorum_resimleri>
    {
        public yorum_resimleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.resim_adi)
                .HasMaxLength(75);

            // Table & Column Mappings
            this.ToTable("yorum_resimleri");
            this.Property(t => t.yorum_id).HasColumnName("yorum_id");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.resim_adi).HasColumnName("resim_adi");

            // Relationships
            this.HasOptional(t => t.yorum)
                .WithMany(t => t.yorum_resimleri)
                .HasForeignKey(d => d.yorum_id);

        }
    }
}
