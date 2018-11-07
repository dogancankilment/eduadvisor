using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class OkulDetayController : ApiController
    {
        public ApiOkulDetayModel Post(OkulDetayPostModel value)
        {
            return islem.OkulDetayGetir(value);
        }
    }
}