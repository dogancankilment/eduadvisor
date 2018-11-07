using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class giris_kayitlariMap : EntityTypeConfiguration<giris_kayitlari>
    {
        public giris_kayitlariMap()
        {
            // Primary Key
            this.HasKey(t => t.tarih);

            // Properties
            this.Property(t => t.ip_no)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("giris_kayitlari");
            this.Property(t => t.tarih).HasColumnName("tarih");
            this.Property(t => t.ip_no).HasColumnName("ip_no");
            this.Property(t => t.yonetici_id).HasColumnName("yonetici_id");
            this.Property(t => t.cikis_tarih).HasColumnName("cikis_tarih");
        }
    }
}
