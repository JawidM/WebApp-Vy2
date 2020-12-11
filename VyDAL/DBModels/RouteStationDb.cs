using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    [Table("RouteStations")]
    public class RouteStationDb
    {

        [Key, Column(Order = 1)]
        public int RouteID { get; set; }

        [Key, Column(Order = 2)]
        public int StationID { get; set; }

        public int StationNumber { get; set; }


    }
}
