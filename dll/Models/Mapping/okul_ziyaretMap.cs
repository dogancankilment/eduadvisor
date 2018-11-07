using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_ziyaretMap : EntityTypeConfiguration<okul_ziyaret>
    {
        public okul_ziyaretMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("okul_ziyaret");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.tarih).HasColumnName("tarih");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_ziyaret)
                .HasForeignKey(d => d.okul_id);
            this.HasRequired(t => t.uyeler)
                .WithMany(t => t.okul_ziyaret)
                .HasForeignKey(d => d.uye_id);

        }
    }
}
