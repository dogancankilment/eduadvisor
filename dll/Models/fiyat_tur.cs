using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class fiyat_tur
    {
        public fiyat_tur()
        {
            this.fiyats = new List<fiyat>();
        }

        public int id { get; set; }
        public int egitim_id { get; set; }
        public string adi { get; set; }
        public string adi_ing { get; set; }
        public Nullable<int> sira_no { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
        public virtual ICollection<fiyat> fiyats { get; set; }
    }
}
