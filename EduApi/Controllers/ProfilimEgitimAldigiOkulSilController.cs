using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class ProfilimEgitimAldigiOkulSilController : ApiController
    {
        // GET api/<controller>/5
        public string Get(string id)
        {
            return islem.ProfilimEgitimAldigiOkulSil(id);
        }
    }
}