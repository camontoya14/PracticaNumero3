using ApiMN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiMN.Controllers
{
    public class UsuarioController : ApiController
    {

        [HttpGet]
        [Route("ConsultaUsuarios")]
        public List<TUsuario> ConsultaUsuarios()
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.TUsuario
                            select x).ToList();
                }
            }
            catch (Exception)
            {
                return new List<TUsuario>();
            }
        }

        [HttpGet]
        [Route("ConsultaUsuario")]
        public TUsuario ConsultaUsuario(long q)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.TUsuario
                                 where x.ConUsuario == q
                                 select x).FirstOrDefault();

                    return datos;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("ConsultarProvincias")]
        public List<System.Web.Mvc.SelectListItem> ConsultarProvincias()
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    var datos = (from x in context.TProvincia
                                 select x).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.ConProvincia.ToString(), Text = item.Descripcion });
                    }

                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpPut]
        [Route("ActualizarCuenta")]
        public string ActualizarCuenta(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    var datos = (from x in context.TUsuario
                                 where x.ConUsuario == entidad.ConUsuario
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.Correo = entidad.Correo;
                        datos.Nombre = entidad.Nombre;
                        datos.Identificacion = entidad.Identificacion;
                        datos.ConProvincia = entidad.ConProvincia;
                        context.SaveChanges();
                    }

                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpPut]
        [Route("ActualizarEstadoUsuario")]
        public string ActualizarEstadoUsuario(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    var datos = (from x in context.TUsuario
                                 where x.ConUsuario == entidad.ConUsuario
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.Estado = (datos.Estado == true ? false : true);
                        context.SaveChanges();
                    }

                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}