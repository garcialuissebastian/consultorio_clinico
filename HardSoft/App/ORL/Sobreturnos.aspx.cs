using Microsoft.Reporting ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bll;
using Microsoft.Reporting.WinForms;
namespace HardSoft.App.ORL
{
    public partial class Sobreturnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {// el evento desde jquery js no anda solo asi
                TxtDoc.Attributes.Add("onblur", "existe();");

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
                     
                    List<Be.Combos> lisSer = Bll.BllTurnosMysql.DameInstancia().CmdEspecialidade();
                    foreach (var item in lisSer)
                    {
                        ListItem t1 = new ListItem();
                        t1.Text = item.descripcion;
                        t1.Value = item.valor.ToString();
                        CmbEspecialidad.Items.Add(t1);


                    }

                    lisSer = Bll.BllTurnosMysql.DameInstancia().CmdProfesinal(CmbEspecialidad.SelectedValue);
                    cmbProf.ClearSelection();
                    cmbProf.Items.Clear();
                    foreach (var item in lisSer)
                    {
                        ListItem t1 = new ListItem();
                        t1.Text = item.descripcion;
                        t1.Value = item.valor.ToString();
                        cmbProf.Items.Add(t1);

                    }



                    lisSer = Bll.BllTurnosMysql.DameInstancia().CmdObraSociales("");
                    cmbObraSocial.ClearSelection();
                    cmbObraSocial.Items.Clear();
                    CmbOSR.ClearSelection();
                    CmbOSR.Items.Clear();
                    foreach (var item in lisSer)
                    {
                        ListItem t1 = new ListItem();
                        t1.Text = item.descripcion;
                        t1.Value = item.descripcion.ToString();
                        cmbObraSocial.Items.Add(t1);
                        CmbOSR.Items.Add(t1);
                    }




                }
            }
            catch (Exception)
            {

                Response.Redirect("http://www.hardsoft.com.ar/", false);
            }
        }
        protected void CmbEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<Be.Combos> lisSer = Bll.BllTurnosMysql.DameInstancia().CmdProfesinal(CmbEspecialidad.SelectedValue);
            cmbProf.ClearSelection();
            cmbProf.Items.Clear();
            foreach (var item in lisSer)
            {
                ListItem t1 = new ListItem();
                t1.Text = item.descripcion;
                t1.Value = item.valor.ToString();
                cmbProf.Items.Add(t1);
            }



        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {




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

        protected void BtnReservar_Click(object sender, EventArgs e)
        {
            try
            {
                Be.turnos tur = new Be.turnos();
                tur.Fecha_Nac = TxtNac.Text;
                tur.fecha = TxtFechaHasta.Text;
                tur.SERVICIO = CmbEspecialidad.SelectedValue;
                tur.PROFESIONAL = cmbProf.SelectedValue;
                tur.hora = TxtHora.Text + ":" + TxtMin.Text;
                tur.NOMBRE = TxtNombre.Text.ToUpper(); ;
                tur.APELLIDO = TxtApellido.Text.ToUpper(); ;
                tur.DOCUMENTO = TxtDoc.Text;
                tur.OBRASOCIAL = cmbObraSocial.SelectedValue;
                tur.TEL = TxtCel.Text.ToUpper();
                tur.MAIL = txtMail.Text.ToUpper();
                tur.OBS = txtObs.Text.ToUpper();
                tur.OBS_Pac = txtObsHC.Text.ToUpper();
                tur.NroOS = TxtNroOS.Text;
                tur.esPaciente = Hddexiste.Value;

                tur.id= Convert.ToInt32( Bll.BllTurnosMysql.DameInstancia().SobreTurno(tur));
                Session["Turno"] = tur;
                Hddexiste.Value = "";

                TxtFechaHasta.Text = "";
                TxtHora.Text = "00";
                TxtMin.Text = "00";
                TxtNombre.Text = "";
                TxtApellido.Text = "";
                TxtDoc.Text = "";
                TxtCel.Text = "";
                txtMail.Text = "";
                txtObs.Text = "";
                txtObsHC.Text = "";

                Pnl.Visible = true;
                TxtApellidoR.Text = ((Be.turnos)Session["Turno"]).APELLIDO;
                TxtTelR.Text = ((Be.turnos)Session["Turno"]).TEL;
                TxtDocumentoR.Text = ((Be.turnos)Session["Turno"]).DOCUMENTO;
                TxtMailR.Text = ((Be.turnos)Session["Turno"]).MAIL;
                TxtNombreR.Text = ((Be.turnos)Session["Turno"]).NOMBRE;
                TxtTurno.Text = ((Be.turnos)Session["Turno"]).fecha + " - " + ((Be.turnos)Session["Turno"]).hora;
                lblEsp.Text = CmbEspecialidad.SelectedItem.ToString() + " - " + cmbProf.SelectedItem.ToString();


                CmbOSR.SelectedItem.Text = ((Be.turnos)Session["Turno"]).OBRASOCIAL;
                TxtApellidoR.Enabled = false;
                TxtTelR.Enabled = false;
                TxtDocumentoR.Enabled = false;
                TxtMailR.Enabled = false;
                TxtNombreR.Enabled = false;
                TxtTurno.Enabled = false;
                CmbOSR.Enabled = false;


                //LocalReport localReport = new LocalReport();

                //string strCurrentDir = Server.MapPath(".") + "\\Report\\turno.rdlc";

                //localReport.ReportPath = strCurrentDir;

                //ReportDataSource reportDataSource = new ReportDataSource("DataSet1", Bll.BllTurnosMysql.DameInstancia().Rp_tur(ids));



                //localReport.DataSources.Add(reportDataSource);
                //string reportType = "PDF";
                //string mimeType;
                //string encoding;
                //string fileNameExtension;
                //string deviceInfo =
                //"<DeviceInfo>" +
                //"  <OutputFormat>PDF</OutputFormat>" +
                //"  <PageWidth>21cm</PageWidth>" +
                //"  <PageHeight>29.7cm</PageHeight>" +
                //"  <MarginTop>0.5in</MarginTop>" +
                //"  <MarginLeft>0.5in</MarginLeft>" +
                //"  <MarginRight>0.5in</MarginRight>" +
                //"  <MarginBottom>0.5in</MarginBottom>" +
                //"</DeviceInfo>";
                //Warning[] warnings;
                //string[] streams;
                //byte[] renderedBytes;
                ////Render the report
                //renderedBytes = localReport.Render(
                //    reportType,
                //    deviceInfo,
                //    out mimeType,
                //    out encoding,
                //    out fileNameExtension,
                //    out streams,
                //    out warnings);
                //Response.Clear();
                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "attachment; filename=TurnoQuilmes." + fileNameExtension);
                //Response.BinaryWrite(renderedBytes);
             
                //Response.End();


             
            }
            catch (Exception)
            {

                
            }
            finally
            {
               
            }
        }

    
    }
}