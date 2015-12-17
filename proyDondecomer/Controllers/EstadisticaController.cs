using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyDondecomer.Models;
using System.Web.Http;

namespace proyDondecomer.Controllers
{
    public class EstadisticaController : ApiController
    {
        //
        // GET: /Estadistica/
        private dondeComerEntities db = new dondeComerEntities();

        public EstadisticaController() {
            db.Configuration.ProxyCreationEnabled = false;
        }
        
        //Top 5 de menus más consumidos
        public IEnumerable<LikeProducto> GetEstadistica()
        {


            var lista = db.LikeProducto.GroupBy(i => i.productoID)
                        .Select(g => new { Count = g.Count() });



                
               /* from l in db.LikeProducto
                        .GroupBy(l => l.productoID)
                        select new{
                            
                        };*/
            
            /*var like = db.LikeProducto.ToList();
            return like.AsEnumerable();*/
            return lista as IEnumerable<LikeProducto>;
        }

        /*
         * LO DE SQL SERVER
         * 
         select top 5  m.nombre as 'NombreMenu', COUNT(*) as 'CantidadLike' from LikeProducto l inner join Menu m
on l.productoID = m.menuID
group by productoID, m.nombre
order by CantidadLike desc
         */


        /*
        public IEnumerable<Menu> GetTopMenu()
        {
             int total;

             var lista = from l in db.LikeProducto
                         group l by l.productoID into g
                         select new 
                         {
                            Menu = g.Key, total =  g.Count()
                         };
           
            var menu = db.Menu.ToList();
            return menu.AsEnumerable();
        }*/


        //Top 5 de restaurantes por frecuencia de personas
        //Top 5 de restaurantes con más likes
        
        //El menu más consumido por mes
        //El restaurante más visitado por mes
        //
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
