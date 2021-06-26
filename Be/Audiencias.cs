using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class Audiencias
    {

        public Int32 Id { get; set; }
        public string Fecha { get; set; }
        public string Lugar { get; set; }
        public string Conjunta { get; set; }
        public string Privada { get; set; }
        public string Fecha_primera { get; set; }
        public string Fecha_ultima { get; set; }
        public string Convenio { get; set; }
        public string Acuerdo { get; set; }
        public string Mediacion { get; set; }
        public string Negociacion { get; set; }
        public string Arbitraje { get; set; }
        public string Facilitacion { get; set; }
        public string Descripcion { get; set; }

        List<Audiencia_req> Item1 = new List<Audiencia_req>();
        public List<Audiencia_req> Requirentes
        {
            get { return Item1; }
            set { Item1 = value; }

        }
    }
}
