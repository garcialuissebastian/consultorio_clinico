using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HardSoft.App.ORL
{
    public partial class ReservarIntranet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            TxtDoc.Attributes.Add("onblur", "existe();");

            if (HttpContext.Current.Session["UsuarioActual"] == null)
            {
                Response.Redirect("http://www.corlquilmes.com.ar", false);
            }
            if (!Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
            {
                Response.Redirect("http://www.corlquilmes.com.ar", false);

            }
            Pnl2.Visible = false;
            if (!Page.IsPostBack)
            {
                if (lblMsg.Text == "")
                {
                  //  Pnl2.Visible = true;
                
                }

               
                List<Be.Combos> lisSer = Bll.BllTurnosMysql.DameInstancia().CmdEspecialidade();
                foreach (var item in lisSer)
                { ListItem t1 = new ListItem();
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
              
                foreach (var item in lisSer)
                {
                    ListItem t1 = new ListItem();
                    t1.Text = item.descripcion;
                    t1.Value = item.descripcion.ToString();
                    cmbObraSocial.Items.Add(t1);
                }



                Session["Dias"] = "";
                gvSueldo.DataSource = null;
                gvSueldo.DataBind();
                Session["s_fecha"] = "";

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

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

            lblMsg.Text = "";


            Session["s_fecha"] = Calendar1.SelectedDate;

        
            try
            {
                string pp = "";
               
                    pp = cmbProf.SelectedValue;
               

                List<Be.turnos> lista = Bll.BllTurnosMysql.DameInstancia().listar_turnosReg(Calendar1.SelectedDate, pp, CmbEspecialidad.SelectedValue);

                gvSueldo.DataSource = lista;
                gvSueldo.DataBind();

                if (lista.Count < 1)
                {
                    lblMsg.Text = "No hay turnos para la fecha seleccionada " + Calendar1.SelectedDate.ToShortDateString();
                }


            }
            catch (Exception ex)
            {

                lblMsg.Text = ex.Message;
            }




            Pnl2.Visible = false;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            try
            {
                lblMsg.Text = "Seleccione un día y horario de atención.";

                foreach (GridViewRow row in gvSueldo.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked)
                        {
                            lblMsg.Text = "";
                            Int32 ids = Convert.ToInt32(row.Cells[1].Text);
                            Session["Turno"] = Convert.ToString(row.Cells[3].Text) + "-" + Convert.ToString(row.Cells[2].Text);
                            Session["ids"] = ids;
                            Pnl.Visible = true;
                           // Response.Redirect("Reserva_end.aspx", false);


                        }

                    }
                }
             



            }
            catch (Exception ex)
            {

                lblMsg.Text = ex.Message;
            }
        }

        protected void CmbEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calendar1.SelectedDates.Clear();
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
            gvSueldo.DataSource = null;
            gvSueldo.DataBind();
            Session["s_fecha"] = "";

            string pp="";
          
                pp = cmbProf.SelectedValue;
         
            string sal = Bll.BllTurnosMysql.DameInstancia().TurnosDispReg(pp, CmbEspecialidad.SelectedValue); // lv y sab

           


            Session["Dias"] = sal; // dias disponibles canlendario -- devuelve los dias dispobles

            Calendar1.DataBind();

        }

        protected void cmbProf_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calendar1.SelectedDates.Clear();

            Session["Dias"] = "";
            gvSueldo.DataSource = null;
            gvSueldo.DataBind();
            Session["s_fecha"] = "";

            string pp = "";
           
                pp = cmbProf.SelectedValue;
           

            string sal = Bll.BllTurnosMysql.DameInstancia().TurnosDispReg(pp, CmbEspecialidad.SelectedValue); // lv y sab

            Session["Dias"] = sal; // dias disponibles canlendario -- devuelve los dias dispobles

            Calendar1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Be.turnos tt = new Be.turnos();
                tt.NOMBRE = TxtNombre.Text.ToUpper();
                tt.APELLIDO = TxtApellido.Text.ToUpper();
                tt.DOCUMENTO = TxtDoc.Text.ToUpper();
                tt.OBRASOCIAL = cmbObraSocial.SelectedItem.Text;
                tt.NroOS = TxtNroOS.Text;
                tt.OBS = txtObs.Text.ToUpper();
                tt.TEL = TxtCel.Text.ToUpper();
                tt.MAIL = txtMail.Text.ToUpper();
                tt.Fecha_Nac = TxtNac.Text.ToUpper();
                tt.OBS_Pac= txtObsHC.Text.ToUpper();
                tt.esPaciente = Hddexiste.Value;
                tt.id = Convert.ToInt32(Session["ids"].ToString());
                string sal = Bll.BllTurnosMysql.DameInstancia().ReservarIntranet(tt);
                tt.SERVICIO = CmbEspecialidad.SelectedItem.Text + " - " + cmbProf.SelectedItem.Text;
                tt.fecha = Session["Turno"].ToString();
             //   Pnl.Visible = false;
                Session["Turno"] = tt;
                if (sal == "S")
                {
                    lblmsg2.Text = "El turno fué reservado por otra persona elija otro horario. ";
                }
                else {
                    Response.Redirect("ReservarEndIntranet.aspx", false);                
                }

                Hddexiste.Value = "";
               



            }
            catch (Exception ex)
            {

                lblmsg2.Text = ex.Message;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TxtNombre.Text = "";
            TxtApellido.Text = "";
            TxtDoc.Text = "";

            TxtCel.Text = "";
            txtMail.Text = "";
            Pnl.Visible = false;
        }

        [WebMethod()]

        public static Be.turnos WsExiste(string v_valor)
        {
            try
            {
             Be.turnos tur = Bll.BllTurnosMysql.DameInstancia().BuscarPaciente(v_valor);
                return  tur ;

            }
            catch (Exception)
            {

                throw;
            }


        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                Be.turnos tt ;
                tt = Bll.BllTurnosMysql.DameInstancia().BuscarDoc(TxtBuscar.Text);

                TxtBuscar.Text = "";
                Session["Turno"] = tt;
                if (tt != null)
                {
                
                    Response.Redirect("ReservarEnd.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                
            }
        }
    }
}