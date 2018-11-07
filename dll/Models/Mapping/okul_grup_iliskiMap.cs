using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_grup_iliskiMap : EntityTypeConfiguration<okul_grup_iliski>
    {
        public okul_grup_iliskiMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("okul_grup_iliski");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.grup_id).HasColumnName("grup_id");

            // Relationships
            this.HasOptional(t => t.okul_gruplari)
                .WithMany(t => t.okul_grup_iliski)
                .HasForeignKey(d => d.grup_id);
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_grup_iliski)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
