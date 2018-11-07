using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class kurumsal_kayit
    {
        public kurumsal_kayit()
        {
            this.kurumsal_kayit_resim = new List<kurumsal_kayit_resim>();
        }

        public int id { get; set; }
        public Nullable<int> uye_id { get; set; }
        public Nullable<int> egitim_id { get; set; }
        public string okul_adi { get; set; }
        public string web_site { get; set; }
        public string ulke_adi { get; set; }
        public string il_adi { get; set; }
        public Nullable<int> yildiz_sayisi { get; set; }
        public string yorum_baslik { get; set; }
        public string yorum_icerik { get; set; }
        public string baslangic { get; set; }
        public string bitis { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public Nullable<int> tur { get; set; }
        public string skype { get; set; }
        public string email { get; set; }
        public string telefon { get; set; }
        public string aciklama { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string fakulte_adi { get; set; }
        public string grupadi { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
        public virtual ICollection<kurumsal_kayit_resim> kurumsal_kayit_resim { get; set; }
    }
}
