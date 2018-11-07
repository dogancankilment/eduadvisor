using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class Anket_SorulariMap : EntityTypeConfiguration<Anket_Sorulari>
    {
        public Anket_SorulariMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.soru)
                .HasMaxLength(400);

            this.Property(t => t.soru_ingilizce)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("Anket_Sorulari");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.anket_id).HasColumnName("anket_id");
            this.Property(t => t.soru).HasColumnName("soru");
            this.Property(t => t.soru_ingilizce).HasColumnName("soru_ingilizce");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
            this.Property(t => t.silindi).HasColumnName("silindi");

            // Relationships
            this.HasOptional(t => t.Anketler)
                .WithMany(t => t.Anket_Sorulari)
                .HasForeignKey(d => d.anket_id);

        }
    }
}
