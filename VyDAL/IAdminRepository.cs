using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public interface IAdminRepository
    {
        bool UserExsist(Admin admin);

    }
}
