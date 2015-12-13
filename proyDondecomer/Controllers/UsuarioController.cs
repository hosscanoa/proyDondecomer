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
    public class UsuarioController : ApiController
    {
        private dondeComerEntities1 db = new dondeComerEntities1();

        public UsuarioController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/Usuario
        public IEnumerable<Usuario> GetUsuarios()
        {
            var usuario = db.Usuario.ToList();
            return usuario.AsEnumerable();
        }

        // GET api/Usuario/5
        public Usuario GetUsuario(string usuario, string password, string rol)
        {
            Usuario bean = db.Usuario.Where(p => p.usuario1 == usuario && p.password == password && p.rol == rol).FirstOrDefault();

            return bean;
        }

        // PUT api/Usuario/5
        public HttpResponseMessage PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != usuario.usuarioID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(usuario).State = EntityState.Modified;

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

        // POST api/Usuario
        public HttpResponseMessage PostUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, usuario);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = usuario.usuarioID}));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Usuario/5
        public HttpResponseMessage DeleteUsuario(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Usuario.Remove(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}