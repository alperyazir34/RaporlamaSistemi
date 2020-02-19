using NZF_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HaftalikRapor.Models
{
    public class RaporDB :PersonelDB
    {
        public int ID { get; set; }
        public int PersonelID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]//Ön Tarafta bu formatta gözükmesi için
        public DateTime RaporBaslangicTarih { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime RaporBitisTarih { get; set; }

        public string YapilanProje { get; set; }
        public string Yapilanlar { get; set; }
        public string Sorunlar { get; set; }
        public string KullanilanMalzeme { get; set; }
        public string Oneriler { get; set; }
        public int DurumID { get; set; }
        public string RedSebep { get; set; }

    }
}