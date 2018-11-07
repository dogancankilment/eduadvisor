using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_indirimleriMap : EntityTypeConfiguration<okul_indirimleri>
    {
        public okul_indirimleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("okul_indirimleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.orani).HasColumnName("orani");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_indirimleri)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
