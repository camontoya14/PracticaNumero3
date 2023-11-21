using System.Web.Mvc;
using WebMN.Entities;
using WebMN.Models;

namespace WebMN.Controllers
{
    public class LoginController : Controller
    {
        UsuarioModel usuarioModel = new UsuarioModel();
        ProductoModel productoModel = new ProductoModel();

        [HttpGet]
        public ActionResult Index()
        {
            var datos = productoModel.ConsultarProductos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("IniciarSesion", "Login");
        }


        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            var resp = usuarioModel.IniciarSesion(entidad);

            if (resp != null)
            {
                Session["ConUsuario"] = resp.ConUsuario;
                Session["Nombre"] = resp.Nombre;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "Compruebe la información de sus credenciales";
                return View();
            }
        }


        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioEnt entidad)
        {
            var resp = usuarioModel.RegistrarCuenta(entidad);

            if (resp == "OK")
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha registrado su información";
                return View();
            }
        }


        [HttpGet]
        public ActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarCuenta(UsuarioEnt entidad)
        {
            var resp = usuarioModel.RecuperarCuenta(entidad);

            if (resp == "OK")
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha enviado el correo con su información";
                return View();
            }
        }

    }
}