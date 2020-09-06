using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using searchfight;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace UnitTestSeachFight
{
    [TestClass]
    public class SearchFightTest
    {
        //It has to return Empty if the regular expression doesn't appear in the body parameter
        [TestMethod]
        public void FindMathMethodValidation()
        {
            var bodyToExporer = "searchFight";
            var regx = "x";
            var response = Utilities.FindMath(bodyToExporer, regx);
            Assert.AreSame(Match.Empty, response);
        }

        //It has to return a number regardless the input parameter type
        [TestMethod]
        public void ParseToLongMethodValidation()
        {
            var inParameter = "&/($%&/·%$&";
            var response = Utilities.ParseToLong(inParameter);
            Assert.IsInstanceOfType(response,typeof(long));
        }

        //It has to skip the repeated or empty values
        [TestMethod]
        public void ValidateLanguagesMethodValidation()
        {
            string[] arrayLanguages = { "JAVA", "java","C#","c#","" };
            var response = Utilities.ValidateLanguages(arrayLanguages);
            Assert.AreEqual(response.Count, 2);
        }

        //Always it has to return a string value
        [TestMethod]
        public void GetHttpBodyMethodValidation()
        {
            var uri = "www.google.com/search?q=";
            var language = "java";
            Task<string> response = Utilities.GetHttpBody(uri, language);
            Assert.IsInstanceOfType(response, typeof(Task<string>));
        }

    }
}
