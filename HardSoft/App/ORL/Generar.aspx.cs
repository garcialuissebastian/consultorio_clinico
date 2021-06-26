using Be;
using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HardSoft.App.ORL
{
    public partial class Generar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Msg.Text = "";
                if (HttpContext.Current.Session["UsuarioActual"] == null)
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);
                }
                if (!Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);

                }
                if(!Page.IsPostBack){



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
                }
            }
            catch (Exception)
            {

                Response.Redirect("http://www.hardsoft.com.ar/", false);
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {




        }
        protected void BtnCanc_Click(object sender, EventArgs e)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().tur_cancelarDias(TxtId.Text);

                Response.Redirect("dias.aspx", false);

            }
            catch (Exception)
            {
                               throw;
            }

        }



        [WebMethod()]

        public static void ModFechafinGenVar(string idgen, string fecha)
        {
            try
            {
                Bll.Bllhosp_gen_turnos.DameInstancia().ModFechafinGenVar(idgen, fecha);


            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static List<Be.hosp_gen_turnos> WsListar(string v_serv , string v_prof)
        {
            try
            {
                List<Be.hosp_gen_turnos> list = new List<Be.hosp_gen_turnos>();

                list = Bll.Bllhosp_gen_turnos.DameInstancia().Listar( v_serv,   v_prof);
                HttpContext.Current.Session["Lista"] = list;

                return list;


            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static string Wsexiste_gen(string id_prof, string id_serv, string id_dia, string fecha_ini, string Turno)
        {
            try
            {
               
                return Bll.Bllhosp_gen_turnos.DameInstancia().existe_gen(  id_prof,   id_serv,  id_dia,   fecha_ini, Turno);


            }
            catch (Exception)
            {

                throw;
            }


        }



        [WebMethod()]

        public static void WsEliminar(string ids)
        {
            try
            {

                 Bll.Bllhosp_gen_turnos.DameInstancia().EliminarGen(ids);


            }
            catch (Exception)
            {

                throw;
            }


        }

        [WebMethod()]

        public static string WsReg(string ids, string fecha_ini , string fecha_fin)
        {
            try
            {
                hosp_gen_turnos hh = new hosp_gen_turnos();
                hh.Id = Convert.ToInt32(ids);
                hh.Fecha_Inicio = fecha_ini;
                hh.Fecha_Fin = fecha_fin;
                string sal = Bllhosp_gen_turnos.DameInstancia().Regenerar(hh);
                return sal;

            }
            catch (Exception)
            {

                throw;
            }


        }


        protected void CmbEspecialidad1_SelectedIndexChanged(object sender, EventArgs e)
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



        //protected void BtnAlta_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        hosp_gen_turnos hh = new hosp_gen_turnos();
        //        hh.Id_Servicio = CmbEspecialidad.SelectedValue;
        //        hh.Id_Profesional = cmbProf.SelectedValue;
        //        hh.Id_Dias = CmbDia.SelectedValue;
        //        hh.DE = TxtCant.Text;
        //        hh.Hora_Fin = CantTur2.Text;

        //        if (ck1.Checked) {
        //            hh.Turno = "M";
        //        }
        //        if (ck2.Checked)
        //        {
        //            hh.Turno = "T";
        //        }
        //        hh.Hora_Ini = TxtHora.Text + ":" + TxtMin.Text;
        //        hh.Cada_Cuanto = TxtCadaCuanto.Text;
        //        hh.Fecha_Inicio = TxtFechaIngreso.Text;
        //        hh.Fecha_Fin = TxtFin.Text;
        //        hh.Obs = txtObs.Text;

        //        Bllhosp_gen_turnos.DameInstancia().Alta(hh);

        //        txtObs.Text = "";
        //        TxtFin.Text = "";
        //        TxtFechaIngreso.Text = "";
        //        TxtHora.Text = "00";
        //        TxtCadaCuanto.Text = "0";
        //        TxtMin.Text = "00";
        //        TxtCant.Text = "0";
        //        CantTur2.Text = "0";
        //        ck1.Checked = false;
        //        ck2.Checked = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        Msg.Text = ex.Message;

        //    }
        //}


        protected void BtnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbDia.SelectedValue == "99")
                {//lunes a viernes
                    List<string> dias = new List<string>();
                    dias.Add("0");// lunes a viernes  0 a 4
                    dias.Add("1");
                    dias.Add("2");
                    dias.Add("3");
                    dias.Add("4");
                    foreach (var item in dias)
                    {
                        generarTur(item.ToString());
                    }
                }
                else
                {

                    generarTur(CmbDia.SelectedValue);
                }

                txtObs.Text = "";
                TxtFin.Text = "";
                TxtFechaIngreso.Text = "";
                TxtHora.Text = "00";
                TxtCadaCuanto.Text = "0";
                TxtMin.Text = "00";
                TxtCant.Text = "0";
                CantTur2.Text = "0";
                ck1.Checked = false;
                ck2.Checked = false;

            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message;

            }
        }

        public void generarTur(string Id_Dias)
        {
            try
            {
                hosp_gen_turnos hh = new hosp_gen_turnos();
                hh.Id_Dias = Id_Dias;


                hh.Id_Servicio = CmbEspecialidad.SelectedValue;
                hh.Id_Profesional = cmbProf.SelectedValue;

                hh.DE = TxtCant.Text;
                hh.Hora_Fin = CantTur2.Text;

                if (ck1.Checked)
                {
                    hh.Turno = "M";
                }
                if (ck2.Checked)
                {
                    hh.Turno = "T";
                }
                hh.Hora_Ini = TxtHora.Text + ":" + TxtMin.Text;
                hh.Cada_Cuanto = TxtCadaCuanto.Text;
                hh.Fecha_Inicio = TxtFechaIngreso.Text;
                hh.Fecha_Fin = TxtFin.Text;
                hh.Obs = txtObs.Text;

                Bllhosp_gen_turnos.DameInstancia().Alta(hh);

            }
            catch (Exception)
            {

                throw;
            }


        }

        protected void btnAlta2_Click(object sender, EventArgs e)
        {
            
            try
            {
                hosp_gen_turnos hh = new hosp_gen_turnos();
                hh.Id =Convert.ToInt32( TxtId.Text);
                hh.Fecha_Inicio = TxtF1.Text;
                hh.Fecha_Fin = TxtF2.Text;
       string sal=         Bllhosp_gen_turnos.DameInstancia().Regenerar(hh);

            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message;

            }
        }

    }
}