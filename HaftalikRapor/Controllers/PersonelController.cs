using HaftalikRapor.Models;
using NZF_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaftalikRapor.Controllers
{
    public class PersonelController : Controller
    {
        List<PersonelDB> list = new List<PersonelDB>();
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
                return View(list);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Hata");
            }
          

        }
        public ActionResult Detay()
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                List<PersonelDB> profil = new List<PersonelDB>();
                DataAccesBase db = new DataAccesBase();
                DataTable rol = db.ReturnDataTable("Select * from PersonelTBL where ID =" + Convert.ToInt32(Session["PersonelID"]));
                foreach (DataRow item in rol.Rows)
                {
                    profil.Add(new PersonelDB
                    {

                        ID = Convert.ToInt32(item["ID"].ToString()),
                        PersonelAd = item["PersonelAd"].ToString(),
                        PersonelSoyAd = item["PersonelSoyAd"].ToString(),
                        Adres = item["Adres"].ToString(),
                        KullaniciAd = item["KullaniciAd"].ToString(),
                        KullaniciSifre = item["KullaniciSifre"].ToString(),
                        EMail = item["EMail"].ToString(),
                        TelNo = item["TelNo"].ToString(),
                        IseGirisTarih = Convert.ToDateTime(item["IseGirisTarih"].ToString()),
                        IstenCikisTarih = Convert.ToDateTime(item["IstenCikisTarih"].ToString()),

                    });
                }


                return View(profil);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
           

        }
        public ActionResult Edit(PersonelDB model)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                //PersonelTBL per = new PersonelTBL(model.ID);
                //per.KullaniciAd = model.KullaniciAd;
                //per.KullaniciSifre = model.KullaniciSifre;
                //per.Kaydet();

                return View("Create");
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
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
           
        }
        [HttpPost]
        public ActionResult Create(PersonelDB p)
        {
            try
            {
                if (Session["KullaniciAd"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (p.ID == 0)
                {
                    if (p.KullaniciAd != null && p.KullaniciSifre != null)
                    {
                        PersonelTBL personel = new PersonelTBL(p.ID);
                        personel.KullaniciAd = p.KullaniciAd;
                        personel.KullaniciSifre = p.KullaniciSifre;

                        personel.Kaydet();
                        return RedirectToAction("Create", "Personel");
                    }
                    else
                    {


                        return View();
                    }



                }
                else
                {
                    if (p.KullaniciAd != null && p.KullaniciSifre != null)
                    {
                        PersonelTBL personel = new PersonelTBL(p.ID);
                        personel.KullaniciAd = p.KullaniciAd;
                        personel.KullaniciSifre = p.KullaniciSifre;

                        personel.Kaydet();
                        return RedirectToAction("Detay", "Personel");
                    }
                    else
                    {


                        return View();
                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }
           
        }

    }
}