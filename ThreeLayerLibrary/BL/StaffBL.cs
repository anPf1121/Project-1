using System.Collections.Generic;
using Persistence;
using DAL;
using System.Text;
using System.Security.Cryptography;

namespace BL;
public class StaffBL
{
    private StaffDAL idal = new StaffDAL();
    public Staff? Authenticate(string username, string password)
    {
        Staff staff = new Staff();
        staff = idal.GetAccountByUsername(username);

        string hashedInputPassword = idal.CreateMD5(password);
        if (hashedInputPassword.Equals(staff.Password))
        {
            if (staff.Role == E.Staff.Role.Seller)
                return staff;

            else if (staff.Role == E.Staff.Role.Accountant)
                return staff;

            else 
                return null;
        }
        else return null;
    }
}