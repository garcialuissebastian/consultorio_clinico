using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bll;
using Be;

namespace HardSoft.App.ORL
{
    public partial class Hosp_obra_sociales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod()]


        public static List<hosp_obra_sociales> WsListar_hosp_obra_sociales(string v_tipo, string v_valor)
        {
            try
            {
                List<hosp_obra_sociales> list = new List<hosp_obra_sociales>();

                list = BllTurnosMysql.DameInstancia().Listar_hosp_obra_sociales(v_tipo, v_valor.ToUpper().Trim());

                //HttpContext.Current.Session["Lista"] = list;

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }




        [WebMethod()]


        public static void WmGvDel_hosp_obra_sociales(Int32 v_Id)
        {
            try
            {

                Bll.BllTurnosMysql.DameInstancia().Eliminar_hosp_obra_sociales(v_Id);

            }

            catch (Exception)
            {

                throw;

            }

        }

        [WebMethod()]


        public static void WmGvMod_hosp_obra_sociales(hosp_obra_sociales v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().Modificacion_hosp_obra_sociales(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [WebMethod()]


        public static void WmGvAlta_hosp_obra_sociales(hosp_obra_sociales v_obj)
        {
            try
            {
                Bll.BllTurnosMysql.DameInstancia().Alta_hosp_obra_sociales(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        } 
    }
}