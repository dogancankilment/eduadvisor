using EduApi.Models;
using EduApi.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class YakinOkulSayisiController : ApiController
    {
        // POST api/<controller>
        public string Post(YakinOkulSayiModel value)
        {
            return islem.YakinOkulSayisiGetir(value);
        }
    }
}