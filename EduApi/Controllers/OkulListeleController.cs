using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class OkulListeleController : ApiController
    {
        // GET api/<controller>/5
        public ResultModel Post(OkulListeleSearchModel gelenler)
        {
            ResultModel donecek = new ResultModel();
            OkulListeleSonucModel gelen = islem.OkulListeleSonuclari(gelenler,"tr-TR");
            donecek.Data = gelen;
            return donecek;
        }
    }
}