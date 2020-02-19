using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NZF_DAL
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;

    public class PersonelTBL : DataAccesBase
    {

        string m_SQL;
        int m_ConCount;
		int m_ID; 


        public Int32 RolID;
        public String PersonelAd;
        public String PersonelSoyAd;
        public String TelNo;
        public String Adres;
        public String EMail;
        public System.DateTime IseGirisTarih = Convert.ToDateTime("1900/01/01 00:00");
        public System.DateTime IstenCikisTarih = Convert.ToDateTime("1900/01/01 00:00");
        public String KullaniciAd;
        public String KullaniciSifre;
		    


        public int ID
        {
            get { return m_ID; }
        }

        public PersonelTBL ()
        {
        }
        public PersonelTBL (int pID)
        {
            m_SQL = "Select * from PersonelTBL where ID=" + pID;
            initialize();
        }

        public PersonelTBL(string pFIELD_NAME, string pVALUE)
        {
            m_SQL = "Select * from PersonelTBL where " + pFIELD_NAME + "='" + pVALUE + "'";
            initialize();
        }
 

        public bool initialize()
        {
            DataTable DT = ReturnDataTable(m_SQL);
            try
            {
                m_ConCount = DT.Rows.Count;
                if (DT.Rows.Count == 0)
                {
                    m_ID = 0;
                    return true;
                }
                m_ID = Convert.ToInt32( DT.Rows[0]["ID"]);
                RolID = Convert.ToInt32(DT.Rows[0]["RolID"]);
                PersonelAd = Convert.ToString(DT.Rows[0]["PersonelAd"]);
                PersonelSoyAd = Convert.ToString(DT.Rows[0]["PersonelSoyAd"]);
                TelNo = Convert.ToString(DT.Rows[0]["TelNo"]);
                Adres = Convert.ToString(DT.Rows[0]["Adres"]);
                EMail = Convert.ToString(DT.Rows[0]["EMail"]);
                IseGirisTarih = Convert.ToDateTime(DT.Rows[0]["IseGirisTarih"]);
                IstenCikisTarih = Convert.ToDateTime(DT.Rows[0]["IstenCikisTarih"]);
                KullaniciAd = Convert.ToString(DT.Rows[0]["KullaniciAd"]);
                KullaniciSifre = Convert.ToString(DT.Rows[0]["KullaniciSifre"]);

                DT.Dispose();
            }
            catch (Exception ex)
            {
            }
            return true;
        }

       public void Kaydet()
        {
            if (Kontrol())
            {
                switch (m_ID)
                {
                    case 0:
                        KaydetInsert();
                        break;
                    default:
                        KaydetUpdate();
                        break;
                }
            }
        }


        public bool Kontrol()
        {
            return true;
        }

		
		
		
		private int KaydetInsert()
        {
            string SQL = null;

			SQL="Insert Into PersonelTBL (RolID, PersonelAd, PersonelSoyAd, TelNo, ";
            SQL += "Adres, EMail, IseGirisTarih, IstenCikisTarih, ";
            SQL += "KullaniciAd, KullaniciSifre) values (";
            SQL += "  " + RolID + " ,";
            SQL += "'" + PersonelAd + "',";
            SQL += "'" + PersonelSoyAd + "',";
            SQL += "'" + TelNo + "',";
            SQL += "'" + Adres + "',";
            SQL += "'" + EMail + "',";
            SQL += "'" + IseGirisTarih.ToString("yyyy/dd/MM HH:mm") + " ',";
            SQL += "'" + IstenCikisTarih.ToString("yyyy/dd/MM HH:mm") + " ',";
            SQL += "'" + KullaniciAd + "',";
            SQL += "'" + KullaniciSifre + "'  ";
            SQL += ") SELECT @@IDENTITY AS ID ";   

            DataSet DS = new DataSet();
            try
            {
                this.FillDataSet(DS, SQL);
                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    m_ID = int.Parse(DS.Tables[0].Rows[0]["ID"].ToString());

                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message + " Hatasql:" + SQL);
            }
            finally
            {
                DS.Dispose();
            }
            return 0;
        }
		
		
		
		
		
		
         private int KaydetUpdate()
        {
            string SQL = null;

            SQL = "UPDATE PersonelTBL SET ";
            SQL += "RolID=  " + RolID + " ,";
            SQL += "PersonelAd='" + PersonelAd + "',";
            SQL += "PersonelSoyAd='" + PersonelSoyAd + "',";
            SQL += "TelNo='" + TelNo + "',";
            SQL += "Adres='" + Adres + "',";
            SQL += "EMail='" + EMail + "',";
            SQL += "IseGirisTarih='" + IseGirisTarih.ToString("yyyy/dd/MM HH:mm") + " ',";
            SQL += "IstenCikisTarih='" + IstenCikisTarih.ToString("yyyy/dd/MM HH:mm") + " ',";
            SQL += "KullaniciAd='" + KullaniciAd + "',";
            SQL += "KullaniciSifre='" + KullaniciSifre + "'  ";
            SQL += " WHERE ID=" + m_ID;

            try
            {
                this.ExecuteSQL(SQL);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message + " Hatasql:" + SQL);
            }
            return 0;
        }
	
	
	
	
	
	
       public object Delete()
        {
            m_SQL = "Delete from PersonelTBL where ID=" + m_ID;
            this.ExecuteSQL(m_SQL);
            return true;
        }

 
	}
}	

