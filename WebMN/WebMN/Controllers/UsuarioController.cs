using System.Linq;
using System.Web.Mvc;
using WebMN.Entities;
using WebMN.Models;

namespace WebMN.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel usuarioModel = new UsuarioModel();

        [HttpGet]
        public ActionResult ConsultaUsuarios()
        {
            long ConUsuario = long.Parse(Session["ConUsuario"].ToString());
            var datos = usuarioModel.ConsultaUsuarios().Where(x => x.ConUsuario != ConUsuario).ToList();
            return View(datos);
        }

        [HttpGet]
        public ActionResult PerfilUsuario()
        {
            long ConUsuario = long.Parse(Session["ConUsuario"].ToString());
            ViewBag.Provincias = usuarioModel.ConsultarProvincias();
            var datos = usuarioModel.ConsultaUsuario(ConUsuario);
            return View(datos);
        }

        [HttpPost]
        public ActionResult PerfilUsuario(UsuarioEnt entidad)
        {
            var resp = usuarioModel.ActualizarCuenta(entidad);

            if (resp == "OK")
            {
                Session["Nombre"] = entidad.Nombre;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Provincias = usuarioModel.ConsultarProvincias();
                ViewBag.MensajeUsuario = "No se pudo actualizar la información de su perfil";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarEstadoUsuario(long q)
        {
            var entidad = new UsuarioEnt();
            entidad.ConUsuario = q;

            var resp = usuarioModel.ActualizarEstadoUsuario(entidad);

            if (resp == "OK")
            {
                return RedirectToAction("ConsultaUsuarios", "Usuario");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del usuario";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarUsuario(long q)
        {
            ViewBag.Provincias = usuarioModel.ConsultarProvincias();
            var datos = usuarioModel.ConsultaUsuario(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(UsuarioEnt entidad)
        {
            var resp = usuarioModel.ActualizarCuenta(entidad);

            if (resp == "OK")
            {
                return RedirectToAction("ConsultaUsuarios", "Usuario");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del usuario";
                return View();
            }
        }

    }
}