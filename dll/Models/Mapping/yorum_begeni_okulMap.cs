using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class yorum_begeni_okulMap : EntityTypeConfiguration<yorum_begeni_okul>
    {
        public yorum_begeni_okulMap()
        {
            // Primary Key
            this.HasKey(t => new { t.yorum_id, t.okul_id });

            // Properties
            this.Property(t => t.yorum_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.okul_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("yorum_begeni_okul");
            this.Property(t => t.yorum_id).HasColumnName("yorum_id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");

            // Relationships
            this.HasRequired(t => t.yorum)
                .WithMany(t => t.yorum_begeni_okul)
                .HasForeignKey(d => d.yorum_id);

        }
    }
}
