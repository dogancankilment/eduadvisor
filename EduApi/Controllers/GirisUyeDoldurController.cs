using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class GirisUyeDoldurController : ApiController
    {
        public GirisUyeDoldurModel Get(string id)
        {
            return islem.GirisUyeGetir(id);
        }
    }
}