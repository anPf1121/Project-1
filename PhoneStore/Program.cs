using System;
using System.Collections.Generic;
using Persistence;
using Ults;
using BL;
using MySql.Data.MySqlClient;
using DAL;


namespace PhoneStoreUI
{
    class Program
    {
        static void Main()
        {
            PhoneBL phoneBL = new PhoneBL();
            Ultilities Ultilities = new Ultilities();
            ConsoleUlts ConsoleUlts = new ConsoleUlts();
            Staff? loginAccount = new Staff();
            int mainChoice = 0, SellerAccount = 0, AccountantAccount = 0;
            bool active = true;
            do
            {
                do
                {
                    mainChoice = Ultilities.StartMenu();
                    if (mainChoice == 1)
                    {
                        loginAccount.Role = Ultilities.LoginUlt();

                        if (loginAccount.Role == E.Staff.Role.Seller)
                            SellerAccount = Ultilities.SellerMenu();

                        else if (loginAccount.Role == E.Staff.Role.Accountant)
                            AccountantAccount = Ultilities.AccountantMenu();

                        else {
                            ConsoleUlts.Alert(E.Feature.Alert.Error, "Invalid Username Or Password");
                            Main();
                        }
                    }
                    else if (mainChoice == 2)
                    {
                        ConsoleUlts.Alert(E.Feature.Alert.Success, "Exiting Success");
                        return;
                    }
                    else
                    {
                        ConsoleUlts.Alert(E.Feature.Alert.Error, "Invalid Choice");
                        Main();
                    }
                } while (mainChoice != 2);
            } while (active);
        }
    }
}


