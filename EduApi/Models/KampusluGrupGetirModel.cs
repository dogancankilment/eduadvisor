using System.Collections.Generic;

namespace EduApi.Models
{
    public class KampusluGrupGetirModel
    {
        public string egitim_id { get; set; }
        public TextValueModel grup { get; set; }
        public List<TextValueModel> kampusler { get; set; }
    }
}