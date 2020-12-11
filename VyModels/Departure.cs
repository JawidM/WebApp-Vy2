using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyModels
{
    public class Departure
    {
        [Display(Name = "Departure ID")]
        public int DepartureID { get; set; }

        [Display(Name = "Route ID")]
        [Required(ErrorMessage = "Route ID should be provided!")]
        public int RouteID { get; set; }

        [Display(Name = "Station ID")]
        [Required(ErrorMessage = "Station should be provided for each departure.")]
        public int StationID { get; set; }

        [Display(Name = "Station Name")]
        public string StationName { get; set; }

        [Display(Name = "Departure Time")]
        [Required(ErrorMessage = "Departure Time should be provided!")]
        public DateTime DepartureTime { get; set; }
    }
}
