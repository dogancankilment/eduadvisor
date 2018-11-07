using System.Collections.Generic;

namespace EduApi.Models
{
    public class AnasayfaModel
    {
        public List<SlaytOkulModel> encokyorumalanlar { get; set; }
        public List<SlaytOkulModel> encokgoruntulenenler { get; set; }
        public List<SlaytOkulModel> enbegenilenler { get; set; }
        public List<TextValueModel> enpopulersehirler { get; set; }
    }
}