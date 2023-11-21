using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMN.Entities;
using WebMN.Models;

namespace WebMN.Controllers
{
    public class ProductoController : Controller
    {
        ProductoModel productoModel = new ProductoModel();

        [HttpGet]
        public ActionResult ConsultarProductos()
        {
            var datos = productoModel.ConsultarProductos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarProducto(HttpPostedFileBase ImgProducto, ProductoEnt entidad)
        {
            entidad.Imagen = string.Empty;
            entidad.Estado = true;

            long conProducto = productoModel.RegistrarProducto(entidad);

            if (conProducto > 0)
            {
                string extension = Path.GetExtension(Path.GetFileName(ImgProducto.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + conProducto + extension;
                ImgProducto.SaveAs(ruta);

                entidad.Imagen = "/Images/" + conProducto + extension;
                entidad.ConProducto = conProducto;

                productoModel.ActualizarRutaImagen(entidad);

                return RedirectToAction("ConsultarProductos", "Producto");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha registrado el producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarEstadoProducto(long q)
        {
            var entidad = new ProductoEnt();
            entidad.ConProducto = q;

            var resp = productoModel.ActualizarEstadoProducto(entidad);

            if (resp == "OK")
            {
                return RedirectToAction("ConsultarProductos", "Producto");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarProducto(long q)
        {
            var datos = productoModel.ConsultarProductos(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult ActualizarProducto(HttpPostedFileBase ImgProducto, ProductoEnt entidad)
        {
            entidad.Imagen = string.Empty;
            entidad.Estado = true;



            string directorioImagenes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            string[] archivosAEliminar = Directory.GetFiles(directorioImagenes, entidad.ConProducto + ".*");

            foreach (string archivo in archivosAEliminar)
            {
                System.IO.File.Delete(archivo);
            }
           

            string extension = Path.GetExtension(Path.GetFileName(ImgProducto.FileName));
            string rutaNuevaImagen = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + entidad.ConProducto + extension;

            if (System.IO.File.Exists(rutaNuevaImagen))
            {
                System.IO.File.Delete(rutaNuevaImagen);
            }

            ImgProducto.SaveAs(rutaNuevaImagen);

            entidad.Imagen = "/Images/" + entidad.ConProducto + extension;
            

            var resp = productoModel.ActualizarProducto(entidad);

            if (resp == "OK")
            {
                return RedirectToAction("ConsultarProductos", "Producto");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el producto";
                return View();
            }
        }
    }
}