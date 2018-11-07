using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_programlariMap : EntityTypeConfiguration<okul_programlari>
    {
        public okul_programlariMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("okul_programlari");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.program_id).HasColumnName("program_id");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_programlari)
                .HasForeignKey(d => d.okul_id);
            this.HasRequired(t => t.program_havuzu)
                .WithMany(t => t.okul_programlari)
                .HasForeignKey(d => d.program_id);

        }
    }
}
