using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class egitim_turleriMap : EntityTypeConfiguration<egitim_turleri>
    {
        public egitim_turleriMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.adi_ingilizce)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.seo_url)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.pin_adi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.harita_key_def)
                .HasMaxLength(50);

            this.Property(t => t.harita_key_ozel)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("egitim_turleri");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.adi_ingilizce).HasColumnName("adi_ingilizce");
            this.Property(t => t.silindi).HasColumnName("silindi");
            this.Property(t => t.seo_url).HasColumnName("seo_url");
            this.Property(t => t.pin_adi).HasColumnName("pin_adi");
            this.Property(t => t.harita_key_def).HasColumnName("harita_key_def");
            this.Property(t => t.harita_key_ozel).HasColumnName("harita_key_ozel");
            this.Property(t => t.program_var).HasColumnName("program_var");
            this.Property(t => t.dil_bilgisi_alinacak).HasColumnName("dil_bilgisi_alinacak");
            this.Property(t => t.kurs_hafta_alinacak).HasColumnName("kurs_hafta_alinacak");
        }
    }
}
