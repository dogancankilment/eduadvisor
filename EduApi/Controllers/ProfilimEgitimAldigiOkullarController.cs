using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class ProfilimEgitimAldigiOkullarController : ApiController
    {
        public List<ProfilimEgitimAldigimOkullarModel> Get(string id)
        {
            return islem.ProfilimEgitimAldigiOkullarGetir(id);
        }
        public string Post(ProfilimEgitimAldigimOkulEkleModel value)
        {
            return islem.ProfilimEgitimAldigiOkulEkle(value);
        }
    }
}