using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class mesajlarMap : EntityTypeConfiguration<mesajlar>
    {
        public mesajlarMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.adi)
                .HasMaxLength(64);

            this.Property(t => t.mail)
                .HasMaxLength(64);

            this.Property(t => t.tel)
                .HasMaxLength(20);

            this.Property(t => t.ip)
                .HasMaxLength(15);

            this.Property(t => t.mesaj)
                .HasMaxLength(512);

            this.Property(t => t.konu)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("mesajlar");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.adi).HasColumnName("adi");
            this.Property(t => t.mail).HasColumnName("mail");
            this.Property(t => t.tel).HasColumnName("tel");
            this.Property(t => t.ip).HasColumnName("ip");
            this.Property(t => t.mesaj).HasColumnName("mesaj");
            this.Property(t => t.konu).HasColumnName("konu");
            this.Property(t => t.tarih).HasColumnName("tarih");
        }
    }
}
