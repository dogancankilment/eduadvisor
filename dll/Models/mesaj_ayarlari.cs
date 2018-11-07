using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class mesaj_ayarlari
    {
        public Nullable<byte> mail_gonder { get; set; }
        public Nullable<byte> mesaj_kaydet { get; set; }
        public int mesaj_ayar_id { get; set; }
    }
}
