using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class SizeYakinOkullarController : ApiController
    {
        // POST api/<controller>
        public List<HaritaOkulModel> Post(SizeYakinOkullarPostModel value)
        {
            return islem.SizeYakinOkullarGetir(value);
        }
    }
}