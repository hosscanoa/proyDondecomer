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
    public class RestauranteController : ApiController
    {
        private dondeComerEntities db = new dondeComerEntities();
        public RestauranteController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET api/Restaurante
        public IEnumerable<Restaurante> GetRestaurantes()
        {
            var restaurante = db.Restaurante.ToList();
            return restaurante.AsEnumerable();
        }

        // GET api/Restaurante/5
        public Restaurante GetRestaurante(int idAdministrador)
        {
            Restaurante restaurante = db.Restaurante.Where(p => p.usuarioID == idAdministrador).FirstOrDefault();
            return restaurante;
        }

        // PUT api/Restaurante/5
        public HttpResponseMessage PutRestaurante(int id, Restaurante restaurante)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != restaurante.restauranteID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(restaurante).State = EntityState.Modified;

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

        // POST api/Restaurante
        public HttpResponseMessage PostRestaurante(Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Restaurante.Add(restaurante);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, restaurante);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = restaurante.restauranteID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Restaurante/5
        public HttpResponseMessage DeleteRestaurante(int id)
        {
            Restaurante restaurante = db.Restaurante.Find(id);
            if (restaurante == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Restaurante.Remove(restaurante);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, restaurante);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}