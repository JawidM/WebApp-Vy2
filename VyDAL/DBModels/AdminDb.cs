using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyDAL
{
    public class AdminDb
    {
        [Key]
        public string Email { get; set; }

        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}