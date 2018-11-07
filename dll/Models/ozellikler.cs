using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class ozellikler
    {
        public ozellikler()
        {
            this.egitim_ozellikleri = new List<egitim_ozellikleri>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public virtual ICollection<egitim_ozellikleri> egitim_ozellikleri { get; set; }
    }
}
