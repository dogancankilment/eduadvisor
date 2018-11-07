using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yorum_yanitlariMap : EntityTypeConfiguration<yorum_yanitlari>
    {
        public yorum_yanitlariMap()
        {
            // Primary Key
            this.HasKey(t => t.yorum_id);

            // Properties
            this.Property(t => t.yorum_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.yanit)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("yorum_yanitlari");
            this.Property(t => t.yorum_id).HasColumnName("yorum_id");
            this.Property(t => t.yanit).HasColumnName("yanit");
            this.Property(t => t.onay).HasColumnName("onay");
            this.Property(t => t.onay_tarihi).HasColumnName("onay_tarihi");
            this.Property(t => t.yanit_tarihi).HasColumnName("yanit_tarihi");

            // Relationships
            this.HasRequired(t => t.yorum)
                .WithOptional(t => t.yorum_yanitlari);

        }
    }
}
