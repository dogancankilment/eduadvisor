using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class egitim_durumlari
    {
        public int id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public Nullable<int> sira_no { get; set; }
        public Nullable<byte> silindi { get; set; }
    }
}
