using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class program_havuzuMap : EntityTypeConfiguration<program_havuzu>
    {
        public program_havuzuMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.adi_ingilizce)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("program_havuzu");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");

            // Relationships
            this.HasRequired(t => t.egitim_turleri)
                .WithMany(t => t.program_havuzu)
                .HasForeignKey(d => d.egitim_id);

        }
    }
}
