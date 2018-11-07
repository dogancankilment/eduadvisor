using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class kurumsal_kayit_resimMap : EntityTypeConfiguration<kurumsal_kayit_resim>
    {
        public kurumsal_kayit_resimMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.resim_adi)
                .HasMaxLength(75);

            // Table & Column Mappings
            this.ToTable("kurumsal_kayit_resim");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.yorum_kurumsal_id).HasColumnName("yorum_kurumsal_id");
            this.Property(t => t.resim_adi).HasColumnName("resim_adi");

            // Relationships
            this.HasOptional(t => t.kurumsal_kayit)
                .WithMany(t => t.kurumsal_kayit_resim)
                .HasForeignKey(d => d.yorum_kurumsal_id);

        }
    }
}
