using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class ulkeMap : EntityTypeConfiguration<ulke>
    {
        public ulkeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.adi_ing)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ulke");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
            this.Property(t => t.adi_ing).HasColumnName("adi_ing");
        }
    }
}
