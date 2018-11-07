using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class FacebookGirisController : ApiController
    {
        // POST api/<controller>
        public UyeModel Post(FacebookProfile bilgiler)
        {
            return islem.FacebookGirisi(bilgiler);
        }
    }
}