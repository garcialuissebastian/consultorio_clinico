using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Reporting.WebForms;
using System.Data;
using Newtonsoft.Json;
using System.Text;

namespace HardSoft.Services
{
    /// <summary>
    /// Descripción breve de PDFMOVIL
    /// </summary>
    public class PDFMOVIL : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {

            try
            {
                context.Response.Clear(); 
                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.Cache.SetExpires(DateTime.MinValue);
                context.Response.Cache.SetNoStore();
                string data = string.Empty;

                try
                {

                    //HttpContext.Current.Session["UsuarioActual"]; 

                    if (HttpContext.Current.Session["PDF1"] == null)
                    {
                        HttpContext.Current.Session["PDF1"] = "";
                    }





                    LocalReport localReport = new LocalReport();

                    string strCurrentDir = "HardSoft.App.ORL.Report.listado.rdlc";
                    localReport.ReportEmbeddedResource = strCurrentDir;



                    DataTable dt = Bll.BllTurnosMysql.DameInstancia().Rp_turListar("1", "1", "25/09/2018", "T","");
                    string salida = "";
                    if (dt.Rows.Count > 0)
                    {
                        ReportDataSource reportDataSource = new ReportDataSource("turno", dt);


                        localReport.DataSources.Add(reportDataSource);
                        string reportType = "PDF";
                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string deviceInfo =
                        "<DeviceInfo>" +
                        "  <OutputFormat>PDF</OutputFormat>" +
                        "  <PageWidth>21cm</PageWidth>" +
                        "  <PageHeight>29.7cm</PageHeight>" +
                        "  <MarginTop>0.5in</MarginTop>" +
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
                        context.Response.Clear();
                        //context.Response.ContentType = mimeType;
                        //Response.AddHeader("content-disposition", "attachment; filename=TurnoQuilmes." + fileNameExtension);
                        //Response.BinaryWrite(renderedBytes);
                        //Response.End();
                        salida = Convert.ToBase64String(renderedBytes);


                    }

                    StringBuilder json = new StringBuilder();



                    json.Append("{");
                    json.Append("\"");
                    json.Append("existe");
                    json.Append("\" :\"");
                    json.Append(salida);
                    json.Append("\"");
                    json.Append("}");


                    json.ToString();

                    data = json.ToString();

                    if (data != string.Empty)
                    {
                        context.Response.Write(data);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}