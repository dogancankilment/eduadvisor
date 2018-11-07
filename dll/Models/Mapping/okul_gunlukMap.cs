using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class okul_gunlukMap : EntityTypeConfiguration<okul_gunluk>
    {
        public okul_gunlukMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ip, t.zaman, t.okul_id, t.gunluk_id });

            // Properties
            this.Property(t => t.ip)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.okul_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.gunluk_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("okul_gunluk");
            this.Property(t => t.ip).HasColumnName("ip");
            this.Property(t => t.zaman).HasColumnName("zaman");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.gunluk_id).HasColumnName("gunluk_id");

            // Relationships
            this.HasRequired(t => t.okullar)
                .WithMany(t => t.okul_gunluk)
                .HasForeignKey(d => d.okul_id);

        }
    }
}
