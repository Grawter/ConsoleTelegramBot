using System;
using System.Collections.Generic;
using AngleSharp;
using AngleSharp.Dom;
using System.Threading.Tasks;

namespace News.News_portal
{
    internal class LentaNews
    {
        public List<string[]> Lenta_main { get; private set; }
        public List<string[]> Lenta_politic{ get; private set; }
        public List<string[]> Lenta_economic { get; private set; }
        public List<string[]> Lenta_sport { get; private set; }
        private string url = "https://lenta.ru";

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
            Lenta_main = new List<string[]>();

            var doc = await GetDoc(url);
            int Lenta_main_count = doc.QuerySelector("div.b-yellow-box__wrap").ChildElementCount;
            
            for (int i = 2; i < Lenta_main_count + 1; i++)
            {
                string[] item = new string[2];
                item[0] = doc.QuerySelector("div.b-yellow-box__wrap > div:nth-child(" + i + ")").Text();
                item[1] = url + doc.QuerySelector("div.b-yellow-box__wrap > div:nth-child(" + i + ") > a").GetAttribute("href");
                Lenta_main.Add(item);
            }

            return Lenta_main;
        }

        public async Task<List<string[]>> GetPolitic()
        {
            Lenta_politic = new List<string[]>();
            string address = "https://lenta.ru/rubrics/world/politic";
            string[] item_first = new string[2];

            var doc = await GetDoc(address);
            int Lenta_politic_count_W = doc.QuerySelector("div.news-list").ChildElementCount;

            item_first[0] = doc.QuerySelector("div.b-feature__header > a").Text();
            item_first[1] = url + doc.QuerySelector("div.b-feature__header > a").GetAttribute("href");
            Lenta_politic.Add(item_first);
            for (int i = 1; i < Lenta_politic_count_W + 1; i++)
            {
                string[] item_second = new string[2];
                try
                {
                    item_second[0] = doc.QuerySelector("div.news-list > div:nth-child(" + i + ") > h4 > a").Text();
                    item_second[1] = url + doc.QuerySelector("div.news-list > div:nth-child(" + i + ") > h4 > a").GetAttribute("href");
                    Lenta_politic.Add(item_second);
                }
                catch (Exception)
                {
                }
                
            }

            return Lenta_politic;
        }

        public async Task<List<string[]>> GetEconomic()
        {
            Lenta_economic = new List<string[]>();
            string address = "https://lenta.ru/rubrics/economics/";

            var doc = await GetDoc(address);

            string[] item_first = new string[2];
            string[] item_second = new string[2];
            string[] item_third = new string[2];

            int Lenta_economic_count_1 = doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column").ChildElementCount;
            int Lenta_economic_count_2 = doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column").ChildElementCount;
            int min = Lenta_economic_count_1 > Lenta_economic_count_2 ? Lenta_economic_count_2 : Lenta_economic_count_1;

            item_first[0] = doc.QuerySelector("div.b-feature__header > a").Text();
            item_first[1] = url + doc.QuerySelector("div.b-feature__header > a").GetAttribute("href");
            Lenta_economic.Add(item_first);

            item_second[0] = doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column > div:nth-child(1) > div.titles > div").Text();
            item_second[1] = url + doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column > div:nth-child(1) > a ").GetAttribute("href");
            Lenta_economic.Add(item_second);

            item_third[0] = doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column > div:nth-child(1) > div.titles > div").Text();
            item_third[1] = url + doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column > div:nth-child(1) > a ").GetAttribute("href");
            Lenta_economic.Add(item_third);
            for (int i = 2; i < min + 1; i++)
            {
                string[] item_fourth = new string[2];
                string[] item_fifth = new string[2];
                try
                {
                    item_fourth[0] = doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column > div:nth-child(" + i + ") > div.titles").Text();
                    item_fourth[1] = url + doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column > div:nth-child(" + i + ") > div.titles > h3 > a").GetAttribute("href");
                    Lenta_economic.Add(item_fourth);                 
                }
                catch (Exception)
                {
                }

                try
                {
                    item_fifth[0] = doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column > div:nth-child(" + i + ") > div.titles").Text();
                    item_fifth[1] = url + doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column > div:nth-child(" + i + ") > div.titles > h3 > a").GetAttribute("href");
                    Lenta_economic.Add(item_fifth);
                }
                catch (Exception)
                {
                }
            }

            return Lenta_economic;
        }

        public async Task<List<string[]>> GetSport()
        {
            Lenta_sport = new List<string[]>();

            string address = "https://lenta.ru/rubrics/sport/";

            var doc = await GetDoc(address);

            string[] item_first = new string[2];
            string[] item_second = new string[2];
            string[] item_third = new string[2];

            int Lenta_sport_count_1 = doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column").ChildElementCount;
            int Lenta_sport_count_2 = doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column").ChildElementCount;
            int min = Lenta_sport_count_1 > Lenta_sport_count_2 ? Lenta_sport_count_2 : Lenta_sport_count_1;

            item_first[0] = doc.QuerySelector("div.b-feature__header > a").Text();
            item_first[1] = url + doc.QuerySelector("div.b-feature__header > a").GetAttribute("href");
            Lenta_sport.Add(item_first);

            item_second[0] = doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column > div:nth-child(1) > div.titles > div").Text();
            item_second[1] = url + doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column > div:nth-child(1) > a ").GetAttribute("href");
            Lenta_sport.Add(item_second);

            item_third[0] = doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column > div:nth-child(1) > div.titles > div").Text();
            item_third[1] = url + doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column > div:nth-child(1) > a ").GetAttribute("href");
            Lenta_sport.Add(item_third);
            for (int i = 2; i < min + 1; i++)
            {
                string[] item_fourth = new string[2];
                string[] item_fifth = new string[2];

                try
                {
                    item_fourth[0] = doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column > div:nth-child(" + i + ") > div.titles").Text();
                    item_fourth[1] = url + doc.QuerySelector("div.span4:nth-child(1) > section.b-longgrid-column > div:nth-child(" + i + ") > div.titles > h3 > a").GetAttribute("href");
                    Lenta_sport.Add(item_fourth);
                }
                catch (Exception)
                {
                }

                try
                {
                    item_fifth[0] = doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column > div:nth-child(" + i + ") > div.titles").Text();
                    item_fifth[1] = url + doc.QuerySelector("div.span4:nth-child(2) > section.b-longgrid-column > div:nth-child(" + i + ") > div.titles > h3 > a").GetAttribute("href");
                    Lenta_sport.Add(item_fifth);
                }
                catch (Exception)
                {
                }
                
            }

            return Lenta_sport;
        }
    }
}