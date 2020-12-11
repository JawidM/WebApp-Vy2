using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyModels
{
    public class Price
    {
        [Display(Name = "Price ID")]
        public int PriceID { get; set; }

        [Display(Name = "Route ID")]
        [Required(ErrorMessage = "A route should be provided for each price!")]
        public int RouteID { get; set; }

        [Display(Name = "Route Name")]
        public string RouteName { get; set; }

        [Display(Name = "Passenger Type")]
        [Required(ErrorMessage = "Passenger type should be provided!")]
        public string PassengerType { get; set; }

        [Display(Name = "Ticket Price")]
        [Required(ErrorMessage = "Ticket Price should be provided!")]
        public float TicketPrice { get; set; }
    }
}
