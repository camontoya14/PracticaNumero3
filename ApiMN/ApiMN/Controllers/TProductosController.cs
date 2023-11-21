using ApiMN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiMN.Controllers
{
    public class TProductosController : ApiController
    {

        [HttpGet]
        [Route("ConsultarProductos")]
        public List<TProducto> ConsultarProductos()
        {
            using (var context = new BDMNEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.TProducto.ToList();
            }
        }

        [HttpPost]
        [Route("RegistrarProducto")]
        public long RegistrarProducto(TProducto tProducto)
        {
            using (var context = new BDMNEntities())
            {
                context.TProducto.Add(tProducto);
                context.SaveChanges();
                return tProducto.ConProducto;
            }
        }

        [HttpPut]
        [Route("ActualizarRutaImagen")]
        public string ActualizarRutaImagen(TProducto tProducto)
        {
            using (var context = new BDMNEntities())
            {
                var datos = context.TProducto.FirstOrDefault(x => x.ConProducto == tProducto.ConProducto);

                if (datos != null)
                {
                    datos.Imagen = tProducto.Imagen;
                    context.SaveChanges();
                }

                return "OK";
            }
        }
        [HttpGet]
        [Route("ConsultarProductos")]
        public TProducto ConsultarProductos(long q)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.TProducto
                                 where x.ConProducto == q
                                 select x).FirstOrDefault();

                    return datos;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPut]
        [Route("ActualizarProducto")]
        public string ActualizarProducto(ProductoEnt entidad)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    var datos = (from x in context.TProducto
                                 where x.ConProducto == entidad.ConProducto
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.Nombre = entidad.Nombre;
                        datos.Descripcion = entidad.Descripcion;
                        datos.Precio = entidad.Precio;
                        datos.Cantidad = entidad.Cantidad;
                        datos.Imagen = entidad.Imagen;
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
        [Route("ActualizarEstadoProducto")]
        public string ActualizarEstadoProducto(ProductoEnt entidad)
        {
            try
            {
                using (var context = new BDMNEntities())
                {
                    var datos = (from x in context.TProducto
                                 where x.ConProducto == entidad.ConProducto
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