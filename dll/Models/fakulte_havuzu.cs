using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class fakulte_havuzu
    {
        public fakulte_havuzu()
        {
            this.okul_fakulteleri = new List<okul_fakulteleri>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public virtual ICollection<okul_fakulteleri> okul_fakulteleri { get; set; }
    }
}
