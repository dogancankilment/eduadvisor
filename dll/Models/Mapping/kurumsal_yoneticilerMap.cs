using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class kurumsal_yoneticilerMap : EntityTypeConfiguration<kurumsal_yoneticiler>
    {
        public kurumsal_yoneticilerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .HasMaxLength(200);

            this.Property(t => t.mail)
                .HasMaxLength(70);

            this.Property(t => t.sifre)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("kurumsal_yoneticiler");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.mail).HasColumnName("mail");
            this.Property(t => t.sifre).HasColumnName("sifre");
            this.Property(t => t.okul_id).HasColumnName("okul_id");

            // Relationships
            this.HasOptional(t => t.okullar)
                .WithMany(t => t.kurumsal_yoneticiler)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
