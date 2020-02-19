using HaftalikRapor.Models;
using NZF_DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace HaftalikRapor.Controllers
{
    public class RaporController : Controller
    {
        List<RaporDB> RaporList = new List<RaporDB>();
        List<PersonelDB> personel = new List<PersonelDB>();
        // GET: Rapor
        public ActionResult Index()
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                string sql;
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID where r.DurumID=1 AND r.PersonelID =" + Session["PersonelID"];
                DataTable rapor = db.ReturnDataTable(sql);
                foreach (DataRow item in rapor.Rows)
                {
                    RaporList.Add(new RaporDB
                    {
                        ID = Convert.ToInt32(item["ID"].ToString()),
                        PersonelID = Convert.ToInt32(item["PersonelID"].ToString()),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString(),
                        RaporBitisTarih = Convert.ToDateTime(item["RaporBitisTarih"])
                    });

                }


                DataTable bildirim = db.ReturnDataTable("Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID left join AdminOnayTBL as a on r.DurumID = a.ID where r.DurumID = 2 AND r.PersonelID =" + Session["PersonelID"]);
                int adet = bildirim.Rows.Count;
                ViewBag.Adet = adet;

                DataTable bildirim2 = db.ReturnDataTable("Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID left join AdminOnayTBL as a on r.DurumID = a.ID where r.DurumID = 3 AND r.PersonelID =" + Session["PersonelID"]);
                int adet2 = bildirim2.Rows.Count;
                ViewBag.Adet2 = adet2;
                return View(RaporList);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
           
        }
        public ActionResult Details(int ID)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                List<PersonelDB> rapor = new List<PersonelDB>();
                DataAccesBase dbb = new DataAccesBase();
                DataTable modell = dbb.ReturnDataTable("Select ID,PersonelAd from PersonelTBL");
                foreach (DataRow item in modell.Rows)
                {
                    rapor.Add(new PersonelDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        PersonelAd = item["PersonelAd"].ToString(),

                    });
                }
                ViewBag.PersonelAd = rapor;

                string sql;
                List<RaporDB> Raporlist = new List<RaporDB>();
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from RaporTBL as s left join PersonelTBL as p on s.PersonelID=p.ID where s.ID=" + ID + "";
                DataTable model = db.ReturnDataTable(sql);
                foreach (DataRow item in model.Rows)
                {
                    Raporlist.Add(new RaporDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        PersonelID = Convert.ToInt32(item["PersonelID"]),
                        RaporBaslangicTarih = Convert.ToDateTime(item["RaporBaslangicTarih"]),
                        RaporBitisTarih = Convert.ToDateTime(item["RaporBitisTarih"]),
                        YapilanProje = item["YapilanProje"].ToString(),
                        Yapilanlar = item["Yapilanlar"].ToString(),
                        Sorunlar = item["Sorunlar"].ToString(),
                        KullanilanMalzeme = item["KullanilanMalzeme"].ToString(),
                        Oneriler = item["Oneriler"].ToString()
                    });
                }

                return View(Raporlist);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
           
        }
        public ActionResult Create()
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                List<PersonelDB> rapor = new List<PersonelDB>();
                DataAccesBase db = new DataAccesBase();
                DataTable model = db.ReturnDataTable("Select ID,PersonelAd from PersonelTBL");
                foreach (DataRow item in model.Rows)
                {
                    rapor.Add(new PersonelDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        PersonelAd = item["PersonelAd"].ToString(),

                    });
                }
                ViewBag.PersonelAd = rapor;
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
           
        }
        
       
        [HttpPost]
        public ActionResult Create(RaporDB r)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (r.ID == 0)
                {
                    RaporTBL rapor = new RaporTBL(r.ID);
                    rapor.PersonelID = Convert.ToInt32(Session["PersonelID"]);
                    rapor.RaporBaslangicTarih = Convert.ToDateTime(r.RaporBaslangicTarih);
                    rapor.RaporBitisTarih = Convert.ToDateTime(r.RaporBitisTarih);
                    rapor.YapilanProje = r.YapilanProje;
                    rapor.Yapilanlar = r.Yapilanlar;
                    rapor.Sorunlar = r.Sorunlar;
                    rapor.KullanilanMalzeme = r.KullanilanMalzeme;
                    rapor.Oneriler = r.Oneriler;
                    rapor.DurumID = 2;
                    rapor.Kaydet();

                    string sql;
                    sql = "Select * from PersonelTBL as p where p.RolID=1";
                    DataAccesBase db = new DataAccesBase();
                    DataTable model = db.ReturnDataTable(sql);
                    foreach (DataRow item in model.Rows)
                    {

                        string Mesaj = DateTime.Now + " tarihinde " + Session["PersonelAd"] + " " + Session["PersonelSoyAd"] + " yeni haftalık rapor eklemiştir";
                        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                        mail.From = new MailAddress("bilgi@technorob.com.tr");//Verici
                        mail.To.Add(item["EMail"].ToString());//Alıcı
                        mail.IsBodyHtml = true;//Html mi 
                        mail.Subject = "Rapor";//Mail Konusu
                        mail.BodyEncoding = System.Text.Encoding.UTF8;//UTF-8 Encoding
                        mail.Body = Mesaj;//Mail Mesajı
                        SmtpClient sc = new SmtpClient();
                        sc.Host = "mail.technorob.com";//Smtp Host
                        sc.Port = 587;//Smtp Port
                        sc.EnableSsl = false;//Enable SSL
                        sc.Credentials = new NetworkCredential("bilgi@technorob.com.tr", "TEchnorob18");//Gmail Kulanıcı - Şifre
                        sc.Send(mail);//Mail Gönder

                    }



                    return RedirectToAction("OnayBekleyen", "Rapor");
                }
                else
                {
                    RaporTBL rapor = new RaporTBL(r.ID);
                    rapor.PersonelID = Convert.ToInt32(Session["PersonelID"]);
                    rapor.RaporBaslangicTarih = Convert.ToDateTime(r.RaporBaslangicTarih);
                    rapor.RaporBitisTarih = Convert.ToDateTime(r.RaporBitisTarih);
                    rapor.YapilanProje = r.YapilanProje;
                    rapor.Yapilanlar = r.Yapilanlar;
                    rapor.Sorunlar = r.Sorunlar;
                    rapor.KullanilanMalzeme = r.KullanilanMalzeme;
                    rapor.Oneriler = r.Oneriler;
                    rapor.DurumID = 2;
                    rapor.Kaydet();
                    return RedirectToAction("Index", "Rapor");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
           
        }

       public ActionResult Edit(int ID)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                RaporDB modeldb = new Models.RaporDB();
                RaporTBL rapor = new RaporTBL(ID);
                modeldb.PersonelAd = Session["PersonelAd"].ToString();
                modeldb.RaporBaslangicTarih = rapor.RaporBaslangicTarih;
                modeldb.RaporBitisTarih = rapor.RaporBitisTarih;
                modeldb.YapilanProje = rapor.YapilanProje;
                modeldb.Yapilanlar = rapor.Yapilanlar;
                modeldb.Sorunlar = rapor.Sorunlar;
                modeldb.Oneriler = rapor.Oneriler;
                modeldb.KullanilanMalzeme = rapor.KullanilanMalzeme;
                modeldb.DurumID = 2;

                return View("Create", modeldb);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
         
       }
       public JsonResult RapSil(int id)
       {
           RaporTBL per = new NZF_DAL.RaporTBL(id);
           var silinen = per.Delete();
           return Json(true, JsonRequestBehavior.AllowGet);
       }
        public ActionResult OnayBekleyen()
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                List<RaporDB> RaporList = new List<RaporDB>();
                string sql;
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID left join AdminOnayTBL as a on r.DurumID = a.ID where  r.DurumID = 2 AND r.PersonelID =" + Session["PersonelID"];

                DataTable rapor = db.ReturnDataTable(sql);
                foreach (DataRow item in rapor.Rows)
                {
                    RaporList.Add(new RaporDB
                    {
                        ID = Convert.ToInt32(item["ID"].ToString()),
                        Durum = item["Durum"].ToString(),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString(),
                        RaporBitisTarih = Convert.ToDateTime(item["RaporBitisTarih"])

                    });

                }


                return View(RaporList);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
           
        }
        public ActionResult Reddedilen()
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                List<RaporDB> RaporList = new List<RaporDB>();
                string sql;
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID left join AdminOnayTBL as a on r.DurumID = a.ID where r.DurumID = 3 AND r.PersonelID =" + Session["PersonelID"];

                DataTable rapor = db.ReturnDataTable(sql);
                foreach (DataRow item in rapor.Rows)
                {
                    RaporList.Add(new RaporDB
                    {
                        ID = Convert.ToInt32(item["ID"].ToString()),
                        Durum = item["Durum"].ToString(),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString(),
                        RaporBitisTarih = Convert.ToDateTime(item["RaporBitisTarih"])

                    });

                }
                return View(RaporList);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
          
        }
        public JsonResult RedSil(int id)
        {
            RaporTBL per = new NZF_DAL.RaporTBL(id);
            var silinen = per.Delete();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}