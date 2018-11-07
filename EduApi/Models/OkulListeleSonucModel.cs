using System.Collections.Generic;

namespace EduApi.Models
{
    public class OkulListeleSonucModel
    {
        public List<OkulListeleItemModel> okullar { get; set; }
        public string TotalCount { get; set; }
    }
}