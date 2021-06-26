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
using Be;
namespace HardSoft.App.ORL
{
    public partial class AsistenciaHc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (HttpContext.Current.Session["UsuarioActual"] == null)
                {
                    Response.Redirect("http://www.hardsoft.com.ar/login_orl.aspx", false);
                }
               

                if (!Page.IsPostBack)
                {
                    string SAL = "";
                    Be.mysql_usuarios user = (Be.mysql_usuarios)HttpContext.Current.Session["UsuarioActual"];
                    if(user.USUARIO !="ORL"){

                        if (user.USUARIO != "ELIASJZACARIAS@GMAIL.COM")
                        {
                            SAL = user.ID_USUARIO;
                        } 
                      
                    }

                

                    List<Be.Combos> lisSer = Bll.BllTurnosMysql.DameInstancia().CmdEspecialidadeHc(SAL);
                    foreach (var item in lisSer)
                    {
                        ListItem t1 = new ListItem();
                        t1.Text = item.descripcion;
                        t1.Value = item.valor.ToString();
                      
                        //if (Request.QueryString["serv"] != null)
                        //{
                        //    if (t1.Value == Request.QueryString["serv"].ToString())
                        //    {
                        //        CmbEspecialidad.Items.Add(t1);

                        //    }
                        //}
                        //else
                        //{
                           // CmbEspecialidad.SelectedValue = Request.QueryString["serv"].ToString();
                            CmbEspecialidad.Items.Add(t1);
                      //  }
                    }

                    lisSer = Bll.BllTurnosMysql.DameInstancia().CmdProfesinalhC(CmbEspecialidad.SelectedValue, SAL);
                        cmbProf.ClearSelection();
                        cmbProf.Items.Clear();
                        foreach (var item in lisSer)
                        {
                            ListItem t1 = new ListItem();
                            t1.Text = item.descripcion;
                            t1.Value = item.valor.ToString();

                            //if (Request.QueryString["prof"] != null)
                            //{
                            //    if (t1.Value == Request.QueryString["prof"].ToString())
                            //    {
                            //        cmbProf.Items.Add(t1);

                            //    }
                            //}
                            //else
                            //{
                                cmbProf.Items.Add(t1);
                         //   }
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

                    string strCurrentDir = Server.MapPath(".") + "\\Report\\";
                    string strFile = "report_o.xlsx";

                    DataSet dt = new DataSet();
                    // ExportTextFile();
                    //   ExportToExcel();

                    // ExportToXLSX(ListasA);
                    //  dt.Tables.Add(exel.ConvertToDataTable(list));
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

                    dt.Tables.Add(exel.ConvertToDataTable(list, noo));

                    CreateExcelFile.CreateExcelDocument(dt, strCurrentDir + strFile);
                    Response.Redirect("Report/" + strFile, false);
                    //createDataInExcel(dt);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
         

        protected void CmbEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SAL = "";
            Be.mysql_usuarios user = (Be.mysql_usuarios)HttpContext.Current.Session["UsuarioActual"];
            if (user.USUARIO != "ORL")
            {
                SAL = user.ID_USUARIO;
            }


            List<Be.Combos>
              lisSer = Bll.BllTurnosMysql.DameInstancia().CmdProfesinalhC(CmbEspecialidad.SelectedValue, SAL);
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
        public static void WsDeleteHc(Int32 v_id)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().EliminarHc(v_id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        

               [WebMethod()]
        public static Be.turnos  WTurAsis_byPac(string v_doc)
        {
            try
            {
           return     Bll.BllTurnosMysql.DameInstancia().TurAsis_byPac(v_doc);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        [WebMethod()] 
        public static void   WsAltaHc(Be.hosp_hc v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().AltaHc(v_obj);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        [WebMethod()]
        public static void WsModificarHc(Be.hosp_hc v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().ModificacionHc(v_obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod()]
        public static void WsCerrarHc(string v_pac, string obs)
        {
            try
            {
             
           Bll.BllTurnosMysql.DameInstancia().cerrarHc(v_pac, obs);
                
            }
            catch (Exception)
            {

                throw;
            }


        }
        
        [WebMethod()]


        public static List<Be.hosp_hc> WsListarHc(string v_pac)
        {
            try
            {
                List<Be.hosp_hc> list = new List<Be.hosp_hc>();
                
                list = Bll.BllTurnosMysql.DameInstancia().ListarHc(v_pac);
                HttpContext.Current.Session["ListaHc"] = list;
                return list;
            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static Be.hosp_hc WsBuscarHc(string v_Id)
        {
            try
            {
                Be.hosp_hc sal = new Be.hosp_hc();
                List<Be.hosp_hc> list = new List<Be.hosp_hc>();
                

                sal = Bll.BllTurnosMysql.DameInstancia().ListarHc_id(v_Id);
                return sal;
            }
            catch (Exception)
            {

                throw;
            }

        }

        
        [WebMethod()]

        public static void WsMonto(string v_vino, string v_monto,string  v_copago, string v_obs, string ids, string v_prac, string v_consulta)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().tur_monto( v_vino,  v_monto, v_copago,  v_obs,   ids,v_prac, v_consulta);
 
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
        protected void BtnDescargarVestibular_Click (object sender, EventArgs e)
        {
            try
            {

                LocalReport localReport = new LocalReport();

                string strCurrentDir = Server.MapPath(".") + "\\Report\\vestibular.rdlc";

                localReport.ReportPath = strCurrentDir;

                DataTable dt = Bll.BllTurnosMysql.DameInstancia().Rp_turListarVestibular(txtidvisticular.Text);

                if (dt.Rows.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource("vestibular", dt);


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
                    Response.AddHeader("content-disposition", "attachment; filename=ExamenVestibular." + fileNameExtension);
                    Response.BinaryWrite(renderedBytes);
                    Response.End();

                }
                else
                {
                    LblMsg.Text = "No posee pdf. !";
                }


            }
            catch (Exception ex)
            {

                LblMsg.Text = ex.Message;
            }
        
        }

        protected void BtnDescargar_Click(object sender, EventArgs e)
        {
            try
            {




                LocalReport localReport = new LocalReport();

                string strCurrentDir = Server.MapPath(".") + "\\Report\\listado.rdlc";

                localReport.ReportPath = strCurrentDir;

                string pp = "";
                //if (cmbProf.SelectedValue == "2")
                //{
                //    pp = "1";
                //}
                //else
                //{
                //    pp = cmbProf.SelectedValue;
                //}
                string tur = "";
             
                    tur = "T";
              
                string dd =  TxtFechaHasta.Text;

                if (dd != "")
                {
                    DataTable dt = Bll.BllTurnosMysql.DameInstancia().Rp_turListar(cmbProf.SelectedValue, CmbEspecialidad.SelectedValue, dd, tur,"");

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
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "attachment; filename=TurnoQuilmes." + fileNameExtension);
                        Response.BinaryWrite(renderedBytes);
                        Response.End();

                    }
                    else
                    {
                        LblMsg.Text = "No posee turnos. !";
                    }

                }
                else
                {
                    LblMsg.Text = "Seleccione un dia";
                }
            }
            catch (Exception ex)
            {

                LblMsg.Text = ex.Message;
            }
        }

        [WebMethod()]


        public static void WmGvMod_hosp_examen_vestibular(hosp_examen_vestibular v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().Modificacion_hosp_examen_vestibular(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        } 
        [WebMethod()]


        public static void WmGvAlta_vestibular(hosp_examen_vestibular v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().Alta_hosp_examen_vestibular(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        } 


        [WebMethod()]


        public static List<hosp_examen_vestibular> WsListar_hosp_examen_vestibular(string v_tipo, string v_valor, string v_pac)
        {
            try
            {
                List<hosp_examen_vestibular> list = new List<hosp_examen_vestibular>();

                list = Bll.BllTurnosMysql.DameInstancia().Listar_hosp_examen_vestibular(v_tipo, v_valor.ToUpper().Trim(),v_pac);

                HttpContext.Current.Session["Lista"] = list;

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [WebMethod()]


        public static void WmGvDel_hosp_examen_vestibular(Int32 v_Id)
        {
            try
            {

                Bll.BllTurnosMysql.DameInstancia().Eliminar_hosp_examen_vestibular(v_Id);

            }

            catch (Exception)
            {

                throw;

            }

        } 

 
    }
}