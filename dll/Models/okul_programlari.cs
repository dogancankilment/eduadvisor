using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_programlari
    {
        public okul_programlari()
        {
            this.fiyats = new List<fiyat>();
        }

        public int id { get; set; }
        public int okul_id { get; set; }
        public int program_id { get; set; }
        public virtual ICollection<fiyat> fiyats { get; set; }
        public virtual okullar okullar { get; set; }
        public virtual program_havuzu program_havuzu { get; set; }
    }
}
