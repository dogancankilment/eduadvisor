using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class KampusluGrupGetirController : ApiController
    {
        public List<KampusluGrupGetirModel> Get()
        {
            return islem.KampusluGrupGetir();
        }
    }
}