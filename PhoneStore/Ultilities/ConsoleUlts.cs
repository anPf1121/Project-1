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
                    break;
                case Feature.Alert.Warning:
                    ConsoleForegroundColor(Enum.UI.Color.Yellow);
                    break;
                case Feature.Alert.Error:
                    ConsoleForegroundColor(Enum.UI.Color.Red);
                    break;
                default:
                    break;
            }
            Console.WriteLine("\n" + msg.ToUpper() + "❌");
            ConsoleForegroundColor(Enum.UI.Color.White);
            Ultilities.PressEnterTo("Continue");
        }
        public void PrintPhoneInfo(Phone phone)
        {
            Console.WriteLine("| {0, 10} | {1, 30} | {2, 15} | {3, 15} | {4, 15} |", phone.PhoneID, phone.PhoneName, phone.Brand, phone.Price, phone.OS);
        }
    }
}