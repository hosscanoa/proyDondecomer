using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyDondecomer.Models;
using System.Web.Http;

namespace proyDondecomer.Controllers
{
    public class ResumenController : ApiController
    {
        //
        // GET: /Resumen/

        private dondeComerEntities db = new dondeComerEntities();


        public ResumenController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<PChart> GetResumen()
        {
            var results = (from T1 in db.Menu_Producto
                           join T2 in db.Menu on T1.menuID equals T2.menuID
                           join T3 in db.Restaurante on T2.restauranteID equals T3.restauranteID
                           group T3 by new { T3.nombre } into xml
                           select new
                           {
                               title = xml.Key.nombre,
                               value = xml.Count()
                           }).Take(5);
            List<PChart> listaChart = new List<PChart>();
            foreach (var r in results.ToList())
            {
                listaChart.Add(new PChart() { title = r.title, value = r.value });
            }

            return listaChart as IEnumerable<PChart>;
        }

    }
}
