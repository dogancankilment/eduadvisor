using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class mesaj_ayarlariMap : EntityTypeConfiguration<mesaj_ayarlari>
    {
        public mesaj_ayarlariMap()
        {
            // Primary Key
            this.HasKey(t => t.mesaj_ayar_id);

            // Properties
            this.Property(t => t.mesaj_ayar_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("mesaj_ayarlari");
            this.Property(t => t.mail_gonder).HasColumnName("mail_gonder");
            this.Property(t => t.mesaj_kaydet).HasColumnName("mesaj_kaydet");
            this.Property(t => t.mesaj_ayar_id).HasColumnName("mesaj_ayar_id");
        }
    }
}
