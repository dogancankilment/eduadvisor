using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class fakulte_havuzuMap : EntityTypeConfiguration<fakulte_havuzu>
    {
        public fakulte_havuzuMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(120);

            this.Property(t => t.adi_ingilizce)
                .IsRequired()
                .HasMaxLength(120);

            // Table & Column Mappings
            this.ToTable("fakulte_havuzu");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
        }
    }
}
