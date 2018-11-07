using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class indirim_uye_bilgilendirmeMap : EntityTypeConfiguration<indirim_uye_bilgilendirme>
    {
        public indirim_uye_bilgilendirmeMap()
        {
            // Primary Key
            this.HasKey(t => t.indirim_uye_id);

            // Properties
            this.Property(t => t.indirim_uye_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("indirim_uye_bilgilendirme");
            this.Property(t => t.indirim_uye_id).HasColumnName("indirim_uye_id");
            this.Property(t => t.durumu).HasColumnName("durumu");
            this.Property(t => t.tarih).HasColumnName("tarih");

            // Relationships
            this.HasRequired(t => t.indirim_uye)
                .WithOptional(t => t.indirim_uye_bilgilendirme);

        }
    }
}
