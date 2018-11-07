using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class kurs_haftaMap : EntityTypeConfiguration<kurs_hafta>
    {
        public kurs_haftaMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.adi_ing)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("kurs_hafta");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ing).HasColumnName("adi_ing");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
            this.Property(t => t.hafta_degeri).HasColumnName("hafta_degeri");
        }
    }
}
