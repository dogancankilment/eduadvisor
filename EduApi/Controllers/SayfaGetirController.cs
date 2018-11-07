using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class SayfaGetirController : ApiController
    {
        public string Post(SayfaModel gelen)
        {
            return islem.SayfaAciklamaGetir(gelen.id);
        }
    }
}