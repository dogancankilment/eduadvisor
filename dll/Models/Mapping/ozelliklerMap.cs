using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class ozelliklerMap : EntityTypeConfiguration<ozellikler>
    {
        public ozelliklerMap()
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
            this.ToTable("ozellikler");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
        }
    }
}
