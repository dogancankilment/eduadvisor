using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yetkilerMap : EntityTypeConfiguration<yetkiler>
    {
        public yetkilerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .HasMaxLength(100);

            this.Property(t => t.adi_ingilizce)
                .HasMaxLength(100);

            this.Property(t => t.menu_degeri)
                .HasMaxLength(150);

            this.Property(t => t.url)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("yetkiler");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
            this.Property(t => t.aciklama).HasColumnName("aciklama");
            this.Property(t => t.aciklama_ingilizce).HasColumnName("aciklama_ingilizce");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
            this.Property(t => t.menu_degeri).HasColumnName("menu_degeri");
            this.Property(t => t.url).HasColumnName("url");

            // Relationships
            this.HasMany(t => t.yoneticilers)
                .WithMany(t => t.yetkilers)
                .Map(m =>
                    {
                        m.ToTable("yonetici_yetkileri");
                        m.MapLeftKey("yetki_id");
                        m.MapRightKey("yonetici_id");
                    });


        }
    }
}
