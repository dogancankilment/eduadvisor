using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class on_basvuru_alici_listesiMap : EntityTypeConfiguration<on_basvuru_alici_listesi>
    {
        public on_basvuru_alici_listesiMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .IsRequired()
                .HasMaxLength(70);

            this.Property(t => t.mail)
                .IsRequired()
                .HasMaxLength(70);

            // Table & Column Mappings
            this.ToTable("on_basvuru_alici_listesi");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.mail).HasColumnName("mail");
        }
    }
}
