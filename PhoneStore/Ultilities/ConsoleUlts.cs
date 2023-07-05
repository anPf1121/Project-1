using System;
using Persistence;
using BL;
using Enum;

namespace Ults
{
    class ConsoleUlts
    {
        public void ConsoleForegroundColor(Enum.UI.Color colorEnum)
        {
            switch (colorEnum)
            {
                case Enum.UI.Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Enum.UI.Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Enum.UI.Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Enum.UI.Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Enum.UI.Color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
        }
        public void Line()
        {
            Console.WriteLine(@"                                                                                                                                                                            
█████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████");
        }

        public void TinyLine()
        {
            Console.WriteLine("============================================================");
        }
        public void Title(string? title, string? subTitle)
        {
            if (title != null)
            {
                ConsoleForegroundColor(Enum.UI.Color.White);
                Line();
                ConsoleForegroundColor(Enum.UI.Color.Red);
                Console.WriteLine("\n" + title);
                ConsoleForegroundColor(Enum.UI.Color.White);
                Line();
            }
            if (subTitle != null)
            {
                Line();
                ConsoleForegroundColor(Enum.UI.Color.Blue);
                Console.WriteLine("\n" + subTitle);
                ConsoleForegroundColor(Enum.UI.Color.White);
                Line();
            }
        }
        public void Alert(Feature.Alert alertType, string msg)
        {
            Ultilities Ultilities = new Ultilities();
            switch (alertType)
            {
                case Feature.Alert.Success:
                    ConsoleForegroundColor(Enum.UI.Color.Green);
                    Console.WriteLine("\n" + msg.ToUpper() + "✅");
                    break;
                case Feature.Alert.Warning:
                    ConsoleForegroundColor(Enum.UI.Color.Yellow);
                    Console.WriteLine("\n" + msg.ToUpper() + "⚠️");
                    break;
                case Feature.Alert.Error:
                    ConsoleForegroundColor(Enum.UI.Color.Red);
                    Console.WriteLine("\n" + msg.ToUpper() + "❌");
                    break;
                default:
                    break;
            }
            ;
            ConsoleForegroundColor(Enum.UI.Color.White);
            Ultilities.PressEnterTo("Continue");
        }
        public void PrintPhoneInfo(Phone phone)
        {
            Console.WriteLine("| {0, 10} | {1, 30} | {2, 15} | {3, 15} | {4, 15} | {5, 15}  |", phone.PhoneID, phone.PhoneName, phone.Brand, phone.Price, phone.OS, phone.Quantity);
        }
        public void PrintPhoneDetailsInfo(Phone phone)
        {
            TinyLine();
            Console.WriteLine("Phone ID: {0}", phone.PhoneID);
            Console.WriteLine("Phone Name: {0}", phone.PhoneName);
            Console.WriteLine("Brand: {0}", phone.Brand);
            Console.WriteLine("CPU: {0}", phone.CPU);
            Console.WriteLine("RAM: {0}", phone.RAM);
            Console.WriteLine("Discount Price: {0}", phone.DiscountPrice);
            Console.WriteLine("Price: {0}", phone.Price);
            Console.WriteLine("Battery: {0}", phone.BatteryCapacity);
            Console.WriteLine("OS: {0}", phone.OS);
            Console.WriteLine("Sim Slot: {0}", phone.SimSlot);
            Console.WriteLine("Screen Hz: {0}", phone.ScreenHz);
            Console.WriteLine("Screen Resolution: {0}", phone.ScreenResolution);
            Console.WriteLine("ROM: {0}", phone.ROM);
            Console.WriteLine("Storage Memory: {0}", phone.StorageMemory);
            Console.WriteLine("Mobile Network: {0}", phone.MobileNetwork);
            Console.WriteLine("Quantity: {0}", phone.Quantity);
            Console.WriteLine("Phone Size: {0}", phone.PhoneSize);
            TinyLine();
        }
    }
}