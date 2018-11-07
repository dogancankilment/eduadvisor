using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class UlkeSehirController : ApiController
    {
        // GET api/<controller>
        public List<UlkeModel> Get()
        {
            return islem.UlkeSehirGetir();
        }
    }
}