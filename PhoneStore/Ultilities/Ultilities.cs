using Persistence;
using BL;
using Enum;

namespace Ults
{
    class Ultilities
    {
        private PhoneBL phoneBL = new PhoneBL();
        private ConsoleUlts ConsoleUlts = new ConsoleUlts();
        private StaffBL StaffBL = new StaffBL();
        public int MenuHandle(string? title, string? subTitle, string[] menuItem)
        {
            int i = 0, choice;
            if (title != null || subTitle != null) ConsoleUlts.Title(title, subTitle);
            for (; i < menuItem.Count(); i++)
            {
                Console.WriteLine("\n" + menuItem[i] + " (" + (i + 1) + ")");
            }
            ConsoleUlts.Line();
            do
            {
                Console.Write("\n" + "👀 Your choice: ");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice <= 0 || choice > menuItem.Count())
                {
                    ConsoleUlts.Alert(Feature.Alert.Error, "Invalid Choice, Please Try Again");
                }
            } while (choice <= 0 || choice > menuItem.Count());
            return choice;
        }
        public bool? ListPhonePagination(List<Phone> listPhone)
        {
            if (listPhone != null)
            {
                ConsoleUlts consoleUlts = new ConsoleUlts();
                bool active = true;
                bool validInput = false;
                Dictionary<int, List<Phone>> phones = new Dictionary<int, List<Phone>>();
                phones = MenuPaginationHandle(listPhone);
                int countPage = phones.Count(), currentPage = 1;
                ConsoleKeyInfo input = new ConsoleKeyInfo();
                ConsoleKeyInfo input2 = new ConsoleKeyInfo();
                while (true)
                {
                    ConsoleUlts.Title(
                        null,
    @"     █████  ██████  ██████      ████████  ██████       ██████  ██████  ██████  ███████ ██████  
    ██   ██ ██   ██ ██   ██        ██    ██    ██     ██    ██ ██   ██ ██   ██ ██      ██   ██ 
    ███████ ██   ██ ██   ██        ██    ██    ██     ██    ██ ██████  ██   ██ █████   ██████  
    ██   ██ ██   ██ ██   ██        ██    ██    ██     ██    ██ ██   ██ ██   ██ ██      ██   ██ 
    ██   ██ ██████  ██████         ██     ██████       ██████  ██   ██ ██████  ███████ ██   ██ "
                    );
                    while (active)
                    {
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("| {0, 10} | {1, 30} | {2, 15} | {3, 15} | {4, 15} |", "ID", "Phone Name", "Brand", "Price", "OS");
                        Console.WriteLine("=================================================================================================================");
                        foreach (Phone phone in phones[currentPage])
                        {
                            ConsoleUlts.PrintPhoneInfo(phone);
                        }
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("{0,55}" + "< " + $"{currentPage}/{countPage}" + " >", " ");
                        Console.WriteLine("=================================================================================================================");
                        Console.WriteLine("Press 'Left Arrow' To Back Previous Page, 'Right Arror' To Next Page");
                        Console.Write("Press 'Space' To Start Creating Order, 'B' To Back Previous Menu");
                        input = Console.ReadKey();
                        if (currentPage <= countPage)
                        {
                            if (input.Key == ConsoleKey.RightArrow)
                            {
                                if (currentPage <= countPage - 1) currentPage++;
                                Console.Clear();
                            }
                            if (input.Key == ConsoleKey.LeftArrow)
                            {
                                if (currentPage > 1) currentPage--;
                                Console.Clear();
                            }

                            if (input.Key == ConsoleKey.B) return null;

                            if (input.Key == ConsoleKey.Spacebar)
                            {
                                Console.Clear();
                                Console.WriteLine("=================================================================================================================");
                                Console.WriteLine("| {0, 10} | {1, 30} | {2, 15} | {3, 15} | {4, 15} |", "ID", "Phone Name", "Brand", "Price", "OS");
                                Console.WriteLine("=================================================================================================================");

                                foreach (Phone phone in phones[currentPage])
                                {
                                    ConsoleUlts.PrintPhoneInfo(phone);
                                }
                                Console.WriteLine("=================================================================================================================");
                                Console.WriteLine("{0,55}" + "< " + $"{currentPage}/{countPage}" + " >", " ");
                                Console.WriteLine("=================================================================================================================");
                                do
                                {
                                    validInput = false;
                                    Console.WriteLine("Continue to create invoices or change the phone listing page ? Press Y to continue or press N to switch pages(Y / N):");
                                    input2 = Console.ReadKey(true);
                                    if (input2.Key == ConsoleKey.Y)
                                    {
                                        validInput = true;
                                        active = false;
                                        return true;
                                    }
                                    else if (input2.Key == ConsoleKey.N)
                                    {
                                        validInput = true;
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        consoleUlts.Alert(Feature.Alert.Error, "Invalid Input (Y/N)");
                                    }
                                } while (!validInput);
                            }
                        }
                    }
                }
            }
            else
            {
                ConsoleUlts.Alert(Feature.Alert.Warning, "Phone Not Found");
                PressEnterTo("Back To Previous Menu");
            }
            return false;
        }
        public Dictionary<int, List<Phone>> MenuPaginationHandle(List<Phone> phoneList)
        {
            List<Phone> sList = new List<Phone>();
            Dictionary<int, List<Phone>> menuTab = new Dictionary<int, List<Phone>>();
            int phoneQuantity = phoneList.Count(), itemInTab = 4, numberOfTab = 0, count = 1, secondCount = 1, idTab = 0;

            if (phoneQuantity % itemInTab != 0) numberOfTab = (phoneQuantity / itemInTab) + 1;
            else numberOfTab = phoneQuantity / itemInTab;

            foreach (Phone phone in phoneList)
            {
                if ((count - 1) == itemInTab)
                {
                    sList = new List<Phone>();
                    count = 1;
                }
                sList.Add(phone);
                if (sList.Count() == itemInTab)
                {
                    idTab++;
                    menuTab.Add(idTab, sList);
                }
                else if (sList.Count() < itemInTab && secondCount == phoneQuantity)
                {
                    idTab++;
                    menuTab.Add(idTab, sList);
                }
                secondCount++;
                count++;
            }
            return menuTab;
        }
        public int StartMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string[] menuItem = { "👉 Login", "👉 Exit" };

