using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yorum_sikayetleriMap : EntityTypeConfiguration<yorum_sikayetleri>
    {
        public yorum_sikayetleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.nedeni)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("yorum_sikayetleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.yorum_id).HasColumnName("yorum_id");
            this.Property(t => t.uye_id).HasColumnName("uye_id");
            this.Property(t => t.tarih).HasColumnName("tarih");
            this.Property(t => t.nedeni).HasColumnName("nedeni");
            this.Property(t => t.incelendi).HasColumnName("incelendi");
            this.Property(t => t.incelenme_tarihi).HasColumnName("incelenme_tarihi");

            // Relationships
            this.HasOptional(t => t.uyeler)
                .WithMany(t => t.yorum_sikayetleri)
                .HasForeignKey(d => d.uye_id);
            this.HasOptional(t => t.yorum)
                .WithMany(t => t.yorum_sikayetleri)
                .HasForeignKey(d => d.yorum_id);

        }
    }
}
