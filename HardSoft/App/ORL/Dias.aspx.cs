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
    public partial class Dias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Msg.Text = "";
                if (!Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
                {
                    Response.Redirect("http://www.hardsoft.com.ar/", false);

                }

            }
            catch (Exception)
            {

                Response.Redirect("http://www.quilmes.gov.ar", false);
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["UsuarioActual"] == null)
            {
                Response.Redirect("http://www.corlquilmes.com.ar", false);
            }
            if (!Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
            {
                Response.Redirect("http://www.corlquilmes.com.ar", false);

            }


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

        public static List<Be.turnos> WsListar()
        {
            try
            {
                List<Be.turnos> list = new List<Be.turnos>();

                list = Bll.BllTurnosMysql.DameInstancia().ListarDiasTurnos();
                HttpContext.Current.Session["Lista"] = list;

                return list;


            }
            catch (Exception)
            {

                throw;
            }


        }

        protected void BtnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                turnos tr = new turnos();
                tr.fecha = TxtFechaIngreso.Text;
                tr.NOMBRE = txtDesc.Text;
                Bll.BllTurnosMysql.DameInstancia().altaDiaTurn(tr);
                Response.Redirect("dias.aspx", false);
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message;

            }
        }

    }
}