using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApi.Models
{
    public class UyeModel
    {
        public string Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Yas { get; set; }
        public string fotograf { get; set; }
        public string Cinsiyet { get; set; }
        public string Haber_Bulten { get; set; }
        public string mail { get; set; }
        public string giris { get; set; }
        public string biyografi { get; set; }
        public string tel_no { get; set; }
        public string puani { get; set; }
        public string adres { get; set; }
        public string dog_kod { get; set; }
        public UyeModel(string Biyografi, string adi, string soyadi, string yas, string Resim = "",string cinsiyet = "", string haber = "",string g_tel_no="",string g_puani="",string g_adres="")
        {
            Adi = adi;
            Soyadi = soyadi;
            Yas = yas;
            fotograf = Resim;
            Cinsiyet = cinsiyet;
            Haber_Bulten = haber;
            biyografi = Biyografi;
            tel_no = g_tel_no;
            puani = g_puani;
            adres = g_adres;
        }
        public UyeModel()
        {

        }
    }
}