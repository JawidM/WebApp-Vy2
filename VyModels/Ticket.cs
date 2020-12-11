using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyModels
{
    public class Ticket
    {
        [Display(Name = "Ticket ID")]
        public int TicketID { get; set; }

        [Display(Name = "Ticket Price")]
        public float TicketPrice { get; set; }

        [Display(Name = "Ticket Duration")]
        public int TicketDuration { get; set; }

        [Display(Name = "Start Station")]
        [Required(ErrorMessage = "Start Station should be provided!")]
        public string StartStation { get; set; }
        [Display(Name = "End Station")]
        [Required(ErrorMessage = "End Station should be provided!")]
        public string EndStation { get; set; }

        [Display(Name = "Purchase Date")]
        [Required(ErrorMessage = "Date should be provided!")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TicketPurchaseDate { get; set; }

        [Display(Name = "Purchase Time")]
        public DateTime TicketPurchaseTime { get; set; }

        [Display(Name = "Ordered By")]
        public string OrderedBy { get; set; }

        public Route TicketRoute { get; set; }

        public List<Passenger> TicketPassengers { get; set; }


    


    }
}
