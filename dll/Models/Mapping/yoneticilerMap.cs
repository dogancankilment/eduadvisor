using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yoneticilerMap : EntityTypeConfiguration<yoneticiler>
    {
        public yoneticilerMap()
        {
            // Primary Key
            this.HasKey(t => t.yonetici_id);

            // Properties
            this.Property(t => t.yonetici_adi)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.yonetici_mail)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.yonetici_sifre)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.hatirlatma_sorusu)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.hatirlatma_cevabi)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.yonetici_foto)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("yoneticiler");
            this.Property(t => t.yonetici_id).HasColumnName("yonetici_id");
            this.Property(t => t.yonetici_adi).HasColumnName("yonetici_adi");
            this.Property(t => t.yonetici_mail).HasColumnName("yonetici_mail");
            this.Property(t => t.yonetici_sifre).HasColumnName("yonetici_sifre");
            this.Property(t => t.hatirlatma_sorusu).HasColumnName("hatirlatma_sorusu");
            this.Property(t => t.hatirlatma_cevabi).HasColumnName("hatirlatma_cevabi");
            this.Property(t => t.yonetici_foto).HasColumnName("yonetici_foto");
        }
    }
}
