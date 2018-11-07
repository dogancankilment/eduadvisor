using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class ilceMap : EntityTypeConfiguration<ilce>
    {
        public ilceMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ilce");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.il_id).HasColumnName("il_id");

            // Relationships
            this.HasRequired(t => t.il)
                .WithMany(t => t.ilces)
                .HasForeignKey(d => d.il_id);

        }
    }
}
