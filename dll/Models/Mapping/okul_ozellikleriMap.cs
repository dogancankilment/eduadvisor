using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_ozellikleriMap : EntityTypeConfiguration<okul_ozellikleri>
    {
        public okul_ozellikleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("okul_ozellikleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.egitim_ozellik_id).HasColumnName("egitim_ozellik_id");

            // Relationships
            this.HasRequired(t => t.egitim_ozellikleri)
                .WithMany(t => t.okul_ozellikleri)
                .HasForeignKey(d => d.egitim_ozellik_id);
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_ozellikleri)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
