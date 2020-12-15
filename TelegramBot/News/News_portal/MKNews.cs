using System;
using System.Collections.Generic;
using AngleSharp;
using AngleSharp.Dom;
using System.Threading.Tasks;

namespace News.News_portal
{
    internal class MKNews
    {
        public List<string[]> MK_politic { get; private set; }
        public List<string[]> MK_economic { get; private set; }
        public List<string[]> MK_sport { get; private set; }
        private string url = "https://www.mk.ru";


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

        public async Task<List<string[]>> GetNews(string address)
        {
            List<string[]> temp = new List<string[]>();

            var doc = await GetDoc(address);

            int MK_count = doc.QuerySelector("ul.article-listing__day-list").ChildElementCount;

            for (int i = 1; i < MK_count + 1; i++)
            {
                string[] item = new string[2];
                try
                {
                    item[0] = doc.QuerySelector("ul.article-listing__day-list > li:nth-child(" + i + ") > article > div:nth-child(3) > a > h3").Text();
                    item[1] = doc.QuerySelector("ul.article-listing__day-list > li:nth-child(" + i + ") > article > div:nth-child(3) > a").GetAttribute("href");

                    temp.Add(item);
                }
                catch (Exception)
                {
                }             
            }

            return temp;
        }

        public async Task<List<string[]>> GetPolitic()
        {
            string address = "https://www.mk.ru/politics";
            MK_politic = await GetNews(address);

            return MK_politic;
        }

        public async Task<List<string[]>> GetEconomic()
        {
            string address = "https://www.mk.ru/economics";
            MK_economic = await GetNews(address);

            return MK_economic;
        }

        public async Task<List<string[]>> GetSport()
        {
            string address = "https://www.sportmk.ru";
            MK_sport = await GetNews(address);

            return MK_sport;
        }

    }
}