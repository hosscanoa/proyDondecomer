using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using proyDondecomer.Models;

namespace proyDondecomer.Controllers
{
    public class PlatoController : ApiController
    {
        private dondeComerEntities db = new dondeComerEntities();
        public PlatoController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/Plato
        public IEnumerable<Producto> GetProductoes()
        {
            return db.Producto.AsEnumerable();
        }

        // GET api/Plato/5
        public Producto GetProducto(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return producto;
        }

        // PUT api/Plato/5
        public HttpResponseMessage PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != producto.productoID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Plato
        public HttpResponseMessage PostProducto(Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, producto);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = producto.productoID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Plato/5
        public HttpResponseMessage DeleteProducto(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Producto.Remove(producto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, producto);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}