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
using System.Globalization;
namespace HardSoft.App.ORL
{
    public partial class Admin_tur  : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              if (HttpContext.Current.Session["UsuarioActual"] == null) {
                Response.Redirect("http://www.corlquilmes.com.ar", false);
            }
            if (!Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
            {
                Response.Redirect("http://www.corlquilmes.com.ar", false);

            }

            if (!Page.IsPostBack)
            {
                ck2.Checked = true;
                if (!Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
                {
                    Response.Redirect("http://www.hardsoft.com.ar/", false);

                }
                lblcanc.Text = "";
                List<Be.Combos> lisSer = Bll.BllTurnosMysql.DameInstancia().CmdEspecialidade();
                foreach (var item in lisSer)
                {
                    ListItem t1 = new ListItem();
                    t1.Text = item.descripcion;
                    t1.Value = item.valor.ToString();
                    CmbEspecialidad.Items.Add(t1);
                    CmbEspecialidad1.Items.Add(t1);
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
                    cmbProf1.Items.Add(t1);
                }
                Session["Dias"] = "";
                Session["Dias1"] = "";
                Session["s_fecha"] = "";
                Session["s_fecha1"] = "";
                string sal = Bll.BllTurnosMysql.DameInstancia().TurnosDispReg(cmbProf.SelectedValue, CmbEspecialidad.SelectedValue); // lv y sab

                Session["Dias"] = sal; // dias disponibles canlendario -- devuelve los dias dispobles

            }







        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {


            string sal = Session["Dias"].ToString();
            string[] namesArray = sal.Split(',');
            CultureInfo provider = CultureInfo.InvariantCulture;
            string ss = "n";

            for (int i = 0; i < namesArray.Length; i++)
            {
                if (namesArray[i] != "") // que no sea el fin , ""
                {
                    // en el aray estan los solo dias dispobles
                    DateTime dateTime = DateTime.ParseExact(namesArray[i], "dd/MM/yyyy", provider);
                    if (e.Day.Date == dateTime)  // comparo el dia del array con el dia q se va cargar en el calendario si existe no lo dehabilito
                    {
                        ss = "s";
                    }


                }
            }
            if (ss == "n")
            {// deshabilito pq no esta en el array
                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                e.Day.IsSelectable = false;
            }

            //if ((e.Day.Date.DayOfWeek == DayOfWeek.Monday) || (e.Day.Date.DayOfWeek == DayOfWeek.Wednesday) || (e.Day.Date.DayOfWeek == DayOfWeek.Friday))
            //{
            //    if (e.Day.Date <= DateTime.Today)
            //    {
            //        e.Cell.BackColor = System.Drawing.Color.Gray;
            //        e.Day.IsSelectable = false;
            //    }// action
            //}




        }


        protected void Button1_Click(object sender, EventArgs e)
        {


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

            Session["Dias"] = "";

            Session["s_fecha"] = "";

            string sal = Bll.BllTurnosMysql.DameInstancia().TurnosDispReg(cmbProf.SelectedValue, CmbEspecialidad.SelectedValue); // lv y sab

            Session["Dias"] = sal; // dias disponibles canlendario -- devuelve los dias dispobles


        }

        protected void CmbEspecialidad_SelectedIndexChanged1(object sender, EventArgs e)
        {

            List<Be.Combos> lisSer = Bll.BllTurnosMysql.DameInstancia().CmdProfesinal(CmbEspecialidad1.SelectedValue);
            cmbProf1.ClearSelection();
            cmbProf1.Items.Clear();
            foreach (var item in lisSer)
            {
                ListItem t1 = new ListItem();
                t1.Text = item.descripcion;
                t1.Value = item.valor.ToString();
                cmbProf1.Items.Add(t1);
            }

            Session["Dias"] = "";

            Session["s_fecha"] = "";

          

        }

        protected void cmbProf_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Dias"] = "";

            Session["s_fecha"] = "";

            string sal = Bll.BllTurnosMysql.DameInstancia().TurnosDispReg(cmbProf.SelectedValue, CmbEspecialidad.SelectedValue); // lv y sab

            Session["Dias"] = sal; // dias disponibles canlendario -- devuelve los dias dispobles


        }


        protected void cmbProf_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Session["Dias"] = "";

            Session["s_fecha"] = "";

            string sal = Bll.BllTurnosMysql.DameInstancia().TurnosDispReg(cmbProf1.SelectedValue, CmbEspecialidad1.SelectedValue); // lv y sab

            Session["Dias"] = sal; // dias disponibles canlendario -- devuelve los dias dispobles


        }
        // btn cancelar
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Be.turnos tt = new Be.turnos();

                tt.id = Convert.ToInt32(Session["ids"].ToString());
                string sal = Bll.BllTurnosMysql.DameInstancia().Reservar(tt);
                tt.SERVICIO = CmbEspecialidad.SelectedItem.Text + " - " + cmbProf.SelectedItem.Text;
                tt.fecha = Session["Turno"].ToString();
                //   Pnl.Visible = false;
                Session["Turno"] = tt;
                if (sal == "S")
                {

                }
                else
                {
                    Response.Redirect("ReservarEnd.aspx", false);
                }






            }
            catch (Exception ex)
            {


            }
        }



        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                
 


                LocalReport localReport = new LocalReport();

                string strCurrentDir = Server.MapPath(".") + "\\Report\\listado.rdlc";

                localReport.ReportPath = strCurrentDir;

                string pp = "";
                if (cmbProf.SelectedValue == "2")
                {
                    pp = "1";
                }
                else
                {
                    pp = cmbProf.SelectedValue;
                }
                string tur = "";
                if (ck1.Checked)
                {
                    tur = "M";
                }
                if (ck2.Checked)
                {
                    tur= "T";
                }
                DataTable dt = Bll.BllTurnosMysql.DameInstancia().Rp_turListar(pp,CmbEspecialidad.SelectedValue,TxtFechaHasta.Text,tur,"");

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
                else {
                    lblimp.Text = "No posee turnos. !"; 
                }

            }
            catch (Exception ex)
            {

                lblimp.Text = ex.Message;
            }
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().CancelarFecha(CmbEspecialidad1.SelectedValue.ToString(), cmbProf1.SelectedValue.ToString(), Txtdia1.Text);
                Txtdia1.Text = "";
                lblcanc.Text = "Cancelado!!";
            }
            catch (Exception ex)
            {

                lblcanc.Text = ex.Message;
            }
        }
  
    
    
 
    }
}