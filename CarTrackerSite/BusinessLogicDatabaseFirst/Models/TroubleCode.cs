//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLogic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TroubleCode
    {
        public TroubleCode()
        {
            this.Cars = new HashSet<Car>();
        }
    
        public int ID { get; set; }
        public string DTCNumber { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Car> Cars { get; set; }
    }
}
