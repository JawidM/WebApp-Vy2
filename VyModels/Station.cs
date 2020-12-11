using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyModels
{
    public class Station
    {
        [Display(Name = "Station ID")]
        public int StationID { get; set; }

        [Display(Name = "Station Name")]
        [Required(ErrorMessage = "Station name should be provided!")]
        public string StationName { get; set; }

        [Display(Name = "Station Number")]
        [Required(ErrorMessage = "Station number should be provided!")]
        public int StationNumber { get; set; }

    }
}
