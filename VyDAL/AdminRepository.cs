using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VyModels;

namespace VyDAL
{
    public class AdminRepository : IAdminRepository
    {
        public bool UserExsist(Admin admin)
        {
            using (var db = new DB())
            {
                AdminDb funnetBruker = db.Admins.FirstOrDefault(b => b.Email == admin.Email);
                if (funnetBruker != null)
                {
                    byte[] passordForTest = fixHash(admin.Password, funnetBruker.Salt);
                    bool riktigBruker = funnetBruker.Password.SequenceEqual(passordForTest);  // merk denne testen!
                    return riktigBruker;
                }
                else
                {
                    return false;
                }
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