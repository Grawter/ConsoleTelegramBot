using System;
using System.Collections.Generic;
using AngleSharp;
using AngleSharp.Dom;
using System.Threading.Tasks;

namespace News.News_portal
{
    internal class RiaNews
    {
        public List<string[]> Ria_main { get; private set; }
        public List<string[]> Ria_politic { get; private set; }
        public List<string[]> Ria_economic { get; private set; }
        public List<string[]> Ria_sport { get; private set; }
        private string url = "https://ria.ru";


        private async Task<AngleSharp.Dom.IDocument> GetDoc(string _address) // можно только через асинхронную функции, иначе просто никак
        {
            // конфигурация
            var config = Configuration.Default.WithDefaultLoader();
            // адрес запроса
            string address = _address;
            // Весь HTML код. Можно использовать var
            AngleSharp.Dom.IDocument doc = await BrowsingContext.New(config).OpenAsync(address);
            // возвращаем HTML файл всего сайта
            return doc;
        }

        public async Task<List<string[]>> GetMain()
        {
            Ria_main = new List<string[]>();

            var doc = await GetDoc(url);

            int Ria_main_count_1 = doc.QuerySelector("div.cell.cell-list.m-title:nth-child(1) > div:nth-child(2)").ChildElementCount;
            int Ria_main_count_2 = doc.QuerySelector("div.floor:nth-child(2) > div > div > div > div > div > div:nth-child(2)").ChildElementCount;
            int min = Ria_main_count_1 > Ria_main_count_2 ? Ria_main_count_2 : Ria_main_count_1;

            for (int i = 1; i < min + 1; i++)
            {
                string[] item_first = new string[2];
                string[] item_second = new string[2];
                try
                {
                    item_first[0] = doc.QuerySelector("div.cell.cell-list.m-title:nth-child(1) > div:nth-child(2) > div:nth-child(" + i + ") > a").Text();
                    item_first[1] = doc.QuerySelector("div.cell.cell-list.m-title:nth-child(1) > div:nth-child(2) > div:nth-child(" + i + ") > a").GetAttribute("href");
                    item_second[0] = doc.QuerySelector("div.floor:nth-child(2) > div > div > div > div > div > div:nth-child(2) > div:nth-child(" + i + ") > a").Text();
                    item_second[1] = doc.QuerySelector("div.floor:nth-child(2) > div > div > div > div > div > div:nth-child(2) > div:nth-child(" + i + ") > a").GetAttribute("href");
                    Ria_main.Add(item_first);
                    Ria_main.Add(item_second);
                }
                catch (Exception)
                {
                }              
            }

            return Ria_main;
        }

        public async Task<List<string[]>> GetPolitic()
        {
            Ria_politic = new List<string[]>();
            string address = "https://ria.ru/politics/";

            var doc = await GetDoc(address);

            int Ria_politic_count = doc.QuerySelector("div.list.list-tags").ChildElementCount;

            for (int i = 1; i < Ria_politic_count + 1; i++)
            {
                string[] item = new string[2];
                try
                {
                    item[0] = doc.QuerySelector("div.list.list-tags > div.list-item:nth-child(" + i + ") > div.list-item__content > a:nth-child(2)").Text();
                    item[1] = doc.QuerySelector("div.list.list-tags > div.list-item:nth-child(" + i + ") > div.list-item__content > a:nth-child(2)").GetAttribute("href");
                    Ria_politic.Add(item);
                }
                catch (Exception)
                {
                }
                
                if (i == 4)
                    i = 9;
            }

            return Ria_politic;
        }

        public async Task<List<string[]>> GetEconomic()
        {
            Ria_economic = new List<string[]>();
            string address = "https://ria.ru/economy/";

            var doc = await GetDoc(address);

            int Ria_economic_count = doc.QuerySelector("div.list.list-tags").ChildElementCount;

            for (int i = 1; i < Ria_economic_count + 1; i++)
            {
                try
                {
                    string[] item = new string[2];
                    item[0] = doc.QuerySelector("div.list.list-tags > div.list-item:nth-child(" + i + ") > div.list-item__content > a:nth-child(2)").Text();
                    item[1] = doc.QuerySelector("div.list.list-tags > div.list-item:nth-child(" + i + ") > div.list-item__content > a:nth-child(2)").GetAttribute("href");
                    Ria_economic.Add(item);
                }
                catch (Exception)
                {
                }               

                if (i == 4)
                    i = 9;
                
            }

            return Ria_economic;
        }

        public async Task<List<string[]>> GetSport()
        {
            Ria_sport = new List<string[]>();
            string address = "https://rsport.ria.ru";

            var doc = await GetDoc(address);

            int Ria_sport_count = doc.QuerySelector("div.cell.cell-list.m-title:nth-child(1) > div:nth-child(2)").ChildElementCount;

            for (int i = 1; i < Ria_sport_count + 1; i++)
            {
                string[] item = new string[2];

                try
                {
                    item[0] = doc.QuerySelector("div.cell.cell-list.m-title:nth-child(1) > div:nth-child(2) > div:nth-child(" + i + ") > a").Text();
                    item[1] = doc.QuerySelector("div.cell.cell-list.m-title:nth-child(1) > div:nth-child(2) > div:nth-child(" + i + ") > a").GetAttribute("href");
                    Ria_sport.Add(item);
                }
                catch (Exception)
                {
                }        
            }

            return Ria_sport;
        }

    }
}
