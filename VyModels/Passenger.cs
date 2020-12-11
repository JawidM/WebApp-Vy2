using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyModels
{
    public class Passenger
    {
        [Display(Name = "Passenger ID")]
        public int PassengerID { get; set; }

        [Display(Name = "Passenger Type")]
        public string PassengerType { get; set; }

        [Display(Name = "Ticket Price")]
        public float TicketPrice { get; set; }


    }
}
