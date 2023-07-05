using System;
using System.Collections.Generic;
using Persistence;
using Ults;
using BL;
using Enum;
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
            int loginAccount = 0, mainChoice = 0, SellerAccount = 0, AccountantAccount = 0;
            bool active = true;
            do
            {
                do
                {
                    mainChoice = Ultilities.StartMenu();
                    if (mainChoice == 1)
                    {
                        loginAccount = Ultilities.LoginUlt();

                        if (loginAccount == (int)Enum.Account.AccountType.Seller)
                            SellerAccount = Ultilities.SellerMenu();

                        else if (loginAccount == (int)Enum.Account.AccountType.Accountant)
                            AccountantAccount = Ultilities.AccountantMenu();

                        else {
                            ConsoleUlts.Alert(Feature.Alert.Error, "Invalid Username Or Password");
                            Main();
                        }
                    }
                    else if (mainChoice == 2)
                    {
                        ConsoleUlts.Alert(Feature.Alert.Success, "Exiting Success");
                        return;
                    }
                    else
                    {
                        ConsoleUlts.Alert(Feature.Alert.Error, "Invalid Choice");
                        Main();
                    }
                } while (mainChoice != 2);
            } while (active);
        }
    }
}


