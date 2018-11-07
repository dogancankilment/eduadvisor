using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class GoogleGirisController : ApiController
    {
        public UyeModel Post(GoogleProfile value)
        {
            return islem.GoogleGirisi(value);
        }
    }
}