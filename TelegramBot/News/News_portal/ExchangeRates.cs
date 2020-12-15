using System;
using AngleSharp;
using AngleSharp.Dom;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace News.News_portal
{
    internal class ExchangeRates
    {
        public List<string> Rates { get; private set; }
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
        public async Task<List<string>> GetRates()
        {
            Rates = new List<string>();
            var address = "https://www.banki.ru/products/currency/cash/moskva/";

            var doc = await GetDoc(address);
            try
            {
                Rates.Add(doc.QuerySelector("div.table-flex.table-flex--no-borders.akbars-table > div:nth-child(2) > div:nth-child(2)").Text());
                Rates.Add(doc.QuerySelector("div.table-flex.table-flex--no-borders.akbars-table > div:nth-child(3) > div:nth-child(2)").Text());
            }
            catch (Exception)
            {
            }
            return Rates;
        }
    }
}
