using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class egitim_duzeyleriMap : EntityTypeConfiguration<egitim_duzeyleri>
    {
        public egitim_duzeyleriMap()
        {
            // Primary Key
            this.HasKey(t => t.okul_id);

            // Properties
            this.Property(t => t.okul_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("egitim_duzeyleri");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.on_lisans).HasColumnName("on_lisans");
            this.Property(t => t.lisans).HasColumnName("lisans");
            this.Property(t => t.yuksek_lisans).HasColumnName("yuksek_lisans");
            this.Property(t => t.doktora).HasColumnName("doktora");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithOptional(t => t.egitim_duzeyleri);

        }
    }
}
