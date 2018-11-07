using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class ProfilResimController : ApiController
    {
        public ProfilResimResultModel Post(ProfilResimModel value)
        {
            return islem.ProfilResimGuncelle(value);
        }
    }
}
