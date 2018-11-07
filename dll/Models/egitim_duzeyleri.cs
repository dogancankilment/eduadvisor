using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class egitim_duzeyleri
    {
        public int okul_id { get; set; }
        public Nullable<byte> on_lisans { get; set; }
        public Nullable<byte> lisans { get; set; }
        public Nullable<byte> yuksek_lisans { get; set; }
        public Nullable<byte> doktora { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
