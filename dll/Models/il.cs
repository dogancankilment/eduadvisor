using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class il
    {
        public il()
        {
            this.ilces = new List<ilce>();
            this.on_kayit_basvuru = new List<on_kayit_basvuru>();
            this.on_kayit_basvuru1 = new List<on_kayit_basvuru>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public int ulke_id { get; set; }
        public Nullable<int> eyalet_id { get; set; }
        public Nullable<int> sira_no { get; set; }
        public virtual ulke ulke { get; set; }
        public virtual ICollection<ilce> ilces { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru1 { get; set; }
    }
}
