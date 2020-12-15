using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using News.News_portal;

namespace News
{
    internal class NewsAgregator
    {
        private LentaNews L = new LentaNews();
        private RiaNews R = new RiaNews();
        private MKNews M;
        private ExchangeRates ER;

        public async Task<string> GetMain()
        {
            await L.GetMain();
            await R.GetMain();

            string News = "";

            if (L.Lenta_main == null || R.Ria_main == null)
            {
                int i = 0;
                if (L.Lenta_main == null && R.Ria_main == null)
                {
                    return News = "Возникли проблемы при поиске указанных новостей по запросу \"Главное\"";
                }
                else if (L.Lenta_main == null)
                {
                    
                    foreach (var item in R.Ria_main)
                    {
                        i++;
                        News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                        if (i == 12)
                            break;
                    }
                    return News;
                }
                else
                {
                    foreach (var item in L.Lenta_main)
                    {
                        i++;
                        News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                        if (i == 12)
                            break;
                    }
                    return News;
                }         
            }
            else
            {              
                Random rnd = new Random();
                List<int> used_num = new List<int>();
                int min = L.Lenta_main.Count > R.Ria_main.Count ? R.Ria_main.Count : L.Lenta_main.Count;

                for (int i = 0; i < 6; i++)
                {
                    
                    int rand = rnd.Next(0, min);

                    while (used_num.Find(x => x == rand) != 0)
                    {
                        rand = rnd.Next(0, min);
                    }
                    used_num.Add(rand);

                    News += L.Lenta_main[rand][0] + Environment.NewLine + "- " + L.Lenta_main[rand][1] + Environment.NewLine;
                    News += R.Ria_main[rand][0] + Environment.NewLine + "- " + R.Ria_main[rand][1] + Environment.NewLine;
                }

                return News;
            }
        }

        public async Task<string> GetPolitic()
        {
            M = new MKNews();

            await L.GetPolitic();
            await R.GetPolitic();
            await M.GetPolitic();

            string News = "";

            if (L.Lenta_politic == null || R.Ria_politic == null || M.MK_politic == null)
            {
                if (L.Lenta_politic == null && R.Ria_politic == null && M.MK_politic == null)
                    return News = "Возникли проблемы при поиске указанных новостей по запросу \"Политика\"";
                else
                {
                    int i = 0;

                    if (L.Lenta_politic != null)
                    {
                        foreach (var item in L.Lenta_politic)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }

                    if (R.Ria_politic != null)
                    {
                        foreach (var item in R.Ria_politic)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }

                    if (M.MK_politic != null)
                    {
                        foreach (var item in M.MK_politic)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }
                    return News;
                }
            }
            else
            {
                Random rnd = new Random();
                List<int> used_num = new List<int>();
                int min = L.Lenta_politic.Count;

                if (min > R.Ria_politic.Count || min > M.MK_politic.Count)
                {
                    min = R.Ria_politic.Count > M.MK_politic.Count ? M.MK_politic.Count : R.Ria_politic.Count;
                }

                for (int i = 0; i < 4; i++)
                {
                    int rand = rnd.Next(0, min);

                    while (used_num.Find(x => x == rand) != 0)
                    {
                        rand = rnd.Next(0, min);
                    }
                    used_num.Add(rand);

                    News += L.Lenta_politic[rand][0] + Environment.NewLine + "- " + L.Lenta_politic[rand][1] + Environment.NewLine;
                    News += R.Ria_politic[rand][0] + Environment.NewLine + "- " + R.Ria_politic[rand][1] + Environment.NewLine;
                    News += M.MK_politic[rand][0] + Environment.NewLine + "- " + M.MK_politic[rand][1] + Environment.NewLine;
                }

                return News;
            }
        }

