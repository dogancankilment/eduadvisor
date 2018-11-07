using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_gruplari
    {
        public okul_gruplari()
        {
            this.okul_grup_iliski = new List<okul_grup_iliski>();
        }

        public int id { get; set; }
        public int egitim_id { get; set; }
        public string adi { get; set; }
        public string seo_url { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
        public virtual ICollection<okul_grup_iliski> okul_grup_iliski { get; set; }
    }
}
