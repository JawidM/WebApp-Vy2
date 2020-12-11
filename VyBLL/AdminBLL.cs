using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyDAL;
using VyModels;

namespace VyBLL
{
    public class AdminBLL : IAdminLogic
    {
        private IAdminRepository _repository;

        public AdminBLL()
        {
            _repository = new AdminRepository();
        }

        public AdminBLL (IAdminRepository stub)
        {
            _repository = stub;
        }

        public bool UserExsist(Admin admin)
        {
            return _repository.UserExsist(admin);
        }
    }
}
