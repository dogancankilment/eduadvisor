using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class ProfilimZiyaretEdilenOkullarController : ApiController
    {
        // GET api/<controller>/5
        public List<ProfilimZiyaretEdilenOkullarModel> Get(string id)
        {
            return islem.ProfilimZiyaretEdilenOkullarGetir(id);
        }

    }
}