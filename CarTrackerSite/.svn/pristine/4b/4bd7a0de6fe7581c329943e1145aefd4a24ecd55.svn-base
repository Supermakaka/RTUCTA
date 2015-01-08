using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BusinessLogic.Models
{
    public class Location
    {
        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime Time { get; set; }
        public Decimal Altitude { get; set; }
        public Decimal Accuracy { get; set; }
        public virtual Car Car { get; set; }
        public int CarId { get; set; }
    }
}
