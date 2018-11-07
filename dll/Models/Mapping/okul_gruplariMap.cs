using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_gruplariMap : EntityTypeConfiguration<okul_gruplari>
    {
        public okul_gruplariMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .HasMaxLength(200);

            this.Property(t => t.seo_url)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("okul_gruplari");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.seo_url).HasColumnName("seo_url");

            // Relationships
            this.HasRequired(t => t.egitim_turleri)
                .WithMany(t => t.okul_gruplari)
                .HasForeignKey(d => d.egitim_id);

        }
    }
}
