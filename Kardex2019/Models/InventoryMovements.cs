//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kardex2019.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InventoryMovements
    {
        public int InventoryMovementId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public System.DateTime DateTime { get; set; }
        public int Type { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}