using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    [Table("Ticket")]

    public class TicketDb
    {
        [Key]
        public int TicketID { get; set; }

        public float TicketPrice { get; set; }

        public int TicketDuration { get; set; }

        public string StartStation { get; set; }

        public string EndStation { get; set; }

        public DateTime TicketPurchaseDate { get; set; }

        public DateTime TicketPurchaseTime { get; set; }

        public string OrderedBy { get; set; }

        //public virtual RouteDb TicketRoute { get; set; }
        public  int TicketRouteID { get; set; }


        public virtual ICollection<PassengerDb> TicketPassengers { get; set; }
    }
}
