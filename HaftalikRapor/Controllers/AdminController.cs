using HaftalikRapor.Models;
using NZF_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services;
using PagedList;
using PagedList.Mvc;
namespace HaftalikRapor.Controllers
{
    public class AdminController : Controller
    {

        List<PersonelDB> list = new List<PersonelDB>();
        public ActionResult PerIndex()
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                string sql;
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from PersonelTBL as p left join RolTBL as r on p.RolID=r.ID";
                DataTable model = db.ReturnDataTable(sql);
                foreach (DataRow item in model.Rows)
                {
                    list.Add(new PersonelDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        RolID = Convert.ToInt32(item["RolID"].ToString()),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString(),
                        TelNo = item["TelNo"].ToString(),
                        Adres = item["Adres"].ToString(),
                        EMail = item["EMail"].ToString(),
                        IseGirisTarih = Convert.ToDateTime(item["IseGirisTarih"]),
                        IstenCikisTarih = Convert.ToDateTime(item["IstenCikisTarih"]),
                        KullaniciAd = item["KullaniciAd"].ToString(),
                        KullaniciSifre = item["KullaniciSifre"].ToString()


                    });
                }
                ViewBag.Dev = list;
                return View("PerIndex");

            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }
        public ActionResult Filtre(PersonelDB p)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                string sql;
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from PersonelTBL as p left join RolTBL as r on p.RolID=r.ID where p.PersonelAd like  '%" + p.PersonelAd + "%'";
                DataTable model = db.ReturnDataTable(sql);
                foreach (DataRow item in model.Rows)
                {
                    list.Add(new PersonelDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString(),
                        EMail = item["EMail"].ToString(),
                    });
                }
                ViewBag.Dev = list;
                return View("PerIndex");

            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
        }
        public ActionResult PerCreate()
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                List<RolDB> RolList = new List<RolDB>();
                DataAccesBase db = new DataAccesBase();
                DataTable rol = db.ReturnDataTable("Select ID,Yetkisi from RolTBL");
                foreach (DataRow item in rol.Rows)
                {
                    RolList.Add(new RolDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        Yetkisi = item["Yetkisi"].ToString()
                    });
                }
                ViewBag.RolAdi = RolList;
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }
        [HttpPost]
        public ActionResult PerCreate(PersonelDB p)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (p.ID == 0)
                {
                    PersonelTBL personel = new PersonelTBL(p.ID);
                    personel.PersonelAd = p.PersonelAd;
                    personel.PersonelSoyAd = p.PersonelSoyAd;
                    personel.KullaniciAd = p.KullaniciAd;
                    personel.KullaniciSifre = p.KullaniciSifre;
                    personel.TelNo = p.TelNo;
                    personel.Adres = p.Adres;
                    personel.IseGirisTarih = p.IseGirisTarih;
                    //personel.IstenCikisTarih = p.IstenCikisTarih;
                    personel.EMail = p.EMail;
                    personel.RolID = p.RolID;

                    personel.Kaydet();
                    return RedirectToAction("PerCreate", "Admin");

                }
                else
                {
                    PersonelTBL personel = new PersonelTBL(p.ID);
                    personel.PersonelAd = p.PersonelAd;
                    personel.PersonelSoyAd = p.PersonelSoyAd;
                    personel.KullaniciAd = p.KullaniciAd;
                    personel.KullaniciSifre = p.KullaniciSifre;
                    personel.TelNo = p.TelNo;
                    personel.Adres = p.Adres;
                    personel.IseGirisTarih = p.IseGirisTarih;
                    //personel.IstenCikisTarih = p.IstenCikisTarih;
                    personel.EMail = p.EMail;
                    personel.RolID = p.RolID;

                    personel.Kaydet();
                    return RedirectToAction("PerIndex", "Admin");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }

        public ActionResult PerEdit(int ID)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                List<RolDB> RolList = new List<RolDB>();
                DataAccesBase db = new DataAccesBase();
                DataTable rol = db.ReturnDataTable("Select ID,Yetkisi from RolTBL");
                foreach (DataRow item in rol.Rows)
                {
                    RolList.Add(new RolDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        Yetkisi = item["Yetkisi"].ToString()
                    });
                }
                ViewBag.RolAdi = RolList;

                PersonelDB model = new Models.PersonelDB();
                PersonelTBL per = new PersonelTBL(ID);
                model.ID = per.ID;
                model.PersonelAd = per.PersonelAd;
                model.PersonelSoyAd = per.PersonelSoyAd;
                model.KullaniciAd = per.KullaniciAd;
                model.KullaniciSifre = per.KullaniciSifre;
                model.IseGirisTarih = per.IseGirisTarih;
                model.IstenCikisTarih = per.IstenCikisTarih;
                model.TelNo = per.TelNo;
                model.Adres = per.Adres;
                model.EMail = per.EMail;
                model.RolID = per.RolID;

                return View("PerCreate", model);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }


        }
        public ActionResult PerDetails(int ID)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }


                string sql;
                List<PersonelDB> PersonelList = new List<PersonelDB>();
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from PersonelTBL as p left join RolTBL as r on p.RolID = r.ID  where p.ID=" + ID + "";
                DataTable model = db.ReturnDataTable(sql);
                foreach (DataRow item in model.Rows)
                {
                    PersonelList.Add(new PersonelDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        Yetkisi = item["Yetkisi"].ToString(),
                        RolID = Convert.ToInt32(item["RolID"]),
                        IseGirisTarih = Convert.ToDateTime(item["IseGirisTarih"]),
                        IstenCikisTarih = Convert.ToDateTime(item["IstenCikisTarih"]),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyad"].ToString(),
                        TelNo = item["TelNo"].ToString(),
                        EMail = item["EMail"].ToString(),
                        KullaniciAd = item["KullaniciAd"].ToString(),
                        KullaniciSifre = item["KullaniciSifre"].ToString(),
                        Adres = item["Adres"].ToString()

                    });
                }

                return View(PersonelList);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }
        public JsonResult PerSil(int id)
        {
            PersonelTBL per = new NZF_DAL.PersonelTBL(id);
            var silinen = per.Delete();
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        /// /////////////////////////////////////////////////////////////////////////////////////////

        List<RaporDB> RaporList = new List<RaporDB>();
        List<PersonelDB> personel = new List<PersonelDB>();
        
        public ActionResult RapIndex(int? page)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                string sql;
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID where r.DurumID = 1";
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

                DataTable bildirim = db.ReturnDataTable("Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID left join AdminOnayTBL as a on r.DurumID = a.ID where r.DurumID = 2");
                int adet = bildirim.Rows.Count;
                ViewBag.Adet = adet;
               
                return View(RaporList.ToPagedList(page ?? 1,12));
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }


        }
        public ActionResult RaporFiltrele(RaporDB r, int? page)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                DataAccesBase db = new DataAccesBase();
                string sql = string.Format("select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID where r.DurumID = 1 AND r.RaporBitisTarih = '{0:yyyyMMdd}'", r.RaporBitisTarih);
                DataTable model = db.ReturnDataTable(sql);
                foreach (DataRow item in model.Rows)
                {
                    RaporList.Add(new RaporDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        PersonelID = Convert.ToInt32(item["PersonelID"]),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString(),
                        RaporBitisTarih = Convert.ToDateTime(item["RaporBitisTarih"])


                    });
                }

                DataTable bildirim = db.ReturnDataTable("Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID left join AdminOnayTBL as a on r.DurumID = a.ID where r.DurumID = 2");
                int adet = bildirim.Rows.Count;
                ViewBag.Adet = adet;

                return PartialView("RapIndex",RaporList.ToPagedList(page ?? 1, 12));
                //return PartialView kullanmamızın sebebi RapIndex sayfası IPagedList lması ve bu görüntüyü partialview dan çekmemiz
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
        }
        public ActionResult RaporİsimFiltrele(PersonelDB per,int? page)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                string sql;
                DataAccesBase db = new DataAccesBase();
                sql = "Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID where p.PersonelAd like  '%" + per.PersonelAd + "%'";
                DataTable model = db.ReturnDataTable(sql);
                foreach (DataRow item in model.Rows)
                {
                    RaporList.Add(new RaporDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        PersonelID = Convert.ToInt32(item["PersonelID"]),
                        RaporBitisTarih = Convert.ToDateTime(item["RaporBitisTarih"]),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString(),

                    });
                }
                DataTable bildirim = db.ReturnDataTable("Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID left join AdminOnayTBL as a on r.DurumID = a.ID where r.DurumID = 2");
                int adet = bildirim.Rows.Count;
                ViewBag.Adet = adet;

               
                return PartialView("RapIndex",RaporList.ToPagedList(page ?? 1, 12));

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Hata");
            }
        }
        public ActionResult RapDetails(int ID)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                List<PersonelDB> rapor = new List<PersonelDB>();
                DataAccesBase dbb = new DataAccesBase();
                DataTable modell = dbb.ReturnDataTable("Select ID,PersonelAd,PersonelSoyAd from PersonelTBL");
                foreach (DataRow item in modell.Rows)
                {
                    rapor.Add(new PersonelDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString()

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
                        Oneriler = item["Oneriler"].ToString(),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString()

                    });
                }

                return View(Raporlist);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }
        public ActionResult RapCreate()
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
        public ActionResult RapCreate(RaporDB r)
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
                    rapor.PersonelID = r.PersonelID;
                    rapor.RaporBaslangicTarih = r.RaporBaslangicTarih;
                    rapor.RaporBitisTarih = r.RaporBitisTarih;
                    rapor.YapilanProje = r.YapilanProje;
                    rapor.Yapilanlar = r.Yapilanlar;
                    rapor.Sorunlar = r.Sorunlar;
                    rapor.KullanilanMalzeme = r.KullanilanMalzeme;
                    rapor.Oneriler = r.Oneriler;
                    rapor.DurumID = 2;
                    rapor.Kaydet();


                    return RedirectToAction("RapOnay", "Admin");

                }
                else
                {
                    RaporTBL rapor = new RaporTBL(r.ID);
                    rapor.PersonelID = r.PersonelID;
                    rapor.RaporBaslangicTarih = r.RaporBaslangicTarih;
                    rapor.RaporBitisTarih = r.RaporBitisTarih;
                    rapor.YapilanProje = r.YapilanProje;
                    rapor.Yapilanlar = r.Yapilanlar;
                    rapor.Sorunlar = r.Sorunlar;
                    rapor.KullanilanMalzeme = r.KullanilanMalzeme;
                    rapor.Oneriler = r.Oneriler;
                    rapor.DurumID = 2;

                    rapor.Kaydet();
                    return RedirectToAction("RapIndex", "Admin");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }
        public ActionResult RapEdit(int ID)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                List<PersonelDB> perlist = new List<PersonelDB>();
                DataAccesBase db = new DataAccesBase();
                DataTable rol = db.ReturnDataTable("Select ID,PersonelAd from PersonelTBL");
                foreach (DataRow item in rol.Rows)
                {
                    perlist.Add(new PersonelDB
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        PersonelAd = item["PersonelAd"].ToString()
                    });
                }
                ViewBag.PersonelAd = perlist;

                RaporDB modeldb = new Models.RaporDB();
                RaporTBL rapor = new RaporTBL(ID);
                modeldb.RaporBaslangicTarih = rapor.RaporBaslangicTarih;
                modeldb.RaporBitisTarih = rapor.RaporBitisTarih;
                modeldb.YapilanProje = rapor.YapilanProje;
                modeldb.Yapilanlar = rapor.Yapilanlar;
                modeldb.Sorunlar = rapor.Sorunlar;
                modeldb.Oneriler = rapor.Oneriler;
                modeldb.KullanilanMalzeme = rapor.KullanilanMalzeme;

                return View("RapCreate", modeldb);
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
        public ActionResult RapOnay()
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
                sql = "Select * from RaporTBL as r left join PersonelTBL as p on r.PersonelID = p.ID left join AdminOnayTBL as a on r.DurumID = a.ID where r.DurumID = 2";
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
        public ActionResult AdminOnay(int ID)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                RaporTBL tb = new RaporTBL(ID);
                tb.DurumID = 1;
                tb.Kaydet();
                return RedirectToAction("RapIndex", "Admin");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }
        public ActionResult AdminRed(int ID)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                RaporTBL tb = new RaporTBL(ID);
                tb.DurumID = 3;
                tb.Kaydet();
                return RedirectToAction("RapOnay", "Admin");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }

        public int PerID;
        public string PerMail;
        [HttpPost]
        public JsonResult RaporRedMail(RaporDB o)
        {

            string sql;
            string sqlper;
            List<RaporDB> Raporlist = new List<RaporDB>();

            DataAccesBase db = new DataAccesBase();
            sql = "Select * from RaporTBL as p where p.ID=" + o.ID;
            DataTable model = db.ReturnDataTable(sql);
            foreach (DataRow item in model.Rows)
            {
                PerID = Convert.ToInt32(item["PersonelID"]);
            }

            sqlper = "Select * from PersonelTBL as p where p.ID=" + PerID;
            DataTable modelPer = db.ReturnDataTable(sqlper);
            foreach (DataRow item in modelPer.Rows)
            {
                PerMail = (item["EMail"].ToString());
            }


            string Mesaj = "Raporunuz " + DateTime.Now + " tarihinde " + o.RedSebep + " sebebinden dolayı reddedilmiştir.  ";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress("bilgi@technorob.com.tr");//Verici
            mail.To.Add(PerMail);//Alıcı
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
            RaporTBL tb = new RaporTBL(o.ID);
            tb.DurumID = 3;
            tb.Kaydet();

            return Json("Rapor Red Edilmiştir.", JsonRequestBehavior.AllowGet);



        }

    }
}


