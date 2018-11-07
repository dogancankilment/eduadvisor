using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class popupMap : EntityTypeConfiguration<popup>
    {
        public popupMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.resim)
                .IsRequired();

            this.Property(t => t.link)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("popup");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.resim).HasColumnName("resim");
            this.Property(t => t.aktif).HasColumnName("aktif");
            this.Property(t => t.link).HasColumnName("link");
        }
    }
}
