using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class uye_mesajlariMap : EntityTypeConfiguration<uye_mesajlari>
    {
        public uye_mesajlariMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("uye_mesajlari");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.icerik).HasColumnName("icerik");
            this.Property(t => t.tarih).HasColumnName("tarih");
            this.Property(t => t.okundu).HasColumnName("okundu");
            this.Property(t => t.alici).HasColumnName("alici");
            this.Property(t => t.turu).HasColumnName("turu");
        }
    }
}
