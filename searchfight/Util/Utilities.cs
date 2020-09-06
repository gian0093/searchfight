using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Text.RegularExpressions;

namespace searchfight
{
    public class Utilities
    {
        public Utilities()
        {

        }
        public const string URI_BING = "https://www.bing.com/search?q="; /*Uri Bing Engine */
        public const string URI_YAHOO = "https://espanol.search.yahoo.com/search?p="; /*Uri Yahoo Engine */
        public const string REGX_BING = "\\<span[^\\>]+class=\"sb_count\"[^\\>]*\\>([\\d\\.\\,]+)"; /*Regx Bing Engine */
        public const string REGX_YAHOO = "\\<span*\\>([\\d\\.\\,]+)"; /*Regx Yahoo Engine */
        public const string BREAKER_SYMBOL = "====================================================================="; /*Breaker used for showing results*/
        public static int timeOut = 5;  /*Http Timeout in seconds*/


        public static async Task<string> getHttpBody(UriBuilder script)
        {
            var response = "";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(timeOut);
                    response =  await httpClient.GetStringAsync(script.Uri);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError making Http call: " + ex.Message);
                }
                return response;

            }
        }

        public static Match FindMath(string responseText, string regx)
        {
            Match match = Match.Empty;
            try
            {
                Regex re = new Regex(regx);
                match = re.Match(responseText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error validating regular expression: " + ex.Message);
            }
            return match;

        }

        public static long ParseToLong(string searchResult)
        {
            long response = 0;
            if (searchResult == "") { return 0; }

            try
            {
                response = long.Parse(searchResult.Replace(",", "").Replace(".", ""));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting number value: "+ ex.Message);
            }
            return response;

        }

        public static List<string> ValidateLanguages(string[] arrayLanguages)
        {

            List<string> lstlanguages = new List<String>();

            if (arrayLanguages.Length > 0)
            {
                foreach (var inparameter in arrayLanguages)
                {
                    if (!inparameter.Trim().Equals(""))
                    {
                        lstlanguages.Add(inparameter);
                    }
                }

                return lstlanguages.Distinct().ToList();
            }
            else
            {
                lstlanguages = new List<String>();
                return lstlanguages;
            }
        }

    }
}
