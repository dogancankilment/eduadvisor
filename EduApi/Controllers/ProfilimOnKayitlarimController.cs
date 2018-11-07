using EduApi.Models;
using EduApi.Siniflar;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class ProfilimOnKayitlarimController : ApiController
    {
        // GET api/<controller>/5
        public List<ProfilimOnKayitlarimModel> Get(string id)
        {
            List<ProfilimOnKayitlarimModel> donecek = islem.ProfilimOnKayitlarimGetir(id);
            for (int i = 0; i < donecek.Count; i++)
            {
                donecek[i].basvuru_tarihi = islem.Crypto(Convert.ToDateTime(donecek[i].basvuru_tarihi).ToString("dd/MM/yyyy"));
            }
            return donecek;

        }
    }
}