using NZF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaftalikRapor.Models
{
    public class RolDB:AdminOnayTBLDB
    {
        public int ID { get; set; }
        public string Yetkisi { get; set; }
    }
}