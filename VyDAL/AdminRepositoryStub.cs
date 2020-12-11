using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class AdminRepositoryStub : IAdminRepository
    {
        public bool UserExsist(Admin admin)
        {
            if (admin.Email == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static byte[] fixHash(string innPassord, byte[] innSalt)
        {
            const int keyLength = 24;
            var pbkdf2 = new Rfc2898DeriveBytes(innPassord, innSalt, 1000); // 1000 angir hvor mange ganger hash funskjonen skal utføres for økt sikkerhet
            return pbkdf2.GetBytes(keyLength);
        }

    }
}
