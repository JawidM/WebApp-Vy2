using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    [Table("Stations")]
    public class StationDb
    {
        [Key]
        public int StationID { get; set; }

        public string StationName { get; set; }

    }
}
