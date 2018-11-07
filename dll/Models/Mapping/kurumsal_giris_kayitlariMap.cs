using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class kurumsal_giris_kayitlariMap : EntityTypeConfiguration<kurumsal_giris_kayitlari>
    {
        public kurumsal_giris_kayitlariMap()
        {
            // Primary Key
            this.HasKey(t => t.tarih);

            // Properties
            this.Property(t => t.ip_no)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("kurumsal_giris_kayitlari");
            this.Property(t => t.tarih).HasColumnName("tarih");
            this.Property(t => t.ip_no).HasColumnName("ip_no");
            this.Property(t => t.id).HasColumnName("id");
        }
    }
}
