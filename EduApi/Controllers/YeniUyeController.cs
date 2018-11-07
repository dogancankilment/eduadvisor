using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class YeniUyeController : ApiController
    {
        // POST api/<controller>
        public string Post(YeniUyeModel gelen)
        {
            return islem.UyeEkle(gelen);
        }
    }
}