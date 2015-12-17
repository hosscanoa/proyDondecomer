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
        public IEnumerable<PChart> GetEstadistica()
        {
            var results = (from T1 in db.Menu_Producto
                          join T2 in db.Menu on T1.menuID equals T2.menuID
                          group T1 by new { T2.nombre} into xml
                          select new {
                            title = xml.Key.nombre,
                            value = xml.Count()
                          }).Take(5);
            List<PChart> listaChart = new List<PChart>();
            foreach (var r in results.ToList()){
                listaChart.Add(new PChart(){title=r.title, value=r.value});
            }
           
            return listaChart as IEnumerable<PChart>;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
