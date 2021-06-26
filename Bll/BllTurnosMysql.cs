
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dal;
    using Be;
    using System.Data;
    namespace Bll
    {
        public class BllTurnosMysql
        {


            private Dal.DalTurnosMysql _mapeador;
            private Dal.DalTurnosMysql _mapeadorReg;
            public BllTurnosMysql()
            {

                _mapeador = new Dal.DalTurnosMysql();
                _mapeadorReg = new Dal.DalTurnosMysql();
            }

            private static BllTurnosMysql instancia = null;

            public static BllTurnosMysql DameInstancia()
            {

                if (instancia == null)
                {

                    return new BllTurnosMysql();
                }
                else
                {
                    return instancia;
                }

            }

               public  turnos   TurAsis_byPac(string v_doc)
        {
            try
            {
             return   this._mapeadorReg.TurAsis_byPac( v_doc);

            }
            catch (Exception)
            {

                throw;
            }

        }
            public void AltaHc(hosp_hc v_obj)
            {
                try
                {
                    this._mapeadorReg.AltaHc(v_obj);

                }
                catch (Exception)
                {

                    throw;
                }

            }
             public void ModPaciente(turnos v_tur)
            {
                try
                {
                    this._mapeadorReg.ModPaciente(v_tur);

                }
                catch (Exception)
                {

                    throw;
                }

            }
          public void ModificacionHc(hosp_hc v_obj)
        {
                try
                {
                    this._mapeadorReg.ModificacionHc(v_obj);

                }
                catch (Exception)
                {

                    throw;
                }

            }
             public  hosp_hc ListarHc_id(string v_pac)
          {
              try
              {
                  return this._mapeadorReg.ListarHc_id(v_pac);

              }
              catch (Exception)
              {

                  throw;
              }

          }
          public List<hosp_hc> ListarHc(string v_pac)
          {
              try
              {
                  return this._mapeadorReg.ListarHc(v_pac);

              }
              catch (Exception)
              {

                  throw;
              }

          }

          public void EliminarHc(Int32 v_id)
          {
              try
              {
                  this._mapeadorReg.EliminarHc(v_id);

              }
              catch (Exception)
              {

                  throw;
              }

          }



             public void tur_cancelarTur(string v_id)
        {
            try
            {
                this._mapeadorReg. tur_cancelarTur(  v_id);

            }
            catch (Exception)
            {

                throw;
            }

        }

             public void tur_monto(string v_vino, string v_monto, string v_obs, string ids, string v_prac, string v_consulta, string v_copago)
        {
            try
            {
                 this._mapeadorReg.tur_monto( v_vino,  v_monto,  v_obs,   ids,  v_prac,   v_consulta, v_copago);

            }
            catch (Exception)
            {

                throw;
            }

        }
             public void cerrarHc(string v_pac, string obs)
               {
                   try
                   {
                      this._mapeadorReg.cerrarHc( v_pac,   obs);

                   }
                   catch (Exception)
                   {

                       throw;
                   }

               }

        public List<turnos> ListarTurAsisRp(string v_serv, string v_prof, string v_fecha, string v_fecha2)
        {
            try
            {
                return this._mapeadorReg.ListarTurAsisRp( v_serv,  v_prof,   v_fecha,   v_fecha2);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<turnos> ListarTurAsis(string v_serv, string v_prof, string v_fecha )
        {
            try
            {
                return this._mapeadorReg.ListarTurAsis(  v_serv,   v_prof,  v_fecha );

            }
            catch (Exception)
            {

                throw;
            }

        }
              public List<turnos> ListarTurPers(string v_tipo, string v_valor)
        {

            try
            {
                return this._mapeadorReg. ListarTurPers(  v_tipo,   v_valor);

            }
            catch (Exception)
            {

                throw;
            }

        }

              public DataTable Rp_turListarVestibular(string v_id)
              {
                  try
                  {
                      return this._mapeadorReg.Rp_turListarVestibular(  v_id);

                  }
                  catch (Exception)
                  {

                      throw;
                  }

              }
              public DataTable Rp_turListar(string v_prof, string v_serv, string v_fecha, string v_tur, string V_TIPO)
        {

            try
            {
                return this._mapeadorReg. Rp_turListar(  v_prof, v_serv,  v_fecha, v_tur, V_TIPO);

            }
            catch (Exception)
            {

                throw;
            }

        }
             public DataSet Rp_Hc(string v_id)
            {
                try
                {
                    return this._mapeadorReg.Rp_Hc(v_id);

                }
                catch (Exception)
                {

                    throw;
                }

            }


            public DataTable Rp_tur(string v_id)
            {
                try
                {
                    return this._mapeadorReg.Rp_tur(v_id);

                }
                catch (Exception)
                {

                    throw;
                }

            }

            public string tiene_tur(string v_doc)
            {
                try
                {
                    return this._mapeadorReg.tiene_tur(v_doc);

                }
                catch (Exception)
                {

                    throw;
                }

            }

            public void altaDiaTurn(turnos tur)
            {
                try
                {
                    this._mapeadorReg.altaDiaTurn(tur);

                }
                catch (Exception)
                {

                    throw;
                }

            }
            public List<turnos> ListarDiasTurnos()
            {
                try
                {
                    return this._mapeadorReg.ListarDiasTurnos();

                }
                catch (Exception)
                {

                    throw;
                }

            }

            public void tur_cancelarDias(string v_id)
            {
                try
                {
                    this._mapeadorReg.tur_cancelarDias(v_id);
                }
                catch (Exception)
                {

                    throw;
                }

            }
              public List<turnos> ListarPaciente(string doc, string tipo)
            {
                try
                {
                    return this._mapeadorReg. ListarPaciente(  doc,  tipo);

                }
                catch (Exception)
                {

                    throw;
                }

            }

            public turnos BuscarPaciente(string doc)
        {
            try
            {
                return this._mapeadorReg.BuscarPaciente(doc);

            }
            catch (Exception)
            {

                throw;
            }

        }
            public turnos BuscarDoc(string doc)
            {
                try
                {
                    return this._mapeadorReg.BuscarDoc(doc);

                }
                catch (Exception)
                {

                    throw;
                }

            }
            

                      public string  ReservarIntranet(turnos v_tur)
            {
                try
                {
                    return this._mapeadorReg.ReservarIntranet(v_tur);

                }
                catch (Exception)
                {

                    throw;
                }

            }
            public string Reservar(turnos v_tur)
            {
                try
                {
                    return this._mapeadorReg.Reservar(v_tur);

                }
                catch (Exception)
                {

                    throw;
                }

            }
               public string SobreTurno(Be.turnos v_tur)
            {
                try
                {
                 return   this._mapeadorReg.SobreTurno( v_tur);

                }
                catch (Exception)
                {

                    throw;
                }

            }
               public List<Be.Combos> CmdProfesinalhC(string id_servicio, string v_user)
               {
                   try
                   {
                       return this._mapeadorReg.CmdProfesinalhC(id_servicio, v_user);

                   }
                   catch (Exception)
                   {

                       throw;
                   }

               }

            public List<Be.Combos> CmdObraSociales(string id_servicio)
               {
                   try
                   {
                       return this._mapeadorReg. CmdObraSociales(id_servicio);

                   }
                   catch (Exception)
                   {

                       throw;
                   }

               }

              public List<Be.Combos> CmdProfesinal(string id_servicio)
            {
                try
                {
                    return this._mapeadorReg.CmdProfesinal(id_servicio);

                }
                catch (Exception)
                {

                    throw;
                }

            }

              public List<Be.Combos> CmdEspecialidadeHc(string v_user)
              {
                  try
                  {
                      return this._mapeadorReg.CmdEspecialidadHC(v_user);

                  }
                  catch (Exception)
                  {

                      throw;
                  }

              }
            public List<Be.Combos> CmdEspecialidade()
            {
                try
                {
                    return this._mapeadorReg.CmdEspecialidade();

                }
                catch (Exception)
                {

                    throw;
                }

            }
        public void    CancelarFecha(string v_consul,string v_prof,string v_dia)
            {
                try
                {
                   this._mapeadorReg.CancelarFecha(v_consul,v_prof,v_dia);

                }
                catch (Exception)
                {

                    throw;
                }

            }


        public string TurnosDispList(string v_id, string v_id2)
            {
                try
                {
                    return this._mapeadorReg.TurnosDispList(v_id, v_id2);

                }
                catch (Exception)
                {

                    throw;
                }

            }
            public string TurnosDispReg(string v_id, string v_id2)
            {
                try
                {
                    return this._mapeadorReg.TurnosDisp(v_id, v_id2);

                }
                catch (Exception)
                {

                    throw;
                }

            }





            public List<Be.turnos> listar_turnosReg(DateTime v_fecha, string v_prof, string v_serv)
            {
                try
                {
                    return this._mapeadorReg.listar_turnos(v_fecha, v_prof, v_serv);

                }
                catch (Exception)
                {
                    throw;
                }

            }
             
            
                public void Alta_hosp_hc_files(hosp_hc_files v_obj)
                {
                    try
                    {
                        this._mapeador.Alta_hosp_hc_files(v_obj);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                public void Modificacion_hosp_hc_files(hosp_hc_files v_obj)
                {
                    try
                    {
                        this._mapeador.Modificacion_hosp_hc_files(v_obj);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                public List<hosp_hc_files> Listar_hosp_hc_files(string v_tipo, string v_valor)
                {
                    try
                    {
                        return this._mapeador.Listar_hosp_hc_files(v_tipo, v_valor);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                public void Eliminar_hosp_hc_files(Int32 v_id)
                {
                    try
                    {
                        this._mapeador.Eliminar_hosp_hc_files(v_id);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

           
            // examen vestibular

              public void Alta_hosp_examen_vestibular(hosp_examen_vestibular  v_obj)
       
{ 
    try
            {
                 this._mapeador.Alta_hosp_examen_vestibular(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        } 

  public void Modificacion_hosp_examen_vestibular(hosp_examen_vestibular  v_obj)
      
{ 
      try
            {
                 this._mapeador.Modificacion_hosp_examen_vestibular( v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        } 

 public  List<hosp_examen_vestibular> Listar_hosp_examen_vestibular(string   v_tipo,   string   v_valor, string v_pac)

         
{ 
  try
            {
             return    this._mapeador.Listar_hosp_examen_vestibular( v_tipo, v_valor,v_pac);
            }
            catch (Exception)
            {
                throw;
            }
        } 

  public void Eliminar_hosp_examen_vestibular(Int32  v_id)
        
{ 
    try
            {
                 this._mapeador.Eliminar_hosp_examen_vestibular( v_id);
            }
            catch (Exception)
            {
                throw;
            }
         

}


public void Alta_hosp_obra_sociales(hosp_obra_sociales  v_obj)
       
{ 
    try
            {
                 this._mapeador.Alta_hosp_obra_sociales(v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        } 

  public void Modificacion_hosp_obra_sociales(hosp_obra_sociales  v_obj)
      
{ 
      try
            {
                 this._mapeador.Modificacion_hosp_obra_sociales( v_obj);
            }
            catch (Exception)
            {
                throw;
            }
        } 

 public  List<hosp_obra_sociales> Listar_hosp_obra_sociales(string   v_tipo,   string   v_valor)

         
{ 
  try
            {
             return    this._mapeador.Listar_hosp_obra_sociales( v_tipo, v_valor);
            }
            catch (Exception)
            {
                throw;
            }
        } 

  public void Eliminar_hosp_obra_sociales(Int32  v_id)
        
{ 
    try
            {
                 this._mapeador.Eliminar_hosp_obra_sociales( v_id);
            }
            catch (Exception)
            {
                throw;
            }
        } 

}

  
     


        
    

}
