using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class GirisController : ApiController
    {
        // POST api/<controller>
        public UyeModel Post(GirisModel bilgiler)
        {
            return islem.girisyap(bilgiler.mail, bilgiler.sifre);
        }
    }
}