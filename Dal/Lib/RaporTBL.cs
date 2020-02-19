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

    public class RaporTBL : DataAccesBase
    {

        string m_SQL;
        int m_ConCount;
		int m_ID; 


        public Int32 PersonelID;
        public System.DateTime RaporBaslangicTarih = Convert.ToDateTime("1900/01/01 00:00");
        public System.DateTime RaporBitisTarih = Convert.ToDateTime("1900/01/01 00:00");
        public String YapilanProje;
        public String Yapilanlar;
        public String Sorunlar;
        public String KullanilanMalzeme;
        public String Oneriler;
        public Int32 DurumID;
        public String RedSebep;
		    


        public int ID
        {
            get { return m_ID; }
        }

        public RaporTBL ()
        {
        }
        public RaporTBL (int pID)
        {
            m_SQL = "Select * from RaporTBL where ID=" + pID;
            initialize();
        }

        public RaporTBL(string pFIELD_NAME, string pVALUE)
        {
            m_SQL = "Select * from RaporTBL where " + pFIELD_NAME + "='" + pVALUE + "'";
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
                PersonelID = Convert.ToInt32(DT.Rows[0]["PersonelID"]);
                RaporBaslangicTarih = Convert.ToDateTime(DT.Rows[0]["RaporBaslangicTarih"]);
                RaporBitisTarih = Convert.ToDateTime(DT.Rows[0]["RaporBitisTarih"]);
                YapilanProje = Convert.ToString(DT.Rows[0]["YapilanProje"]);
                Yapilanlar = Convert.ToString(DT.Rows[0]["Yapilanlar"]);
                Sorunlar = Convert.ToString(DT.Rows[0]["Sorunlar"]);
                KullanilanMalzeme = Convert.ToString(DT.Rows[0]["KullanilanMalzeme"]);
                Oneriler = Convert.ToString(DT.Rows[0]["Oneriler"]);
                DurumID = Convert.ToInt32(DT.Rows[0]["DurumID"]);
                RedSebep = Convert.ToString(DT.Rows[0]["RedSebep"]);

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

			SQL="Insert Into RaporTBL (PersonelID, RaporBaslangicTarih, RaporBitisTarih, YapilanProje, ";
            SQL += "Yapilanlar, Sorunlar, KullanilanMalzeme, Oneriler, ";
            SQL += "DurumID, RedSebep) values (";
            SQL += "  " + PersonelID + " ,";
            SQL += "'" + RaporBaslangicTarih.ToString("yyyy/dd/MM HH:mm") + " ',";
            SQL += "'" + RaporBitisTarih.ToString("yyyy/dd/MM HH:mm") + " ',";
            SQL += "'" + YapilanProje + "',";
            SQL += "'" + Yapilanlar + "',";
            SQL += "'" + Sorunlar + "',";
            SQL += "'" + KullanilanMalzeme + "',";
            SQL += "'" + Oneriler + "',";
            SQL += "  " + DurumID + " ,";
            SQL += "'" + RedSebep + "'  ";
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

            SQL = "UPDATE RaporTBL SET ";
            SQL += "PersonelID=  " + PersonelID + " ,";
            SQL += "RaporBaslangicTarih='" + RaporBaslangicTarih.ToString("yyyy/dd/MM HH:mm") + " ',";
            SQL += "RaporBitisTarih='" + RaporBitisTarih.ToString("yyyy/dd/MM HH:mm") + " ',";
            SQL += "YapilanProje='" + YapilanProje + "',";
            SQL += "Yapilanlar='" + Yapilanlar + "',";
            SQL += "Sorunlar='" + Sorunlar + "',";
            SQL += "KullanilanMalzeme='" + KullanilanMalzeme + "',";
            SQL += "Oneriler='" + Oneriler + "',";
            SQL += "DurumID=  " + DurumID + " ,";
            SQL += "RedSebep='" + RedSebep + "'  ";
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
            m_SQL = "Delete from RaporTBL where ID=" + m_ID;
            this.ExecuteSQL(m_SQL);
            return true;
        }

 
	}
}	

