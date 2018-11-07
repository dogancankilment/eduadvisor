using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class sorularMap : EntityTypeConfiguration<sorular>
    {
        public sorularMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.soru)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.soru_ingilizce)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.cevap)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.cevap_ingilizce)
                .IsRequired()
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("sorular");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.soru).HasColumnName("soru");
            this.Property(t => t.soru_ingilizce).HasColumnName("soru_ingilizce");
            this.Property(t => t.cevap).HasColumnName("cevap");
            this.Property(t => t.cevap_ingilizce).HasColumnName("cevap_ingilizce");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
        }
    }
}
