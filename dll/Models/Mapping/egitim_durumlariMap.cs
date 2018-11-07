using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class egitim_durumlariMap : EntityTypeConfiguration<egitim_durumlari>
    {
        public egitim_durumlariMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .HasMaxLength(145);

            this.Property(t => t.adi_ingilizce)
                .HasMaxLength(145);

            // Table & Column Mappings
            this.ToTable("egitim_durumlari");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
            this.Property(t => t.silindi).HasColumnName("silindi");
        }
    }
}
