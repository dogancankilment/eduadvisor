using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class mesaj_alici_listesiMap : EntityTypeConfiguration<mesaj_alici_listesi>
    {
        public mesaj_alici_listesiMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.alici_adi)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.alici_mail)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("mesaj_alici_listesi");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.alici_adi).HasColumnName("alici_adi");
            this.Property(t => t.alici_mail).HasColumnName("alici_mail");
        }
    }
}
