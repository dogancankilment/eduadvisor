using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class sosyal_medyaMap : EntityTypeConfiguration<sosyal_medya>
    {
        public sosyal_medyaMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.image_url)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.link)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("sosyal_medya");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.image_url).HasColumnName("image_url");
            this.Property(t => t.link).HasColumnName("link");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
        }
    }
}
