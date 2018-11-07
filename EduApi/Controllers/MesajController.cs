using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class MesajController : ApiController
    {
        // POST api/<controller>
        public string Post(iletisimModel value)
        {
            return islem.MesajGonder(value);
        }
    }
}