using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class sayfalarMap : EntityTypeConfiguration<sayfalar>
    {
        public sayfalarMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.ad)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.ad_ingilizce)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.seo_url)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("sayfalar");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ad).HasColumnName("ad");
            this.Property(t => t.ad_ingilizce).HasColumnName("ad_ingilizce");
            this.Property(t => t.turkce).HasColumnName("turkce");
            this.Property(t => t.ingilizce).HasColumnName("ingilizce");
            this.Property(t => t.gavurca).HasColumnName("gavurca");
            this.Property(t => t.seo_url).HasColumnName("seo_url");
        }
    }
}
