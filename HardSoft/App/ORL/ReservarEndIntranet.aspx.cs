using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HardSoft.App.ORL
{
    public partial class ReservarEndIntranet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session["UsuarioActual"] == null)
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);
                }
                if (!Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);

                }
          
            if (!Page.IsPostBack)
            {
                List<Be.Combos> lisSer;
                lisSer = Bll.BllTurnosMysql.DameInstancia().CmdObraSociales("");
                cmbObraSocial.ClearSelection();
                cmbObraSocial.Items.Clear();

                foreach (var item in lisSer)
                {
                    ListItem t1 = new ListItem();
                    t1.Text = item.descripcion;
                    t1.Value = item.descripcion.ToString();
                    cmbObraSocial.Items.Add(t1);
                }
                TxtApellido.Text = ((Be.turnos)Session["Turno"]).APELLIDO;
                TxtCel.Text = ((Be.turnos)Session["Turno"]).TEL;
                TxtDoc.Text = ((Be.turnos)Session["Turno"]).DOCUMENTO;
                txtMail.Text = ((Be.turnos)Session["Turno"]).MAIL;
                TxtNombre.Text = ((Be.turnos)Session["Turno"]).NOMBRE;
                TxtTurno.Text = ((Be.turnos)Session["Turno"]).fecha;
                lblEsp.Text = ((Be.turnos)Session["Turno"]).SERVICIO;
                cmbObraSocial.SelectedItem.Text = ((Be.turnos)Session["Turno"]).OBRASOCIAL;
                TxtApellido.Enabled = false;
                TxtCel.Enabled = false;
                 TxtDoc.Enabled = false;
                 txtMail.Enabled = false;
                 TxtNombre.Enabled = false;
                 TxtTurno.Enabled = false;
                 cmbObraSocial.Enabled = false;

            }
            }
            catch (Exception)

            {

                Response.Redirect("http://www.corlquilmes.com.ar", false);
            }


        }

      
   
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Be.turnos tt = new Be.turnos();
                tt.NOMBRE = TxtNombre.Text.ToUpper();
                tt.APELLIDO = TxtApellido.Text.ToUpper();
                tt.DOCUMENTO = TxtDoc.Text.ToUpper();
               
 
                tt.TEL = TxtCel.Text.ToUpper();
                tt.MAIL = txtMail.Text.ToUpper();
                tt.id = Convert.ToInt32(Session["ids"].ToString());
                string sal = Bll.BllTurnosMysql.DameInstancia().Reservar(tt);

                Pnl.Visible = false;

                if (sal == "S")
                {
                    lblmsg2.Text = "El turno fué reservado por otra persona elija otro horario. ";
                }


               


                LocalReport localReport = new LocalReport();

                string strCurrentDir = Server.MapPath(".") + "\\Report\\turno.rdlc";

                localReport.ReportPath = strCurrentDir;

                ReportDataSource reportDataSource = new ReportDataSource("DataSet1", Bll.BllTurnosMysql.DameInstancia().Rp_tur(tt.id.ToString()));



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
                Response.AddHeader("content-disposition", "attachment; filename=Turno." + fileNameExtension);
                Response.BinaryWrite(renderedBytes);

          
                Response.Flush();
               
                
                Response.Close();

             



            }
            catch (Exception ex)
            {

                lblmsg2.Text = ex.Message;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try {




                LocalReport localReport = new LocalReport();

                string strCurrentDir = Server.MapPath(".") + "\\Report\\turno.rdlc";

                localReport.ReportPath = strCurrentDir;

                ReportDataSource reportDataSource = new ReportDataSource("DataSet1", Bll.BllTurnosMysql.DameInstancia().Rp_tur(((Be.turnos)Session["Turno"]).id.ToString()));



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
                Response.AddHeader("content-disposition", "attachment; filename=Turno." + fileNameExtension);
                Response.BinaryWrite(renderedBytes);

 


                Response.End();

               



            }
            catch (Exception ex)
            {

                lblmsg2.Text = ex.Message;
            }
        }
    }
}