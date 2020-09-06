using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace searchfight
{
    class Program
    {
        static void Main(string[] args)
        {
            init(args);
        }

        public static void init(string[] args)
        {
            List<string> lstLanguagesValidation = new List<String>();
            List<SearchResult> lstSearchResult = new List<SearchResult>();

            lstLanguagesValidation = Utilities.ValidateLanguages(args);
            if (lstLanguagesValidation.Count > 0)
            {
                Console.WriteLine(Utilities.BREAKER_SYMBOL);
                Console.WriteLine("Programming Challenge\n");
                Console.Write("\rLoading {0} de {1}", 0, lstLanguagesValidation.Count);
                lstSearchResult = getSearchResult(lstLanguagesValidation);
                showSearchResult(lstSearchResult);

            }
            else
            {
                Console.WriteLine("Enter at least one value.");
            }
        }

        public static void showSearchResult(List<SearchResult> lstSearchResult)
        {
            Console.WriteLine("\n");
            var lstProgrammingLanguage = lstSearchResult.GroupBy(re => re.programmingLanguage);
            string generalSearchResult = "";

            /*Find Total Winner*/
            var lstTotaResultByLanguages = lstProgrammingLanguage.Select(lst => new { programmingLanguage = lst.Key, searchResult = lst.Sum(sum => sum.searchResult) }).OrderByDescending(lst => lst.searchResult).ToList();
            /*Find Yahoo Winner*/
            var lstYahooWinner = getMaxValueBySearchEngine(lstSearchResult, "Yahoo");
            /*Find Bing Winner*/
            var lstBingWinner = getMaxValueBySearchEngine(lstSearchResult, "Bing");


            /*Print General Results*/
            foreach (var lst in lstProgrammingLanguage)
            {
                generalSearchResult += lst.Key + ":\t";
                foreach (var search in lstSearchResult)
                {
                    if (lst.Key == search.programmingLanguage)
                    {
                        generalSearchResult += " / " + search.searchEngine + ":\t" + search.searchResult + " ";
                    }
                }
                Console.WriteLine(generalSearchResult);
                generalSearchResult = "";
            }

            Console.WriteLine(Utilities.BREAKER_SYMBOL);
            //===================================================================
            /*Print Bing Winner*/
            if (lstBingWinner.Count > 1)
            {
                Console.WriteLine("Bing - There was a tie between: ");
                foreach (var a in lstBingWinner)
                {
                    Console.WriteLine(a.programmingLanguage + ":\t" + a.searchResult);
                }
            }
            else
            {
                Console.WriteLine("Bing Winner:\t" + lstBingWinner[0].programmingLanguage);
            }
            //===================================================================
            /*Print Yahoo Winner*/
            if (lstYahooWinner.Count > 1)
            {
                Console.WriteLine("Yahoo - There was a tie between: ");
                foreach (var a in lstYahooWinner)
                {
                    Console.WriteLine(a.programmingLanguage + ":\t" + a.searchResult);
                }
            }
            else
            {
                Console.WriteLine("Yahoo Winner:\t" + lstYahooWinner[0].programmingLanguage);
            }
            Console.WriteLine(Utilities.BREAKER_SYMBOL);
            //===================================================================
            /*Total Winner*/
            Console.WriteLine("Total Winner:\t" + lstTotaResultByLanguages[0].programmingLanguage);



        }

        public static List<SearchResult> getSearchResult(List<String> lstLanguages)
        {

            List<SearchResult> lstSearchResult = new List<SearchResult>();
            SearchResult entitySearchResult = new SearchResult();


            for (int j = 0; j < lstLanguages.Count; j++)
            {

                entitySearchResult = new SearchResult();
                entitySearchResult.programmingLanguage = lstLanguages[j];
                entitySearchResult.searchEngine = "Bing";
                entitySearchResult.searchResult = getHttpResponse(lstLanguages[j], Utilities.URI_BING, Utilities.REGX_BING);
                lstSearchResult.Add(entitySearchResult);

                entitySearchResult = new SearchResult();
                entitySearchResult.programmingLanguage = lstLanguages[j];
                entitySearchResult.searchEngine = "Yahoo";
                entitySearchResult.searchResult = getHttpResponse(lstLanguages[j], Utilities.URI_YAHOO, Utilities.REGX_YAHOO);
                lstSearchResult.Add(entitySearchResult);

                Console.Write("\rLoading {0} de {1}", j + 1, lstLanguages.Count);

            }
            return lstSearchResult;

        }

        public static long getHttpResponse(string language, string uri, string regx)
        {

            long response = 0;
            var uriBuilder = new UriBuilder(String.Format(uri + "{0}", language));
            var responseHtml = Utilities.getHttpBody(uriBuilder).Result;

            if (responseHtml != "")
            {
                response = getValueFromSearch(responseHtml, regx);
            }

            return response;


        }

        public static long getValueFromSearch(string responseHtml, string regx)
        {

            long response = 0;

            Match x = Utilities.FindMath(responseHtml, regx);
            if (x.Success)
            {
                response = Utilities.ParseToLong(x.Groups[1].Value);
            }

            return response;

        }

        public static List<SearchResult> getMaxValueBySearchEngine(List<SearchResult> lstSearchResult, string searchEngine)
        {
            var maxSearchValue = lstSearchResult.Where(a => a.searchEngine == searchEngine).Max(max => max.searchResult);
            var response = lstSearchResult.Where(a => a.searchEngine == searchEngine && a.searchResult == maxSearchValue).ToList();
            return response;
        }


    }
}
