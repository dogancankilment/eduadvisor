using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yorum_sikayet_nedenleriMap : EntityTypeConfiguration<yorum_sikayet_nedenleri>
    {
        public yorum_sikayet_nedenleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.adi_ingilizce)
                .IsRequired()
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("yorum_sikayet_nedenleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
        }
    }
}
