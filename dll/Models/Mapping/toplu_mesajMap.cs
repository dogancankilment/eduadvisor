using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dll.Models.Mapping
{
    public class toplu_mesajMap : EntityTypeConfiguration<toplu_mesaj>
    {
        public toplu_mesajMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.baslik)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.icerik)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("toplu_mesaj");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.okul_id).HasColumnName("okul_id");
            this.Property(t => t.baslik).HasColumnName("baslik");
            this.Property(t => t.icerik).HasColumnName("icerik");
            this.Property(t => t.bas_tarih).HasColumnName("bas_tarih");
            this.Property(t => t.son_tarih).HasColumnName("son_tarih");
            this.Property(t => t.olusturulma).HasColumnName("olusturulma");
            this.Property(t => t.silindi).HasColumnName("silindi");
            this.Property(t => t.seviye_mi).HasColumnName("seviye_mi");
        }
    }
}
