using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class AnketlerMap : EntityTypeConfiguration<Anketler>
    {
        public AnketlerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.adi_ingilizce)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("Anketler");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
            this.Property(t => t.aktif).HasColumnName("aktif");
            this.Property(t => t.silindi).HasColumnName("silindi");

            // Relationships
            this.HasRequired(t => t.egitim_turleri)
                .WithMany(t => t.Anketlers)
                .HasForeignKey(d => d.egitim_id);

        }
    }
}
