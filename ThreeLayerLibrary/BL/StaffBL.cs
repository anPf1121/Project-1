using System.Collections.Generic;
using Persistence;
using DAL;
using System.Text;
using System.Security.Cryptography;

namespace BL;
public class StaffBL
{
    private StaffDAL idal = new StaffDAL();
    public E.Staff.Role? Authenticate(string username, string password)
    {
        Staff? staff = null;
        staff = idal.GetAccountByUsername(username);

        if (staff != null)
        {
            string hashedInputPassword = idal.CreateMD5(password);
            if (hashedInputPassword.Equals(staff.Password))
            {
                if (staff.Role == E.Staff.Role.Seller)
                    return E.Staff.Role.Seller;

                else if (staff.Role == E.Staff.Role.Accountant)
                    return E.Staff.Role.Accountant;

                else
                    return null;
            }
            else return null;
        }
        else return null;
    }
}