using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class uye_seviyeleriMap : EntityTypeConfiguration<uye_seviyeleri>
    {
        public uye_seviyeleriMap()
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

            // Table & Column Mappings
            this.ToTable("uye_seviyeleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
            this.Property(t => t.puani).HasColumnName("puani");
        }
    }
}
