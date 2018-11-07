using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class uye_dogrulamaMap : EntityTypeConfiguration<uye_dogrulama>
    {
        public uye_dogrulamaMap()
        {
            // Primary Key
            this.HasKey(t => t.uye_id);

            // Properties
            this.Property(t => t.uye_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.dogrulama_kodu)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("uye_dogrulama");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.dogrulama_kodu).HasColumnName("dogrulama_kodu");
            this.Property(t => t.onay).HasColumnName("onay");
            this.Property(t => t.dogrulama_tarihi).HasColumnName("dogrulama_tarihi");

            // Relationships
            this.HasRequired(t => t.uyeler)
                .WithOptional(t => t.uye_dogrulama);

        }
    }
}
