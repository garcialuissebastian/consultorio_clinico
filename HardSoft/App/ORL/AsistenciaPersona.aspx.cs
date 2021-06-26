using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Bll;
using System.Data;
using System.IO;
using System.Globalization;
namespace HardSoft.App.ORL
{
    public partial class AsistenciaPersona : System.Web.UI.Page
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

                if (!Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
                {
                    Response.Redirect("http://www.hardsoft.com.ar/", false);

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
                }
            }
            catch (Exception)
            {

                Response.Redirect("http://www.hardsoft.com.ar/", false);
            }
        }

    
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<Be.turnos> list = new List<Be.turnos>();
                 

                if (HttpContext.Current.Session["Listar"] != null)
                {
                    list = (List<Be.turnos>)HttpContext.Current.Session["Listar"];

                   DataSet dt = new DataSet();

                  
                    List<string> noo = new List<string>();
                    noo.Add("id");
                    noo.Add("CUIT");
                    noo.Add("FECHA");
                    noo.Add("SEXO");
                    noo.Add("DOMICILIO");
                    noo.Add("OBS");
                    noo.Add("PROFESIONAL");
                    //noo.Add("IdGrilla");
                    //noo.Add("ClienteSelect");
                  
                  
                    DataTable dts = exel.ConvertToDataTable(list, noo);

                    double sal =0.0;
                    double salCopago = 0.0;
                    foreach (var item in list)
                    {
                        sal += Convert.ToDouble(item.Pago.Replace(",", "."), CultureInfo.InvariantCulture);
                        salCopago += Convert.ToDouble(item.Copago.Replace(",", "."), CultureInfo.InvariantCulture);
                    }

                    DataTable datos = new DataTable();

                    DataColumn colDato1 = new DataColumn("total", typeof(String));
                    datos.Columns.Add(colDato1);
                    DataRow row1 = datos.NewRow();
                    row1["total"] = sal;

                    datos.Rows.Add(row1);


                    DataColumn colDato2 = new DataColumn("totalCopago", typeof(String));
                    datos.Columns.Add(colDato2);
                    DataRow row2 = datos.NewRow();
                    row1["totalCopago"] = salCopago;

                    datos.Rows.Add(row2);


                    LocalReport localReport = new LocalReport();

                 //   localReport.EnableExternalImages = true;

                    string strCurrentDir = Server.MapPath(".") + "\\Report\\asistencia.rdlc";
                    localReport.ReportPath = strCurrentDir;

                  //  localReport.ReportEmbeddedResource = "HardSoft.App.ORL.Report.asistencia.rdlc";
                  //  localReport.SetParameters(new ReportParameter("parametro", sal.ToString()));

 
                    ReportDataSource reportDataSource = new ReportDataSource("asistencia",dts);
                    ReportDataSource reportDataSource1 = new ReportDataSource("datos", datos);
                  
                   
                    localReport.DataSources.Add(reportDataSource);
                    localReport.DataSources.Add(reportDataSource1);
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
                    "  <MarginRight>0.1in</MarginRight>" +
                    "  <MarginBottom>0.1in</MarginBottom>" +
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
 
                    Response.AddHeader("content-disposition", "attachment; filename=AsistenciaQuilmes." + fileNameExtension);
                    Response.BinaryWrite(renderedBytes);
                    Response.End();
                }
            }
            catch (Exception)
            {

                throw;
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

        [WebMethod()]

        public static List<Be.turnos> WsListar(string v_serv, string v_prof, string v_fecha )
        {
            try
            {
                List<Be.turnos> list = new List<Be.turnos>();
              
                list = Bll.BllTurnosMysql.DameInstancia().ListarTurAsis( v_serv,   v_prof,  v_fecha );
                HttpContext.Current.Session["Listar"] = list;
                return list;
            }
            catch (Exception)
            {

                throw;
            }


        }


        [WebMethod()]

        public static void WsListarRp (string v_serv, string v_prof, string v_fecha, string v_fecha2)
        {
            try
            {
                List<Be.turnos> list = new List<Be.turnos>();

                list = Bll.BllTurnosMysql.DameInstancia().ListarTurAsisRp(v_serv, v_prof, v_fecha, v_fecha2);
                HttpContext.Current.Session["Listar"] = list;
            
            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static void WsMonto(string v_vino, string v_monto, string  v_copago, string v_obs, string ids, string v_prac, string v_consulta)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().tur_monto( v_vino,  v_monto,  v_obs,   ids,   v_prac,  v_consulta, v_copago);
 
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