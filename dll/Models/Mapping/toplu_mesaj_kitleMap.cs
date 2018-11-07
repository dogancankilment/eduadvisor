using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class toplu_mesaj_kitleMap : EntityTypeConfiguration<toplu_mesaj_kitle>
    {
        public toplu_mesaj_kitleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.mesaj_id, t.egitim_id });

            // Properties
            this.Property(t => t.mesaj_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.egitim_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("toplu_mesaj_kitle");
            this.Property(t => t.mesaj_id).HasColumnName("mesaj_id");
            this.Property(t => t.egitim_id).HasColumnName("egitim_id");

            // Relationships
            this.HasRequired(t => t.toplu_mesaj)
                .WithMany(t => t.toplu_mesaj_kitle)
                .HasForeignKey(d => d.mesaj_id);

        }
    }
}
