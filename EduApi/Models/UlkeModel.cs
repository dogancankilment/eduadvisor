using System.Collections.Generic;

namespace EduApi.Models
{
    public class UlkeModel
    {
        public TextValueModel ulke { get; set; }
        public List<TextValueModel> sehirler { get; set; }
    }
}