            int choice = MenuHandle(@"        ██████  ██   ██  ██████  ███    ██ ███████ ███████ ████████  ██████  ██████  ███████ 
        ██   ██ ██   ██ ██    ██ ████   ██ ██      ██         ██    ██    ██ ██   ██ ██      
        ██████  ███████ ██    ██ ██ ██  ██ █████   ███████    ██    ██    ██ ██████  █████   
        ██      ██   ██ ██    ██ ██  ██ ██ ██           ██    ██    ██    ██ ██   ██ ██      
        ██      ██   ██  ██████  ██   ████ ███████ ███████    ██     ██████  ██   ██ ███████ "
            , null, menuItem);

            if (choice == 1) return 1;
            else if (choice == 2) return 2;
            else return -1;
        }
        public int LoginUlt()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleUlts.Title(null, @"                            ██       ██████   ██████  ██ ███    ██ 
                            ██      ██    ██ ██       ██ ████   ██ 
                            ██      ██    ██ ██   ███ ██ ██ ██  ██ 
                            ██      ██    ██ ██    ██ ██ ██  ██ ██ 
                            ███████  ██████   ██████  ██ ██   ████ ");
            Console.Write("\n👉 User Name: ");
            string userName = Console.ReadLine() ?? "";
            Console.Write("\n👉 Password: ");
            string pass = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    // Xóa ký tự cuối cùng trong chuỗi pass khi người dùng nhấn phím Backspace
                    pass = pass.Substring(0, pass.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            pass = pass.Substring(0, pass.Length - 1);
            return StaffBL.Authenticate(userName, pass);
        }
        public void PressEnterTo(string? action)
        {
            if (action != null)
            {
                Console.Write($"\n👉 Press Enter To {action}...");
            }
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter) return;
            else PressEnterTo(null);
        }
        public int CreateOrderMenuHandle()
        {
            int reEnterOrCancel;
            bool active = true, activeSearchPhone = true;
            List<Phone>? phones = phoneBL.GetAllItem();
            if (phones != null)
            {
                List<Phone>? listSearch = new List<Phone>();
                string searchPhone = "";
                string[] menuItem = { "👉 Search Phone By Information", "👉 Back To Previous Menu" };
                string[] menuOption = { "👉 Re-Enter Phone Information", "👉 Cancel Order" };
                while (active)
                {
                    switch (MenuHandle(
                       null,
        @"     ██████ ██████  ███████  █████  ████████ ███████      ██████  ██████  ██████  ███████ ██████  
    ██      ██   ██ ██      ██   ██    ██    ██          ██    ██ ██   ██ ██   ██ ██      ██   ██ 
    ██      ██████  █████   ███████    ██    █████       ██    ██ ██████  ██   ██ █████   ██████  
    ██      ██   ██ ██      ██   ██    ██    ██          ██    ██ ██   ██ ██   ██ ██      ██   ██ 
     ██████ ██   ██ ███████ ██   ██    ██    ███████      ██████  ██   ██ ██████  ███████ ██   ██ ", menuItem))
                    {
                        case 1:
                            do
                            {
                                ConsoleUlts.Title(null,
                                @"    ███████ ███████  █████  ██████   ██████ ██   ██     ██████  ██   ██  ██████  ███    ██ ███████ 
    ██      ██      ██   ██ ██   ██ ██      ██   ██     ██   ██ ██   ██ ██    ██ ████   ██ ██      
    ███████ █████   ███████ ██████  ██      ███████     ██████  ███████ ██    ██ ██ ██  ██ █████   
         ██ ██      ██   ██ ██   ██ ██      ██   ██     ██      ██   ██ ██    ██ ██  ██ ██ ██      
    ███████ ███████ ██   ██ ██   ██  ██████ ██   ██     ██      ██   ██  ██████  ██   ████ ███████ "
                                );
                                Console.Write("\nEnter Phone Information To Search: ");
                                searchPhone = Console.ReadLine() ?? "";
                                listSearch = phoneBL.SearchByPhoneInformation(searchPhone);
                                if (listSearch == null)
                                {
                                    ConsoleUlts.Alert(Feature.Alert.Warning, "Phone Not Found");
                                    ConsoleUlts.Title(null,
                                    @"    ███████ ███████  █████  ██████   ██████ ██   ██     ██████  ██   ██  ██████  ███    ██ ███████ 
    ██      ██      ██   ██ ██   ██ ██      ██   ██     ██   ██ ██   ██ ██    ██ ████   ██ ██      
    ███████ █████   ███████ ██████  ██      ███████     ██████  ███████ ██    ██ ██ ██  ██ █████   
         ██ ██      ██   ██ ██   ██ ██      ██   ██     ██      ██   ██ ██    ██ ██  ██ ██ ██      
    ███████ ███████ ██   ██ ██   ██  ██████ ██   ██     ██      ██   ██  ██████  ██   ████ ███████ "
                                    );
                                    reEnterOrCancel = MenuHandle(null, null, menuOption);
                                    switch (reEnterOrCancel)
                                    {
                                        case (int)Enum.Feature.SearchPhone.ReEnterPhoneInfo:
                                            PressEnterTo("Re-Enter Phone Infomation");
                                            break;
                                        case (int)Enum.Feature.SearchPhone.CancelOrder:
                                            PressEnterTo("Back Previous Menu");
                                            activeSearchPhone = false;
                                            break;

                                    }
                                }
                                else
                                {
                                    bool? temp = ListPhonePagination(listSearch);
                                    int phoneId;
                                    if (temp != null)
                                    {
                                        do
                                        {
                                            Console.Write("👉 Input Phone ID To Add To Order: ");
                                            int.TryParse(Console.ReadLine(), out phoneId);

                                            if (phoneId <= 0 || phoneId > phones.Count())
                                                ConsoleUlts.Alert(Feature.Alert.Error, "Invalid Phone ID, Please Try Again");

                                        } while (phoneId <= 0 || phoneId > phones.Count());
                                        return 1;
                                    }
                                    else return 0;
                                }
                            } while (activeSearchPhone);
                            break;
                        case 2:
                            active = false;
                            return 0;
                    }
                }
            }
            else return -1;
            return 1;
        }
        public void HandleOrderMenuHandle()
        {
            bool active = true;

            string[] menuItem = { "👉 Show order by paid status in day", "👉 Back To Previous Menu" };
            while (active)
            {
                switch (MenuHandle(
                    null,
                    @"    ██   ██  █████  ███    ██ ██████  ██      ███████      ██████  ██████  ██████  ███████ ██████  
    ██   ██ ██   ██ ████   ██ ██   ██ ██      ██          ██    ██ ██   ██ ██   ██ ██      ██   ██ 
    ███████ ███████ ██ ██  ██ ██   ██ ██      █████       ██    ██ ██████  ██   ██ █████   ██████  
    ██   ██ ██   ██ ██  ██ ██ ██   ██ ██      ██          ██    ██ ██   ██ ██   ██ ██      ██   ██ 
    ██   ██ ██   ██ ██   ████ ██████  ███████ ███████      ██████  ██   ██ ██████  ███████ ██   ██ ", menuItem))
                {
                    case 1:
                        ConsoleUlts.Title(null,
                        @"            ███████ ██   ██  ██████  ██     ██      ██████  ██████  ██████  ███████ ██████  
            ██      ██   ██ ██    ██ ██     ██     ██    ██ ██   ██ ██   ██ ██      ██   ██ 
            ███████ ███████ ██    ██ ██  █  ██     ██    ██ ██████  ██   ██ █████   ██████  
                 ██ ██   ██ ██    ██ ██ ███ ██     ██    ██ ██   ██ ██   ██ ██      ██   ██ 
            ███████ ██   ██  ██████   ███ ███       ██████  ██   ██ ██████  ███████ ██   ██ "
                        );
                        string orderId = "";
                        Console.Write("Input order id:");
                        orderId = Console.ReadLine() ?? "";
                        break;
                    case 2:
                        active = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public int SellerMenu()
        {
            int result = 0;
            bool active = true;
            string[] menuItem = { "👉 Create Order", "👉 Handle Order", "👉 Log Out" };
            while (active)
            {
                switch (MenuHandle(
                    null
    , @"                                ███████ ███████ ██      ██      ███████ ██████  
                                ██      ██      ██      ██      ██      ██   ██ 
                                ███████ █████   ██      ██      █████   ██████  
                                     ██ ██      ██      ██      ██      ██   ██ 
                                ███████ ███████ ███████ ███████ ███████ ██   ██ ", menuItem))
                {
                    case 1:
                        int createOrderStatus = CreateOrderMenuHandle();
                        if (createOrderStatus == 1) ConsoleUlts.Alert(Feature.Alert.Success, "Create Order Completed");
                        else if (createOrderStatus == -1) ConsoleUlts.Alert(Feature.Alert.Error, "Don't Have Any Phone To Create Order");
                        else break;
                        break;
                    case 2:
                        HandleOrderMenuHandle();
                        break;
                    case 3:
                        active = false;
                        result = 1;
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
        public int AccountantMenu()
        {
            int result = 0;
            bool active = true;
            Ultilities ultilities = new Ultilities();
            string[] menuItem = { "👉 Select Order", "👉 Payment", "👉 Revenue 1 Day", "👉 Log Out" };
            while (active)
            {
                switch (ultilities.MenuHandle(null,
                @"              █████   ██████  ██████  ██████  ██    ██ ███    ██ ████████  █████  ███    ██ ████████ 
             ██   ██ ██      ██      ██    ██ ██    ██ ████   ██    ██    ██   ██ ████   ██    ██    
             ███████ ██      ██      ██    ██ ██    ██ ██ ██  ██    ██    ███████ ██ ██  ██    ██    
             ██   ██ ██      ██      ██    ██ ██    ██ ██  ██ ██    ██    ██   ██ ██  ██ ██    ██    
             ██   ██  ██████  ██████  ██████   ██████  ██   ████    ██    ██   ██ ██   ████    ██ ", menuItem))
                {
                    case 1:
                        Console.WriteLine("INPUT ORDER ID:");
                        break;
                    case 2:
                        Console.WriteLine("Payment method");
                        break;
                    case 3:
                        Console.WriteLine("Revenue method");
                        break;
                    case 4:
                        active = false;
                        result = 1;
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}