using Be;
using Bll;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HardSoft.App.ORL.Controlador
{
    /// <summary>
    /// Descripción breve de FillHc
    /// </summary>
    public class FillHc : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        [WebMethod(EnableSession = true)]
        public void ProcessRequest(HttpContext context)
        {
            try
            {

                string data = string.Empty;

                try
                {

                    Be.hosp_hc sal = new Be.hosp_hc();
                    List<Be.hosp_hc> list = new List<Be.hosp_hc>();


                    sal = Bll.BllTurnosMysql.DameInstancia().ListarHc_id(context.Request.QueryString["v_Id"]);

                 

                    string json2 = JsonConvert.SerializeObject(sal); // esto anda pero me pasa todas las propiedades


                    data = json2.ToString();
                    if (data != string.Empty)
                    {
                        context.Response.Write(data);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}