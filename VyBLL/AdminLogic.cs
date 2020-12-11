using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyDAL;
using VyModels;

namespace VyBLL
{
    public class AdminLogic
    {
        public bool UserExsist(Admin admin)
        {
            var AdminRepo = new AdminRepository();
            var AdminExisit = AdminRepo.UserExsist(admin);
            return AdminExisit;
        }
    }
}