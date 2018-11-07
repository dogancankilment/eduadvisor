using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class uye_mesajlari_kurumsalMap : EntityTypeConfiguration<uye_mesajlari_kurumsal>
    {
        public uye_mesajlari_kurumsalMap()
        {
            // Primary Key
            this.HasKey(t => t.mesaj_id);

            // Properties
            this.Property(t => t.mesaj_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("uye_mesajlari_kurumsal");
            this.Property(t => t.mesaj_id).HasColumnName("mesaj_id");
            this.Property(t => t.yonetici_id).HasColumnName("yonetici_id");
        }
    }
}
