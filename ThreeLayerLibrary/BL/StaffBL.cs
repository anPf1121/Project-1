using System.Collections.Generic;
using Persistence;
using DAL;
using System.Text;
using System.Security.Cryptography;

namespace BL;
public class StaffBL
{
    private StaffDAL idal = new StaffDAL();
    public int Authenticate(string username, string password)
    {
        Staff staff = new Staff();
        staff = idal.GetAccountByUsername(username);

        string hashedInputPassword = idal.CreateMD5(password);
        if (hashedInputPassword.Equals(staff.Password))
        {
            if (staff.TitleID == (int)Enum.Staff.Role.Seller)
                return (int)Enum.Staff.Role.Seller;

            else if (staff.TitleID == (int)Enum.Staff.Role.Accountant)
                return (int)Enum.Staff.Role.Accountant;

            else 
                return -1;
        }
        else return -1;
    }
}