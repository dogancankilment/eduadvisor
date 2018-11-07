using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class ProgramController : ApiController
    {
        public List<GruplarModel> Get()
        {
            return islem.ProgramlariGetir();
        }
    }
}