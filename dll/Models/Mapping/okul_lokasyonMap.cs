using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_lokasyonMap : EntityTypeConfiguration<okul_lokasyon>
    {
        public okul_lokasyonMap()
        {
            // Primary Key
            this.HasKey(t => t.okul_id);

            // Properties
            this.Property(t => t.okul_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("okul_lokasyon");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.lat).HasColumnName("lat");
            this.Property(t => t.lng).HasColumnName("lng");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithOptional(t => t.okul_lokasyon);

        }
    }
}
