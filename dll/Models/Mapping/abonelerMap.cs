using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class abonelerMap : EntityTypeConfiguration<aboneler>
    {
        public abonelerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.mail)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("aboneler");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.mail).HasColumnName("mail");
        }
    }
}
