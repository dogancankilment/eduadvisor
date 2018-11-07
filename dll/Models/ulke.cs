using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class ulke
    {
        public ulke()
        {
            this.eyalets = new List<eyalet>();
            this.ils = new List<il>();
            this.on_kayit_basvuru = new List<on_kayit_basvuru>();
            this.on_kayit_basvuru1 = new List<on_kayit_basvuru>();
            this.on_kayit_basvuru2 = new List<on_kayit_basvuru>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public int sira_no { get; set; }
        public string adi_ing { get; set; }
        public virtual ICollection<eyalet> eyalets { get; set; }
        public virtual ICollection<il> ils { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru1 { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru2 { get; set; }
    }
}
