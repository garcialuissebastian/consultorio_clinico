using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using Dal;
namespace Bll
{
    public class Bllhosp_gen_turnos
    {
        private Dal.Dalhosp_gen_turnos _mapeador;

        public Bllhosp_gen_turnos()
        {
            _mapeador = new Dalhosp_gen_turnos();
        }

        private static Bllhosp_gen_turnos instancia = null;

        public static Bllhosp_gen_turnos DameInstancia()
        {
            if (instancia == null)
            {
                return new Bllhosp_gen_turnos();
            }
            else
            {
                return instancia;
            }
        }

         public string Regenerar(hosp_gen_turnos v_obj)
        {
            try
            {
             return   this._mapeador.Regenerar(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Alta(hosp_gen_turnos v_obj)
        {
            try
            {
                this._mapeador.Alta(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Modificacion(hosp_gen_turnos v_obj)
        {
            try
            {
                this._mapeador.Modificacion(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EliminarGen(string ids)
        {
            try
            {
                this._mapeador.EliminarGen(ids);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
         public string existe_gen( string id_prof,string id_serv, string id_dia, string fecha_ini,  string Turno)
        {
            try
            {
                return this._mapeador. existe_gen(  id_prof,  id_serv,   id_dia,  fecha_ini, Turno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModFechafinGenVar(string ids, string fecha)
        {
            try
            {
                this._mapeador.ModFechafinGenVar(ids, fecha);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<hosp_gen_turnos> Listar(string v_serv, string v_prof)
        {
            try
            {
                return this._mapeador.Listar( v_serv,  v_prof);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void Eliminar(Int32 v_id)
        {
            try
            {
                this._mapeador.Eliminar(v_id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
