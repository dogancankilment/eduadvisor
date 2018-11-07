using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class UlkelerController : ApiController
    {
        // GET api/<controller>
        public List<TextValueModel> Get()
        {
            return islem.Ulkeler();
        }        
        public List<TextValueModel> Get(int id)
        {
            return islem.SehirleriGetir(id);
        }
    }
}