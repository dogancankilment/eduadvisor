using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class seviye_odulleriMap : EntityTypeConfiguration<seviye_odulleri>
    {
        public seviye_odulleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.baslik)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.baslik_ingilizce)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.aciklama)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.aciklama_ingilizce)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("seviye_odulleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.seviye_id).HasColumnName("seviye_id");
            this.Property(t => t.baslik).HasColumnName("baslik");
            this.Property(t => t.baslik_ingilizce).HasColumnName("baslik_ingilizce");
            this.Property(t => t.aciklama).HasColumnName("aciklama");
            this.Property(t => t.aciklama_ingilizce).HasColumnName("aciklama_ingilizce");

            // Relationships
            this.HasRequired(t => t.uye_seviyeleri)
                .WithMany(t => t.seviye_odulleri)
                .HasForeignKey(d => d.seviye_id);

        }
    }
}
