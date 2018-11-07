using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class YorumController : ApiController
    {
        // POST api/<controller>
        public string Post(YeniOkulYorumModel value)
        {
            return islem.YorumEkle(value);
        }
    }
}