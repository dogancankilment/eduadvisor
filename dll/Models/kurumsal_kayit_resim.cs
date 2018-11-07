using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class kurumsal_kayit_resim
    {
        public int id { get; set; }
        public Nullable<int> yorum_kurumsal_id { get; set; }
        public string resim_adi { get; set; }
        public virtual kurumsal_kayit kurumsal_kayit { get; set; }
    }
}
