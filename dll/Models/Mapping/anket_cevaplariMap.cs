using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class anket_cevaplariMap : EntityTypeConfiguration<anket_cevaplari>
    {
        public anket_cevaplariMap()
        {
            // Primary Key
            this.HasKey(t => t.anket_cevap_id);

            // Properties
            // Table & Column Mappings
            this.ToTable("anket_cevaplari");
            this.Property(t => t.soru_id).HasColumnName("soru_id");
            this.Property(t => t.cevap_id).HasColumnName("cevap_id");
            this.Property(t => t.degeri).HasColumnName("degeri");
            this.Property(t => t.anket_cevap_id).HasColumnName("anket_cevap_id");

            // Relationships
            this.HasOptional(t => t.anket_cevaplayan)
                .WithMany(t => t.anket_cevaplari)
                .HasForeignKey(d => d.cevap_id);
            this.HasOptional(t => t.Anket_Sorulari)
                .WithMany(t => t.anket_cevaplari)
                .HasForeignKey(d => d.soru_id);

        }
    }
}
