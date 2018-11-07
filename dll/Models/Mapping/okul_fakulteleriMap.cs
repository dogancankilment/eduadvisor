using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_fakulteleriMap : EntityTypeConfiguration<okul_fakulteleri>
    {
        public okul_fakulteleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("okul_fakulteleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.fakulte_id).HasColumnName("fakulte_id");

            // Relationships
            this.HasRequired(t => t.fakulte_havuzu)
                .WithMany(t => t.okul_fakulteleri)
                .HasForeignKey(d => d.fakulte_id);
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_fakulteleri)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
