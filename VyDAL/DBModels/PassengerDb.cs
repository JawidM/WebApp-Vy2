using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VyDAL
{
    [Table("Passengers")]
    public class PassengerDb
    {
        [Key]
        public int PassengerID { get; set; }

        public int PassengerTypeID { get; set; }

        //public virtual PassengerTypeDb PassengerType { get; set; }




    }
}