using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;
namespace HaftalikRapor.Models
{
    public class PersonelDB:RolDB
    {
        public int ID{ get; set; }
        public int RolID { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyAd { get; set; }
        public string TelNo { get; set; }
        public string Adres { get; set; }
        public string EMail { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime IseGirisTarih { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public Nullable<DateTime> IstenCikisTarih { get; set; }

        public string KullaniciAd { get; set; }
       
        public string KullaniciSifre { get; set; }

       

        }
    }
