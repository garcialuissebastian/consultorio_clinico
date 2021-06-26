using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HardSoft.App.ORL
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LocalReport localReport = new LocalReport();

                //string strCurrentDir = Server.MapPath(".") + "\\Report\\hc.rdlc";


                localReport.ReportEmbeddedResource = "HardSoft.App.ORL.Report.hc.rdlc";


                //localReport.ReportPath = strCurrentDir;
                DataSet ds = Bll.BllTurnosMysql.DameInstancia().Rp_Hc("22");
                ReportDataSource reportDataSource = new ReportDataSource("cab", ds.Tables["cab"]);
                ReportDataSource reportDataSource1 = new ReportDataSource("det", ds.Tables["det"]);


                localReport.DataSources.Add(reportDataSource1);
                localReport.DataSources.Add(reportDataSource);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>29.7cm</PageWidth>" +
                "  <PageHeight>21cm</PageHeight>" +
                "  <MarginTop>0.2in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.5in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;
                //Render the report
                renderedBytes = localReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                //Response.Clear();
                //Response.ContentType = mimeType;



                  HttpContext.Current.Session["PDF"]= Convert.ToBase64String(renderedBytes);

              
            }
            catch (Exception ex)
            {

                throw;
            }

         
        }


        [WebMethod()]

        public static string rp()
        {
            try
            {
                LocalReport localReport = new LocalReport();

                //string strCurrentDir = Server.MapPath(".") + "\\Report\\hc.rdlc";


                localReport.ReportEmbeddedResource = "HardSoft.App.ORL.Report.hc.rdlc";


                //localReport.ReportPath = strCurrentDir;
                DataSet ds = Bll.BllTurnosMysql.DameInstancia().Rp_Hc("22");
                ReportDataSource reportDataSource = new ReportDataSource("cab", ds.Tables["cab"]);
                ReportDataSource reportDataSource1 = new ReportDataSource("det", ds.Tables["det"]);


                localReport.DataSources.Add(reportDataSource1);
                localReport.DataSources.Add(reportDataSource);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>29.7cm</PageWidth>" +
                "  <PageHeight>21cm</PageHeight>" +
                "  <MarginTop>0.2in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.5in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;
                //Render the report
                renderedBytes = localReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                //Response.Clear();
                //Response.ContentType = mimeType;



                string salida = "data:" + mimeType + ";base64," + Convert.ToBase64String(renderedBytes);

                return salida;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}