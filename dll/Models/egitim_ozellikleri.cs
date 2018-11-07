using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class egitim_ozellikleri
    {
        public egitim_ozellikleri()
        {
            this.okul_ozellikleri = new List<okul_ozellikleri>();
        }

        public int id { get; set; }
        public int ozellik_id { get; set; }
        public int egitim_id { get; set; }
        public byte silindi { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
        public virtual ozellikler ozellikler { get; set; }
        public virtual ICollection<okul_ozellikleri> okul_ozellikleri { get; set; }
    }
}
