using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    [Table("Price")]
    public class PriceDb
    {
        [Key]
        public int PriceID { get; set; }

        public int RouteID { get; set; }

        public int PassengerTypeID { get; set; }

        public float TicketPrice { get; set; }


    }
}
