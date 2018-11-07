using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class SorulanSorularController : ApiController
    {
        public List<SorulanSorularModel> Get()
        {
            return islem.SorulanSorular();
        }
    }
}