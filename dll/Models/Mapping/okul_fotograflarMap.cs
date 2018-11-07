using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_fotograflarMap : EntityTypeConfiguration<okul_fotograflar>
    {
        public okul_fotograflarMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.resim_adi)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("okul_fotograflar");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.resim_adi).HasColumnName("resim_adi");
            this.Property(t => t.sira_no).HasColumnName("sira_no");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_fotograflar)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
