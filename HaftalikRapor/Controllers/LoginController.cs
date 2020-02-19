using HaftalikRapor.Models;
using NZF_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HaftalikRapor.Controllers
{
    public class LoginController : Controller
    {

        
        public ActionResult Index()
        {
            try
            {

                Session["KullaniciAd"] = null;
                Session["PersonelID"] = null;
                Session["YetkiID"] = null;
                Session["PersonelAd"] = null;
                Session["PersonelSoyAd"] = null;
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }
        [HttpPost]
        public ActionResult Index(PersonelDB models, string responsables, bool checkResp = false)
        {
            try
            {
                DataAccesBase db = new DataAccesBase();
                string sql = "Select * from PersonelTBL";
                DataTable dt = db.ReturnDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["KullaniciAd"].ToString() == models.KullaniciAd & dr["KullaniciSifre"].ToString() == models.KullaniciSifre)
                    {
                        Session["KullaniciAd"] = dr["KullaniciAd"].ToString();
                        Session["PersonelID"] = dr["ID"].ToString();
                        Session["YetkiID"] = dr["RolID"].ToString();
                        Session["PersonelAd"] = dr["PersonelAd"].ToString();
                        Session["PersonelSoyAd"] = dr["PersonelSoyAd"].ToString();
                        if (checkResp == true)
                        {
                            HttpCookie cerez = new HttpCookie("cerezim");
                            cerez.Values.Add("KullaniciSifre", models.KullaniciSifre);
                            cerez.Values.Add("KullaniciAd", models.KullaniciAd);
                            cerez.Expires = DateTime.Now.AddDays(30);
                            Response.Cookies.Add(cerez);
                        }
                        
                        if (Session["YetkiID"].ToString() == "1")
                        {
                            return RedirectToAction("PerIndex", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Personel");
                        }

                    }
                }
                Response.Write("<script>alert('Kullanıcı Adı veya Şifre Yanlış')</script>");
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }

        }

        public string mails;
        public string sifre;
        public string Ad;
        public string Soyad;
        public string Kullaniciad;
        public ActionResult SifreUnut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreUnut(PersonelDB p)
        {
            try
            {
                string sql;
                sql = "Select * from PersonelTBL as p where p.EMail=" + "'" + p.EMail + "'";
                DataAccesBase db = new DataAccesBase();
                DataTable model = db.ReturnDataTable(sql);
                foreach (DataRow item in model.Rows)
                {
                    Kullaniciad = item["KullaniciAd"].ToString();
                    Soyad = item["PersonelSoyAd"].ToString();
                    Ad = item["PersonelAd"].ToString();
                    sifre = item["KullaniciSifre"].ToString();
                    mails = item["EMail"].ToString();

                }
                if (mails == null)
                {

                    Response.Write("<script>alert('Böyle bir mail yoktur')</script>");
                }
                else
                {
                    string Mesaj = "Sayın " + Ad + " " + Soyad + " Kullanıcı Adını: " + Kullaniciad + " Şifreniz: " + sifre;
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.From = new MailAddress("bilgi@technorob.com.tr");//Verici
                    mail.To.Add(mails);//Alıcı
                    mail.IsBodyHtml = true;//Html mi 
                    mail.Subject = "Rapor Kullanıcı Bilgileri";//Mail Konusu
                    mail.BodyEncoding = System.Text.Encoding.UTF8;//UTF-8 Encoding
                    mail.Body = Mesaj;//Mail Mesajı
                    SmtpClient sc = new SmtpClient();
                    sc.Host = "mail.technorob.com";//Smtp Host
                    sc.Port = 587;//Smtp Port
                    sc.EnableSsl = false;//Enable SSL
                    sc.Credentials = new NetworkCredential("bilgi@technorob.com.tr", "TEchnorob18");//Gmail Kulanıcı - Şifre
                    sc.Send(mail);//Mail Gönder
                    Response.Write("<script>alert('Şifreniz Mailinize Gönderilmiştir')</script>");
                }
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Hata");
            }


        }
       

    }
}