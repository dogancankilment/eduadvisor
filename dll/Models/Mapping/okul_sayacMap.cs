using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_sayacMap : EntityTypeConfiguration<okul_sayac>
    {
        public okul_sayacMap()
        {
            // Primary Key
            this.HasKey(t => t.okul_id);

            // Properties
            this.Property(t => t.okul_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("okul_sayac");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.sayac).HasColumnName("sayac");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithOptional(t => t.okul_sayac);

        }
    }
}
