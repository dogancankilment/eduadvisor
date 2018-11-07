using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class ProfilimKullaniciBilgileriController : ApiController
    {
        // GET api/<controller>/5
        public ProfilimKullaniciBilgileriModel Get(string id)
        {
            return islem.ProfilimKullaniciBilgileriGetir(id);
        }
        public string Post(ProfilimKullaniciBilgileriModel value)
        {
            return islem.ProfilimKullaniciGuncelle(value);
        }
    }
}