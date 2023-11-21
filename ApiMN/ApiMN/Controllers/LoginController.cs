using System;
using System.Web.Http;
using ApiMN.Entities;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace ApiMN.Controllers
{
    public class LoginController : ApiController
    {

        Utilitarios util = new Utilitarios();

        [HttpPost]
        [Route("RegistrarCuenta")]
        public string RegistrarCuenta(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    //var user = new TUsuario();
                    //user.Identificacion = entidad.Identificacion;
                    //user.Nombre = entidad.Nombre;
                    //user.Correo = entidad.Correo;
                    //user.Contrasenna = entidad.Contrasenna;
                    //user.Direccion = entidad.Direccion;
                    //user.Estado = entidad.Estado;

                    //context.TUsuario.Add(user);
                    //context.SaveChanges();

                    context.RegistrarCuenta_SP(entidad.Identificacion, entidad.Nombre, entidad.Correo, entidad.Contrasenna);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public IniciarSesion_SP_Result IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    //return (from x in context.TUsuario
                    //             where x.Correo == entidad.Correo
                    //                && x.Contrasenna == entidad.Contrasenna
                    //                && x.Estado == true
                    //             select x).FirstOrDefault();

                    return context.IniciarSesion_SP(entidad.Correo, entidad.Contrasenna).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("RecuperarCuenta")]
        public string RecuperarCuenta(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    var datos = (from x in context.TUsuario
                                 where x.Identificacion == entidad.Identificacion
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "Templates\\mail.html";
                        string html = File.ReadAllText(urlHtml);

                        html = html.Replace("@@Nombre", datos.Nombre);
                        html = html.Replace("@@Contrasenna", datos.Contrasenna);

                        util.EnvioCorreos(datos.Correo, "Recuperar Contraseña", html);
                        return "OK";
                    }

                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}