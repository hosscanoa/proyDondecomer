//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proyDondecomer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu_Producto
    {
        public int MenuProductoID { get; set; }
        public int menuID { get; set; }
        public int productoID { get; set; }
    
        public virtual Menu Menu { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
