using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class anket_cevaplayanMap : EntityTypeConfiguration<anket_cevaplayan>
    {
        public anket_cevaplayanMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("anket_cevaplayan");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.anket_id).HasColumnName("anket_id");
            this.Property(t => t.tarih).HasColumnName("tarih");

            // Relationships
            this.HasOptional(t => t.Anketler)
                .WithMany(t => t.anket_cevaplayan)
                .HasForeignKey(d => d.anket_id);
            this.HasOptional(t => t.okullar)
                .WithMany(t => t.anket_cevaplayan)
                .HasForeignKey(d => d.okul_id);
            this.HasOptional(t => t.uyeler)
                .WithMany(t => t.anket_cevaplayan)
                .HasForeignKey(d => d.uye_id);

        }
    }
}
