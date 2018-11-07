using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class OkulEkleController : ApiController
    {
        // POST api/<controller>
        public string Post(YeniOkulModel value)
        {
            return islem.OkulEkle(value);
        }
    }
}