using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class GrupController : ApiController
    {
        // GET api/<controller>
        public List<GruplarModel> Get()
        {
            return islem.GruplariGetir();
        }
    }
}