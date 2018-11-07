using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class indirim_uyeMap : EntityTypeConfiguration<indirim_uye>
    {
        public indirim_uyeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("indirim_uye");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.mesaj_id).HasColumnName("mesaj_id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.tarih).HasColumnName("tarih");
            this.Property(t => t.durumu).HasColumnName("durumu");

            // Relationships
            this.HasOptional(t => t.toplu_mesaj)
                .WithMany(t => t.indirim_uye)
                .HasForeignKey(d => d.mesaj_id);
            this.HasOptional(t => t.uyeler)
                .WithMany(t => t.indirim_uye)
                .HasForeignKey(d => d.uye_id);

        }
    }
}
