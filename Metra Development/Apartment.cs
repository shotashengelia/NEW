//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Metra_Development
{
    using System;
    using System.Collections.Generic;
    
    public partial class Apartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apartment()
        {
            this.Rooms = new HashSet<Room>();
        }
    
        public int ID { get; set; }
        public short Area { get; set; }
        public string ArchitectualDraw { get; set; }
        public byte Bedroom { get; set; }
        public byte Washroom { get; set; }
        public bool Status { get; set; }
        public int Price { get; set; }
        public int FloorID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual Floor Floor { get; set; }
    }
}