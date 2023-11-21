using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using WebMN.Entities;

namespace WebMN.Models
{
    public class ProductoModel
    {

        public string urlApi = ConfigurationManager.AppSettings["urlApi"];

        public List<ProductoEnt> ConsultarProductos()
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "ConsultarProductos";
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            }
        }

        public long RegistrarProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "RegistrarProducto";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<long>().Result;
            }
        }

        public string ActualizarRutaImagen(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "ActualizarRutaImagen";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public ProductoEnt ConsultarProductos(long q)
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "ConsultarProductos?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<ProductoEnt>().Result;
            }
        }

        public string ActualizarProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "ActualizarProducto";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string ActualizarEstadoProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "ActualizarEstadoProducto";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

    }
}