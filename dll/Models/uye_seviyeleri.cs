using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class uye_seviyeleri
    {
        public uye_seviyeleri()
        {
            this.seviye_odulleri = new List<seviye_odulleri>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public int puani { get; set; }
        public virtual ICollection<seviye_odulleri> seviye_odulleri { get; set; }
    }
}
