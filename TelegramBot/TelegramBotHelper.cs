using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;
using News;

namespace TelegramBot
{
    internal class TelegramBotHelper
    {
        private string _token;
        Telegram.Bot.TelegramBotClient _client;
        public TelegramBotHelper(string token)
        {
            _token = token;
        }

        internal void GetUpdates()
        {
            _client = new Telegram.Bot.TelegramBotClient(_token);
            var me = _client.GetMeAsync().Result; // информация о боте
            if (me != null && !string.IsNullOrEmpty(me.Username))
            {
                Console.WriteLine("Бот запущен");
                int offset = 0;
                while (true)
                {
                    try
                    {
                        var updates = _client.GetUpdatesAsync(offset).Result; // получаем сам апдейт
                        if (updates != null && updates.Count() > 0)
                        {
                            foreach (var update in updates)
                            {
                                if (update.Message != null)
                                    Console.WriteLine(DateTime.Now + " От пользователя: " + update.Message.Chat.FirstName + " - "
                                                     + update.Message.From + " Текст: " + update.Message.Text);
                                else if (update.CallbackQuery != null)
                                    Console.WriteLine(DateTime.Now + " От пользователя: " + update.CallbackQuery.Message.Chat.FirstName + " - "
                                                     + update.CallbackQuery.From + " CallbackQuery: " + update.CallbackQuery.Data);                           

                                processUpdate(update); // обработка сообщения
                                offset = update.Id + 1; // переключение на след. сообщение
                            }
                        }
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine("Ошибка в работе бота");
                        Console.WriteLine(ex.Message); 
                    }

                    Thread.Sleep(1000);
                }
            }
        }

        private void processUpdate(Telegram.Bot.Types.Update update)
        {
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    var text = update.Message.Text;

                    switch (text)
                    {
                        case "/Start":
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Бот запушен\t\n Для получения новостей воспользуйтесь командой /News");
                            break;
                        case "/start":
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Бот запушен\t\n Для получения новостей воспользуйтесь командой /News");
                            break;
                        case "/News":
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Выберите интересующий вас новостной раздел", replyMarkup: GetInlineButtons());
                            break;
                        case "/news":
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Выберите интересующий вас новостной раздел", replyMarkup: GetInlineButtons());
                            break;
                        default:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Неизвестная команда - " + text + "\t\n Для получения новостей воспользуйтесь командой /News");
                            break;
                    }
                    break;


                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:  
                    NewsAgregator NA = new NewsAgregator();

                    switch (update.CallbackQuery.Data)
                    {                    
                        case "1":
                            var msg1 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "\ud83d\udce2 ГЛАВНОЕ:\t\n" + NA.GetMain().Result, replyMarkup: GetInlineButtons());
                            break;
                        case "2":
                            var msg2 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "\u2694\ufe0f ПОЛИТИКА:\t\n" + NA.GetPolitic().Result, replyMarkup: GetInlineButtons());
                            break;
                        case "3":
                            var msg3 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "\ud83d\udcbc ЭКОНОМИКА:\t\n" + NA.GetEconomic().Result, replyMarkup: GetInlineButtons());
                            break;
                        case "4":
                            var msg4 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "\ud83e\udd4a СПОРТ:\t\n" + NA.GetSport().Result, replyMarkup: GetInlineButtons());
                            break;
                        case "5":
                            var msg5 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "\ud83d\udcb5 КУРС ВАЛЮТ:\t\n" + NA.GetRates().Result);
                            break;
                    }
                    break;

                default:
                    Console.WriteLine(update.Type + " Данный тип сообщения не обрабатывается");
                    break;
            }
        }

        private IReplyMarkup GetInlineButtons()
        {
           return new InlineKeyboardMarkup(new[]
           {
               new []
               {
                   InlineKeyboardButton.WithCallbackData("Главное", "1"),
               },
               new [] 
               {
                  new InlineKeyboardButton{ Text = "Политика", CallbackData = "2" },
               },
               new [] 
               {
                  new InlineKeyboardButton { Text = "Экономика", CallbackData = "3" }
               },
               new [] 
               {
                  new InlineKeyboardButton { Text = "Спорт", CallbackData = "4" }
               },
               new [] 
               {
                  new InlineKeyboardButton { Text = "Валютный курс", CallbackData = "5" }
               },
           });

        }
    }
}