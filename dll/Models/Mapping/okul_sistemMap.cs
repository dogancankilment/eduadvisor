using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_sistemMap : EntityTypeConfiguration<okul_sistem>
    {
        public okul_sistemMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.link)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.kullanici_adi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.sifre)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("okul_sistem");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.link).HasColumnName("link");
            this.Property(t => t.kullanici_adi).HasColumnName("kullanici_adi");
            this.Property(t => t.sifre).HasColumnName("sifre");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_sistem)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
