using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Bll;
namespace HardSoft.App.ORL
{
    public partial class CancelarPersona : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UsuarioActual"] == null)
            {
                Response.Redirect("http://www.corlquilmes.com.ar", false);
            }
            if (!Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
            {
                Response.Redirect("http://www.corlquilmes.com.ar", false);

            }
            if (!Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
            {
                Response.Redirect("http://www.hardsoft.com.ar/", false);

            }
        }

        [WebMethod()]

        public static List<Be.turnos> WsListar(string v_tipo, string v_valor)
        {
            try
            {
                List<Be.turnos> list = new List<Be.turnos>();
                list = Bll.BllTurnosMysql.DameInstancia().ListarTurPers( v_tipo, v_valor);
                HttpContext.Current.Session["Lista"] = list;
                return list;
            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static void WsCancelar(string v_id)
        {
            try
            {

                Bll.BllTurnosMysql.DameInstancia().tur_cancelarTur(v_id);
                
            }
            catch (Exception)
            {

                throw;
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                LocalReport localReport = new LocalReport();

                string strCurrentDir = Server.MapPath(".") + "\\Report\\turno.rdlc";

                localReport.ReportPath = strCurrentDir;

                ReportDataSource reportDataSource = new ReportDataSource("DataSet1", Bll.BllTurnosMysql.DameInstancia().Rp_tur( HddId.Value));



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
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=TurnoQuilmes." + fileNameExtension);
                Response.BinaryWrite(renderedBytes);
                Response.End();



            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}