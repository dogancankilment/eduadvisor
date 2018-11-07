using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class ziyaretciMap : EntityTypeConfiguration<ziyaretci>
    {
        public ziyaretciMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.ip)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ziyaretci");
            this.Property(t => t.ip).HasColumnName("ip");
            this.Property(t => t.zaman).HasColumnName("zaman");
            this.Property(t => t.id).HasColumnName("id");
        }
    }
}
