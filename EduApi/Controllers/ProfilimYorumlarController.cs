using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class ProfilimYorumlarController : ApiController
    {
        public List<ProfilimYorumlarModel> Get(string id)
        {
            return islem.ProfilimYorumlariGetir(id);
        }
        public string Post(ProfilimYorumlarModel gelen)
        {
            return islem.ProfilimYorumResimleriGuncelle(gelen);
        }
    }
}