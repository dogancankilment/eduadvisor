using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class site_haritasiMap : EntityTypeConfiguration<site_haritasi>
    {
        public site_haritasiMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(145);

            this.Property(t => t.adi_ingilizce)
                .IsRequired()
                .HasMaxLength(145);

            this.Property(t => t.link)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("site_haritasi");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
            this.Property(t => t.link).HasColumnName("link");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
        }
    }
}
