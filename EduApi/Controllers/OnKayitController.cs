using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class OnKayitController : ApiController
    {
        public string Post(OnKayitFormModel value)
        {
            return islem.OnKayitOlustur(value);
        }
    }
}