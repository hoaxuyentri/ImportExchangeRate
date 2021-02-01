using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Data.SqlClient;

namespace ImportExchangeRate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {

                XmlDocument xml = new XmlDocument();
                xml.Load("https://portal.vietcombank.com.vn/Usercontrols/TVPortal.TyGia/pXML.aspx");

                string cnnstr;
                SqlConnection cnn;
                cnnstr = "Data Source=LAPTOP310\\SQLLUANTT;Initial Catalog='';Integrated Security=True";
                cnn = new SqlConnection(cnnstr);
                cnn.Open();

                XmlNodeList DateTimexml;
                DateTimexml = xml.SelectNodes("/ExrateList/DateTime");

                XmlNodeList Dataxml;
                Dataxml = xml.SelectNodes("/ExrateList/Exrate");

                for (int i = 0; i <= Dataxml.Count - 1; i++)
                {
                    SqlDataAdapter da = new SqlDataAdapter("INSERT INTO TEST..VCB_EXCHANGE_RATE(DATETIME, CODE, NAME, BUY, TRANSFER, SELL) values('" + DateTimexml.Item(0).InnerText + "','" + Dataxml.Item(i).Attributes["CurrencyCode"].InnerText + "','" + Dataxml.Item(i).Attributes["CurrencyName"].InnerText + "'," + Double.Parse(Dataxml.Item(i).Attributes["Buy"].InnerText.Replace("-", "0")) + "," + Double.Parse(Dataxml.Item(i).Attributes["Transfer"].InnerText.Replace("-", "0")) + "," + Double.Parse(Dataxml.Item(i).Attributes["Sell"].InnerText.Replace("-", "0")) + ")", cnn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                }

                //MessageBox.Show("Data were added to sql server successful!");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
