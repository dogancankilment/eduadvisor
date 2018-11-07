using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class kurumsal_yonetici_subeMap : EntityTypeConfiguration<kurumsal_yonetici_sube>
    {
        public kurumsal_yonetici_subeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("kurumsal_yonetici_sube");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.yonetici_id).HasColumnName("yonetici_id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");

            // Relationships
            this.HasRequired(t => t.kurumsal_yoneticiler)
                .WithMany(t => t.kurumsal_yonetici_sube)
                .HasForeignKey(d => d.yonetici_id);
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.kurumsal_yonetici_sube)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
