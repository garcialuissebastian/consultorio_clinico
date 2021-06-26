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
    public partial class ReservarAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["UsuarioActual"] == null)
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);
                }
                if (!Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);

                }

                List<Be.Combos> lisSer = Bll.BllTurnosMysql.DameInstancia().CmdEspecialidade();
                foreach (var item in lisSer)
                { ListItem t1 = new ListItem();
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

        protected void cmbProf_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Dias"] = "";
           
            Session["s_fecha"] = "";

            string sal = Bll.BllTurnosMysql.DameInstancia().TurnosDispReg(cmbProf.SelectedValue, CmbEspecialidad.SelectedValue); // lv y sab

            Session["Dias"] = sal; // dias disponibles canlendario -- devuelve los dias dispobles

           
        }

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
                else {
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
                Be.turnos tt  = null;
               
               
               
                if (tt != null)
                {
                
                    Response.Redirect("ReservarEnd.aspx", false);
                }
            }
            catch (Exception ex)
            {
            
                
            }
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().CancelarFecha(CmbEspecialidad1.SelectedValue.ToString(), cmbProf1.SelectedValue.ToString(), Txtdia1.Text);
                Txtdia1.Text = "";
            }
            catch (Exception)
            {
                
                throw;
            }
        }
  
    
    
    
    
    
    
    
    }
}