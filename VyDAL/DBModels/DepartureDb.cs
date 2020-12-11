using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    [Table("Departures")]
    public class DepartureDb
    {
        [Key]
        public int DepartureID { get; set; }

        public int RouteID { get; set; }

        public int StationID { get; set; }

        public DateTime DepartureTime { get; set; }
    }
}