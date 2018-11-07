using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yorum_begenilerMap : EntityTypeConfiguration<yorum_begeniler>
    {
        public yorum_begenilerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("yorum_begeniler");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.yorum_id).HasColumnName("yorum_id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");

            // Relationships
            this.HasRequired(t => t.uyeler)
                .WithMany(t => t.yorum_begeniler)
                .HasForeignKey(d => d.uye_id);
            this.HasRequired(t => t.yorum)
                .WithMany(t => t.yorum_begeniler)
                .HasForeignKey(d => d.yorum_id);

        }
    }
}
