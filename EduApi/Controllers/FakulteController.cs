using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class FakulteController : ApiController
    {
        // GET api/<controller>
        public List<TextValueModel> Get()
        {
            return islem.FakulteleriGetir();
        }
    }
}