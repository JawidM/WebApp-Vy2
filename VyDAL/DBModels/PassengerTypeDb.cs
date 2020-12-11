using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    [Table("PassengerTypes")]
    public class PassengerTypeDb
    {
        [Key]
        public int PassengerTypeID { get; set; }

        public string PassengerType { get; set; }


    }
}
