using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class onkayit_egitim_turleriMap : EntityTypeConfiguration<onkayit_egitim_turleri>
    {
        public onkayit_egitim_turleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(75);

            this.Property(t => t.adi_ingilizce)
                .HasMaxLength(75);

            // Table & Column Mappings
            this.ToTable("onkayit_egitim_turleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
            this.Property(t => t.sira_no).HasColumnName("sira_no");
            this.Property(t => t.silindi).HasColumnName("silindi");

            // Relationships
            this.HasRequired(t => t.egitim_turleri)
                .WithMany(t => t.onkayit_egitim_turleri)
                .HasForeignKey(d => d.egitim_id);

        }
    }
}
