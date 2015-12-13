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
    public class PlatoMenuController : ApiController
    {
        private dondeComerEntities db = new dondeComerEntities();
        public PlatoMenuController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/PlatoMenu
        public IEnumerable<Menu_Producto> GetMenu_Producto()
        {
            var menu_producto = db.Menu_Producto.ToList();
            return menu_producto.AsEnumerable();
        }

        // GET api/PlatoMenu/5
        public Menu_Producto GetMenu_Producto(int id)
        {
            Menu_Producto menu_producto = db.Menu_Producto.Find(id);
            if (menu_producto == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return menu_producto;
        }

        // PUT api/PlatoMenu/5
        public HttpResponseMessage PutMenu_Producto(int id, Menu_Producto menu_producto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != menu_producto.MenuProductoID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(menu_producto).State = EntityState.Modified;

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

        // POST api/PlatoMenu
        public HttpResponseMessage PostMenu_Producto(Menu_Producto menu_producto)
        {
            if (ModelState.IsValid)
            {
                db.Menu_Producto.Add(menu_producto);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, menu_producto);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = menu_producto.MenuProductoID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/PlatoMenu/5
        public HttpResponseMessage DeleteMenu_Producto(int id)
        {
            Menu_Producto menu_producto = db.Menu_Producto.Find(id);
            if (menu_producto == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Menu_Producto.Remove(menu_producto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, menu_producto);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}