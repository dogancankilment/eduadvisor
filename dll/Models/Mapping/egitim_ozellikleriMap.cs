using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class egitim_ozellikleriMap : EntityTypeConfiguration<egitim_ozellikleri>
    {
        public egitim_ozellikleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("egitim_ozellikleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ozellik_id).HasColumnName("ozellik_id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");
            this.Property(t => t.silindi).HasColumnName("silindi");

            // Relationships
            this.HasRequired(t => t.egitim_turleri)
                .WithMany(t => t.egitim_ozellikleri)
                .HasForeignKey(d => d.egitim_id);
            this.HasRequired(t => t.ozellikler)
                .WithMany(t => t.egitim_ozellikleri)
                .HasForeignKey(d => d.ozellik_id);

        }
    }
}
