using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    [Table("Routes")]

    public class RouteDb
    {

        [Key]
        public int RouteID { get; set; }

        public string RouteName { get; set; }


    }
}