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
    public class LikeController : ApiController
    {
        private dondeComerEntities db = new dondeComerEntities();

        public LikeController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/Like
        public IEnumerable<LikeProducto> GetLikeProductoes()
        {
            var likeproducto = db.LikeProducto;
            return likeproducto.AsEnumerable();
        }

        // GET api/Like/5
        public LikeProducto GetLikeProducto(int id)
        {
            LikeProducto likeproducto = db.LikeProducto.Find(id);
            if (likeproducto == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return likeproducto;
        }

        // PUT api/Like/5
        public HttpResponseMessage PutLikeProducto(int id, LikeProducto likeproducto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != likeproducto.LikeProductoID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(likeproducto).State = EntityState.Modified;

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

        // POST api/Like
        public HttpResponseMessage PostLikeProducto(LikeProducto likeproducto)
        {
            if (ModelState.IsValid)
            {
                db.LikeProducto.Add(likeproducto);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, likeproducto);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = likeproducto.LikeProductoID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Like/5
        public HttpResponseMessage DeleteLikeProducto(int id)
        {
            LikeProducto likeproducto = db.LikeProducto.Find(id);
            if (likeproducto == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.LikeProducto.Remove(likeproducto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, likeproducto);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}