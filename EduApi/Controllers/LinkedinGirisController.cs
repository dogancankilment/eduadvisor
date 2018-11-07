using EduApi.Models;
using EduApi.Siniflar;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class LinkedinGirisController : ApiController
    {
        public UyeModel Post(LinkedinProfile value)
        {
            return islem.LinkedinGirisi(value);
        }
    }
}