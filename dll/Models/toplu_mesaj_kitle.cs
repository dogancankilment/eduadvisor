using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class toplu_mesaj_kitle
    {
        public int mesaj_id { get; set; }
        public int egitim_id { get; set; }
        public virtual toplu_mesaj toplu_mesaj { get; set; }
    }
}
