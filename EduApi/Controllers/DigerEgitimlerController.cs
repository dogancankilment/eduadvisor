using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class DigerEgitimlerController : ApiController
    {
        public List<TextValueModel> Get()
        {
            return islem.DigerEgitimTurleriGetir();
        }
    }
}