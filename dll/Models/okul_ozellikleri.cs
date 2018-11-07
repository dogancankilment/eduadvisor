using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_ozellikleri
    {
        public int id { get; set; }
        public int okul_id { get; set; }
        public int egitim_ozellik_id { get; set; }
        public virtual egitim_ozellikleri egitim_ozellikleri { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
