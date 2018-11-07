using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class eyalet
    {
        public int id { get; set; }
        public int ulke_id { get; set; }
        public string adi { get; set; }
        public int sira_no { get; set; }
        public virtual ulke ulke { get; set; }
    }
}
