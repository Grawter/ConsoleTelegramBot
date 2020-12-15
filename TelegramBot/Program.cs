using System;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            string _token = "";
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // протокол безопасности

                TelegramBotHelper hlp = new TelegramBotHelper(token: _token);
                hlp.GetUpdates(); // Получение апдейтов (сообщений пользователя)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}