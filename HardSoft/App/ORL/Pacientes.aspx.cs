using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bll;
using Be;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Globalization; // sin esto no anda los reporte en el hosting
namespace HardSoft.App.ORL
{
    public partial class Pacientes : System.Web.UI.Page

    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["SIS"] == "S")
                {
                    mysql_usuarios lista = Bllmysql_usuarios.DameInstancia().Obtener_usuario("ORL");
                    HttpContext.Current.Session["ActiveSession"] = DateTime.Now.ToString();
                    HttpContext.Current.Session["UsuarioActual"] = lista;
                }


                if (HttpContext.Current.Session["UsuarioActual"] == null)
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);
                }
               
                List<Be.Combos> lisSer = Bll.BllTurnosMysql.DameInstancia().CmdObraSociales("");
                cmbObraSocial.ClearSelection();
                cmbObraSocial.Items.Clear();

                foreach (var item in lisSer)
                {
                    ListItem t1 = new ListItem();
                    t1.Text = item.descripcion;
                    t1.Value = item.descripcion.ToString();
                    cmbObraSocial.Items.Add(t1);
                }
            }
            catch (Exception)
            {
                Response.Redirect("http://www.corlquilmes.com.ar", false);
             
            }
        


        }
       

      
        [WebMethod()]

        public static void WmMod2(Be.turnos v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().ModPaciente(v_obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod()]

        public static List<Be.provincia> WsListarProvincia(string v_valor)
        {
            try
            {
                List<Be.provincia> list = new List<Be.provincia>();

                list = Bll.BllDatosBasico.DameInstancia().ListarProvincias(v_valor);
                return list;
            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static List<Be.departamento> WsListarDepartamento(string v_valor)
        {
            try
            {
                List<Be.departamento> list = new List<Be.departamento>();

                list = Bll.BllDatosBasico.DameInstancia().ListarDepartamento(v_valor);
                return list;
            }
            catch (Exception)
            {

                throw;
            }


        }
        [WebMethod()]

        public static List<Be.distrito> WsListarDistrito(string v_valor)
        {
            try
            {
                List<Be.distrito> list = new List<Be.distrito>();

                list = Bll.BllDatosBasico.DameInstancia().ListarDistrito(v_valor);
                return list;
            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static List<Be.pais> WsListarP(string v_tipo, string v_valor)
        {
            try
            {
                List<Be.pais> list = new List<Be.pais>();

                list = Bll.BllDatosBasico.DameInstancia().ListarPais();

                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [WebMethod()]

        public static Be.turnos WsBuscarP(string v_Id)
        {
            try
            {
                Be.turnos sal = new turnos();
                List<Be.turnos> list = new List<Be.turnos>();
               
                    list = list = Bll.BllTurnosMysql.DameInstancia().ListarPaciente(v_Id, "99");
                    foreach (var item in list)
                    {
                        if (item.id.ToString() == v_Id)
                        {
                            sal = item;
                        }

                    }

              
                return sal;
            }
            catch (Exception)
            {

                throw;
            }

        }


        [WebMethod()]

        public static List<Be.turnos> WsListar(string v_tipo, string v_valor)
        {
            try
            {

                List<Be.turnos> list = new List<Be.turnos>();

                list = Bll.BllTurnosMysql.DameInstancia().ListarPaciente( v_valor, v_tipo);
                HttpContext.Current.Session["Lista"] = list;

                return list;


            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static void gvProv_del(string v_id)
        {
            try
            {

              //  Bllkx_cliente.DameInstancia().Eliminar(Convert.ToInt32(v_id));

            }
            catch (Exception)
            {

                throw;
            }



        }

        protected void BtnHc_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                  LocalReport localReport = new LocalReport();

                 //string strCurrentDir = Server.MapPath(".") + "\\Report\\hc.rdlc";


                 localReport.ReportEmbeddedResource = "HardSoft.App.ORL.Report.hc.rdlc";


                 //localReport.ReportPath = strCurrentDir;
                DataSet ds =  Bll.BllTurnosMysql.DameInstancia().Rp_Hc(TxtCodSistema.Text);
                 ReportDataSource reportDataSource = new ReportDataSource("cab",ds.Tables["cab"]);
                 ReportDataSource reportDataSource1 = new ReportDataSource("det",ds.Tables["det"]);


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
                 Response.Clear();
                 Response.ContentType = mimeType;


                 string salida = "data:" + mimeType + ";base64," + Convert.ToBase64String(renderedBytes);

                Response.AddHeader("content-disposition", "attachment; filename=HcQuilmes." + fileNameExtension);
                 Response.BinaryWrite(renderedBytes);
                 Response.End();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
      
        //hosp_hc_files

        [WebMethod()] 
        public static List<hosp_hc_files> WsListar_hosp_hc_files(string v_tipo, string v_valor)
        {
            try
            {
                List<hosp_hc_files> list = new List<hosp_hc_files>();

                list = Bll.BllTurnosMysql.DameInstancia().Listar_hosp_hc_files(v_tipo, v_valor.ToUpper().Trim());

                HttpContext.Current.Session["Lista"] = list;

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        [WebMethod()]
         
        public static void WmGvDel_hosp_hc_files(Int32 v_Id)
        {
            try
            {

                Bll.BllTurnosMysql.DameInstancia().Eliminar_hosp_hc_files(v_Id);

            }

            catch (Exception)
            {

                throw;

            }

        }

        [WebMethod()] 
        public static void WmGvMod_hosp_hc_files(hosp_hc_files v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().Modificacion_hosp_hc_files(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [WebMethod()] 
        public static void WmGvAlta_hosp_hc_files(hosp_hc_files v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().Alta_hosp_hc_files(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        } 

    
    }
}