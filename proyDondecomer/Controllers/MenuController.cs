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
    public class MenuController : ApiController
    {
        private dondeComerEntities1 db = new dondeComerEntities1();
        public MenuController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET api/Menu
        public IEnumerable<Menu> GetMenus()
        {
            var menu = db.Menu.ToList();
            return menu.AsEnumerable();
        }

        // GET api/Menu/5
        public Menu GetMenu(int idRestaurante)
        {
            Menu menu = db.Menu.Where(p => p.pestauranteID == idRestaurante).FirstOrDefault();

            return menu;
        }

        // PUT api/Menu/5
        public HttpResponseMessage PutMenu(int id, Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != menu.menuID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(menu).State = EntityState.Modified;

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

        // POST api/Menu
        public HttpResponseMessage PostMenu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menu.Add(menu);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, menu);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = menu.menuID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Menu/5
        public HttpResponseMessage DeleteMenu(int id)
        {
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Menu.Remove(menu);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, menu);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}