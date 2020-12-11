using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyModels
{
    public class Route
    {
        [Display(Name = "Route ID")]
        public int RouteID { get; set; }

        [Display(Name = "Route Name")]
        [Required(ErrorMessage = "Route name should be provided!")]
        public string RouteName { get; set; }

        public List<Station> RouteStations { get; set; }

        public List<Departure> RouteDepartures { get; set; }

    }
}