        public async Task<string> GetEconomic()
        {
            M = new MKNews();

            await L.GetEconomic();
            await R.GetEconomic();
            await M.GetEconomic();

            string News = "";

            if (L.Lenta_economic == null || R.Ria_economic == null || M.MK_economic == null)
            {
                if (L.Lenta_economic == null && R.Ria_economic == null && M.MK_economic == null)
                    return News = "Возникли проблемы при поиске указанных новостей по запросу \"Экономика\"";
                else
                {
                    int i = 0;

                    if (L.Lenta_economic != null)
                    {
                        foreach (var item in L.Lenta_politic)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }

                    if (R.Ria_economic != null)
                    {
                        foreach (var item in R.Ria_politic)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }

                    if (M.MK_economic != null)
                    {
                        foreach (var item in M.MK_economic)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }                  
                    return News;
                }

            }
            else
            {
                Random rnd = new Random();
                List<int> used_num = new List<int>();

                int min = L.Lenta_economic.Count;

                if (min > R.Ria_economic.Count || min > M.MK_economic.Count)
                {
                    min = R.Ria_economic.Count > M.MK_economic.Count ? (int)M.MK_economic.Count : R.Ria_economic.Count;
                }

                for (int i = 0; i < 4; i++)
                {
                    int rand = rnd.Next(0, min);

                    while (used_num.Find(x => x == rand) != 0)
                    {
                        rand = rnd.Next(0, min);
                    }
                    used_num.Add(rand);

                    News += L.Lenta_economic[rand][0] + Environment.NewLine + "- " + L.Lenta_economic[rand][1] + Environment.NewLine;
                    News += R.Ria_economic[rand][0] + Environment.NewLine + "- " + R.Ria_economic[rand][1] + Environment.NewLine;
                    News += M.MK_economic[rand][0] + Environment.NewLine + "- " + M.MK_economic[rand][1] + Environment.NewLine;
                }

                return News;
            }           
        }

        public async Task<string> GetSport()
        {
            M = new MKNews();

            await L.GetSport();
            await R.GetSport();
            await M.GetSport();

            string News = "";

            if (L.Lenta_sport == null || R.Ria_sport == null || M.MK_sport == null)
            {
                if (L.Lenta_sport == null && R.Ria_sport == null && M.MK_sport == null)
                    return News = "Возникли проблемы при поиске указанных новостей по запросу \"Спорт\"";
                else
                {
                    int i = 0;

                    if (L.Lenta_sport != null)
                    {
                        foreach (var item in L.Lenta_sport)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }

                    if (R.Ria_sport != null)
                    {
                        foreach (var item in R.Ria_sport)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }

                    if (M.MK_sport != null)
                    {
                        foreach (var item in M.MK_sport)
                        {
                            i++;
                            News += item[0] + Environment.NewLine + "- " + item[1] + Environment.NewLine;
                            if (i == 6)
                                break;
                        }
                        i = 0;
                    }

                    return News;
                }
            }
            else
            {
                Random rnd = new Random();
                List<int> used_num = new List<int>();

                int min = L.Lenta_sport.Count;

                if (min > R.Ria_sport.Count || min > M.MK_sport.Count)
                {
                    min = R.Ria_sport.Count > M.MK_sport.Count ? M.MK_sport.Count : R.Ria_sport.Count;
                }

                for (int i = 0; i < 4; i++)
                {
                    int rand = rnd.Next(0, min);

                    while (used_num.Find(x => x == rand) != 0)
                    {
                        rand = rnd.Next(0, min);
                    }
                    used_num.Add(rand);

                    News += L.Lenta_sport[rand][0] + Environment.NewLine + "- " + L.Lenta_sport[rand][1] + Environment.NewLine;
                    News += R.Ria_sport[rand][0] + Environment.NewLine + "- " + R.Ria_sport[rand][1] + Environment.NewLine;
                    News += M.MK_sport[rand][0] + Environment.NewLine + "- " + M.MK_sport[rand][1] + Environment.NewLine;
                }

                return News;
            }           
        }

        public async Task<string> GetRates()
        {
            ER = new ExchangeRates();

            await ER.GetRates();

            string News = "";

            if (ER.Rates == null)
                return News = "Возникли проблемы при поиске указанных новостей по запросу \"Курс валют\"";
            else
                News = "Доллар - " + ER.Rates[0] + Environment.NewLine + "Евро - " + ER.Rates[1];

            return News;
        }
    }
}