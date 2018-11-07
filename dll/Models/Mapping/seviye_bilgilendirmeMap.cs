using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class seviye_bilgilendirmeMap : EntityTypeConfiguration<seviye_bilgilendirme>
    {
        public seviye_bilgilendirmeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.baslik)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.baslik_ingilizce)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.icerik)
                .IsRequired();

            this.Property(t => t.icerik_ingilizce)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("seviye_bilgilendirme");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.baslik).HasColumnName("baslik");
            this.Property(t => t.baslik_ingilizce).HasColumnName("baslik_ingilizce");
            this.Property(t => t.icerik).HasColumnName("icerik");
            this.Property(t => t.icerik_ingilizce).HasColumnName("icerik_ingilizce");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
        }
    }
}
